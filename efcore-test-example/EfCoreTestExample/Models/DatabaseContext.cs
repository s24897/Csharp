using Microsoft.EntityFrameworkCore;

namespace PrzykladoweKolowkium2.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderPastry> OrderPastries { get; set; }
        public DbSet<Pastry> Pastries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var clients = new List<Client>
            {
                new Client
                {
                    ID = 1,
                    FirstName = "Kacper",
                    LastName = "Kowalski"
                }
            };

            var employees = new List<Employee>
            {
                new Employee
                {
                    ID = 1,
                    FirstName = "Michal",
                    LastName = "Kowalski"
                }
            };

            var orders = new List<Order>
            {
                new Order
                {
                    ID = 1,
                    AcceptedAt = DateTime.UtcNow,
                    ClientID = 1,
                    EmployeeID = 1
                }
            };

            var pastries = new List<Pastry>
            {
                new Pastry
                {
                    ID = 1,
                    Name = "Text",
                    Price = 4.20M,
                    Type = "Text"
                }
            };

            var orderPastries = new List<OrderPastry>
            {
                new OrderPastry
                {
                    OrderID = 1,
                    PastryID = 1,
                    Amount = 1,
                }
            };

            modelBuilder.Entity<Client>().HasData(clients);
            modelBuilder.Entity<Employee>().HasData(employees);
            modelBuilder.Entity<Order>().HasData(orders);
            modelBuilder.Entity<Pastry>().HasData(pastries);
            modelBuilder.Entity<OrderPastry>().HasData(orderPastries);
        }
    }
}
