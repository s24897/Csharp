using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolowkium2.Models;
using PrzykladoweKolowkium2.Models.DTOs;
using PrzykladoweKolowkium2.Services;
using System.Transactions;

namespace PrzykladoweKolowkium2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IDbService _dbService;

        public ClientsController(IDbService dbService)
        {
            _dbService=dbService;
        }

        [HttpPost("{clientId}/orders")]
        public async Task<IActionResult> AddOrder(int clientId, AddOrderForClientDTO data)
        {
            //Sprawdź czy klient o podanym id istnieje
            if (!await _dbService.DoesClientExist(clientId)) 
                return NotFound("Client not found");

            //Sprawdź czy pracownik o podanym id istnieje
            if (!await _dbService.DoesEmployeeExist(data.EmployeeID))
                return NotFound("Employee not found");

            //Sprawdź czy wypieki o podanym id istnieją - jeżeli tak,
            //to utwórz dla nich odpowiednie obiekty bazując na modelu OrderPastry
            var pastries = new List<OrderPastry> { };
            foreach (var pastry in data.Pastries)
            {
                var pastryFromDb = await _dbService.GetPastryByName(pastry.Name);

                if (pastryFromDb is null) 
                    return NotFound($"Pastry {pastry.Name} does not exist");

                pastries.Add(new OrderPastry
                {
                    PastryID = pastryFromDb.ID,
                    Amount = pastry.Amount,
                    Comme = pastry.Comments
                });
            }


            //Rozpoczęcie transakcji
            using (var scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                //Utwórz nowe zamówienie
                var order = await _dbService.AddOrder(new Order
                {
                    AcceptedAt = data.AcceptedAt,
                    Comments = data.Comments,
                    ClientID = clientId,
                    EmployeeID = data.EmployeeID
                });

                //Przypisz do wcześniej utworzonej listy obiektów powstałe zamówienie
                //(w ten sposób automatycznie zostanie przypisana wartość klucza obcego)
                pastries.ForEach(p => p.Order = order);

                //Dodaj do bazy obiekty z listy "pastries"
                await _dbService.AddOrderPastries(pastries);

                //Zatwierdzenie transakcji
                scope.Complete();
            }

            return Created("", "");
        }
    }
}
