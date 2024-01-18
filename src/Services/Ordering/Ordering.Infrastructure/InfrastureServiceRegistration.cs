using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Infrastructure.Email;
using Ordering.Infrastructure.Persistence;
using Ordering.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure
{
    public static class InfrastureServiceRegistration
    {
        public static IServiceCollection AddInfrastureService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrderDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("OrderDB")));
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IEmailService, EmailService>();
            return services;
        }
    }
}
