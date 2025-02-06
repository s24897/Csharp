using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PrzykladoweKolowkium2.Models.DTOs;
using PrzykladoweKolowkium2.Services;

namespace PrzykladoweKolowkium2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IDbService _dbService;

        public OrdersController(IDbService dbService)
        {
            _dbService=dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrdersData(string? clientLastName)
        {
            //Pobierz wszystkie zamówienia / wszystkie zamówienia klienta o danym nazwisku
            var orders = await _dbService.GetOrdersData(clientLastName);

            //Przemapuj obiekt zamówień na DTO
            return Ok(orders.Select(e => new GetOrdersDTO
            {
                ID= e.ID,
                AcceptedAt = e.AcceptedAt,
                FulfilledAt = e.FulfilledAt,
                Comments = e.Comments,
                Pastries = e.OrderPastries.Select(p => new GetOrdersPastryDTO
                {
                    Name = p.Pastry.Name,
                    Price = p.Pastry.Price,
                    Amount = p.Amount
                }).ToList()
            }));
        }
    }
}
