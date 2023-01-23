using Backend_Final.Database;
using Microsoft.EntityFrameworkCore;

namespace Backend_Final.Infrastructure.Configurations
{
    public static class DatabaseConfigurations
    {
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(o =>
            {
                o.UseSqlServer(configuration.GetConnectionString("Zeynal"));
            });
        }
    }
}
