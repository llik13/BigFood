using Aplication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AdminContext>(options =>
            {
                string connectionString = configuration.GetConnectionString("MySqlConnection");
                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new InvalidOperationException("Connection string 'MySqlConnection' is not configured.");
                }
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddScoped<IApplicationDbContext>(provider =>
            {
                var context = provider.GetService<AdminContext>();
                if (context == null)
                {
                    throw new InvalidOperationException("AdminContext is not resolved.");
                }
                return context;
            });

            return services;
        }
    }
}
