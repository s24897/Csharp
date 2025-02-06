using Microsoft.EntityFrameworkCore;
using PrzykladoweKolowkium2.Models;

namespace PrzykladoweKolowkium2.Services
{
    public interface IDbService
    {
        public Task<ICollection<Order>> GetOrdersData(string? clientLastName = null);
        public Task<bool> DoesClientExist(int clientId);
        public Task<bool> DoesEmployeeExist(int employeeID);
        public Task<Pastry?> GetPastryByName(string pastryName);
        public Task<Order> AddOrder(Order newOrder);
        public Task AddOrderPastries(ICollection<OrderPastry> pastries);
    }

    public class DbService : IDbService
    {
        private readonly DatabaseContext _databaseContext;

        public DbService(DatabaseContext databaseContext)
        {
            _databaseContext=databaseContext;
        }

        public async Task<Order> AddOrder(Order newOrder)
        {
            await _databaseContext.Orders.AddAsync(newOrder);
            await _databaseContext.SaveChangesAsync();
            return newOrder;
        }

        public async Task AddOrderPastries(ICollection<OrderPastry> pastries)
        {
            await _databaseContext.OrderPastries.AddRangeAsync(pastries);
            await _databaseContext.SaveChangesAsync();
        }

        public async Task<bool> DoesClientExist(int clientId)
        {
            return await _databaseContext.Clients.AnyAsync(e => e.ID == clientId);
        }

        public async Task<bool> DoesEmployeeExist(int employeeID)
        {
            return await _databaseContext.Employees.AnyAsync(e => e.ID == employeeID);
        }

        public async Task<ICollection<Order>> GetOrdersData(string? clientLastName = null)
        {
            return await _databaseContext.Orders
                .Include(e => e.Client)
                .Include(e => e.OrderPastries).ThenInclude(e => e.Pastry)
                .Where(e => clientLastName == null || e.Client.LastName == clientLastName)
                .ToListAsync();
        }

        public async Task<Pastry?> GetPastryByName(string pastryName)
        {
            return await _databaseContext.Pastries.FirstOrDefaultAsync(e => e.Name == pastryName);
        }
    }
}
