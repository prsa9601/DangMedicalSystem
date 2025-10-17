using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DataBaseConfiguration
    {
        public static IServiceCollection DataBaseConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
            //configuration.GetConnectionString("DefaultConnection");
            return services;
        }
    }
}
