using Backend_Final.Database;
using Backend_Final.Services.Abstracts;
using Backend_Final.Services.Concretes;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Infrastructure.Configurations
{
    public static class RegisterCustomServicesConfigurations
    {
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmailService, SMTPService>();
            services.AddScoped<IUserService, UserService>();
            //services.AddScoped<IBasketService, BasketService>();
            services.AddSingleton<IFileService, FileService>();
            services.AddScoped<IUserActivationService, UserActivationService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<INotificationService, NotificationService>();
        }
    }
}
