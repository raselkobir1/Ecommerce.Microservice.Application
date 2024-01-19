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
                    FirstName = "Md",
                    LastName = "Rasel",
                    EmailAddress = "raselkobir57@gmail.com",
                    Address = "Dhaka",
                    PhoneNumber = "01234567890",
                    State = "Dhaka",
                    ZipCode = "7890",
                    TotalPrice = 1000,

                    City = "Dhaka",
                    CVV = "CVV",
                    CardName = "Rasel",
                    CardNumber = "12345678905698",
                    Expiration = DateTime.Now.AddDays(10).ToString(),
                    PaymentMethod = 1,
                    
                    CreatedBy = "Me",
                    CreatedDate = DateTime.Now,
                    UpdatedBy = "1",
                    UpdatedDate = DateTime.Now,

                });
        }
    }
}
