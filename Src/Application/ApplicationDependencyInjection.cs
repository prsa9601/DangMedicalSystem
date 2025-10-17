using Application.Product.Service;
using Domain.ProductAgg.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection Inject(this IServiceCollection services)
        {
            services.AddScoped<IProductDomainService, ProductDomainService>();

            return services;
        }
    }
}
