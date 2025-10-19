using Application;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.RoleAgg.Interfaces.Repository;
using Domain.UserAgg;
using Domain.UserAgg.Interfaces.Repository;
using Infrastructure.Persistent.Ef.Product.Repository;
using Infrastructure.Persistent.Ef.Role.Repository;
using Infrastructure.Persistent.Ef.User.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureBootStrapper
    {
        public static IServiceCollection InfrastructureInject(this IServiceCollection services, IConfiguration configuration) 
        {
            services.DataBaseConfig(configuration);
            services.ConfigureJwtAuthentication(configuration);
            services.ApplicationInject();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
