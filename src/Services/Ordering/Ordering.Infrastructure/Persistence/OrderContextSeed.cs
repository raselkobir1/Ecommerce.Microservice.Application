using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Persistence
{
    public class OrderContextSeed
    {
        public static async Task Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().HasData(
                new Order()
                {
                    Id = 1,
                    UserName = "rasel@gmail.com",
                    FirstName ="Md",
                    LastName = "Rasel",
                    EmailAddress = "raselkobir57@gmail.com",
                    Address = "Dhaka",
                    TotalPrice = 1000,
                    City = "Dhaka"
                });
        }
    }
}
