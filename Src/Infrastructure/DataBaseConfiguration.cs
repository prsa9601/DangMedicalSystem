using Application.Auth.Commands.Register;
using Application.Utilities;
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

            //MediatR
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(Directories).Assembly,
                    typeof(RegisterUserCommandHandler).Assembly
                );
            });

            //configuration.GetConnectionString("DefaultConnection");
            return services;
        }
    }
}
