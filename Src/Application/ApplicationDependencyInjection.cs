using Application.Auth.Commands.Builder;
using Application.Product.Service;
using Domain.ProductAgg.Interfaces.Services;
using Domain.UserAgg.Interfaces.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection ApplicationInject(this IServiceCollection services)
        {
            //Builders
            services.AddScoped<IUserBuilder, UserBuilder>();



            services.AddScoped<IProductDomainService, ProductDomainService>();

            return services;
        }
    }
}
