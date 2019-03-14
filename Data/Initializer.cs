using OData.NetCore.Models;
using System.Collections.Generic;
using System.Linq;

namespace OData.NetCore.Data
{
    public static class Initializer
    {
        public static void InitializeData(DatabaseContext dbContext)
        {
            dbContext.Database.EnsureCreated();
            AddClients(dbContext);
            AddProducts(dbContext);
        }

        private static void AddClients(DatabaseContext dbContext)
        {
            var clients = new List<Client>()
            {
                new Client
                {
                    Id = 1,
                    Name = "John Paradise"
                },
                new Client
                {
                    Id = 2,
                    Name = "Daniel Luxemburg"
                }
            };

            if (!dbContext.Clients.Any())
            {
                dbContext.AddRange(clients);
                dbContext.SaveChanges();
            }
        }

        private static void AddProducts(DatabaseContext dbContext)
        {
            var products = new List<Product>()
            {
                new Product
                {
                    Id = 1,
                    ClientId = 1,
                    Name = "Product 1",
                    Price = 20
                },
                new Product
                {
                    Id = 2,
                    ClientId = 1,
                    Name = "Product 2",
                    Price = 30
                },
                new Product
                {
                    Id = 3,
                    ClientId = 2,
                    Name = "Product 3",
                    Price = 10
                }
            };

            if (!dbContext.Products.Any())
            {
                dbContext.Products.AddRange(products);
                dbContext.SaveChanges();
            }
        }
    }
}
