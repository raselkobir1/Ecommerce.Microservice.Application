using Ordering.Infrastructure.Email;

namespace Ordering.API
{
    public static class OrderServiceRegistration
    {
        public static IServiceCollection AddOrderServices(this IServiceCollection services)
        {
            services.AddOptions<EmailSetting>().BindConfiguration(nameof(EmailSetting)).ValidateDataAnnotations();

            return services;
        }
    }
}
