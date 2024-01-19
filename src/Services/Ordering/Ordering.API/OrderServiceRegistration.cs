using Ordering.Infrastructure.Email;
using System.Reflection;

namespace Ordering.API
{
    public static class OrderServiceRegistration
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services)
        {
            services.AddOptions<EmailSetting>().BindConfiguration(nameof(EmailSetting)).ValidateDataAnnotations();
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
