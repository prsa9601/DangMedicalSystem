using Application.Auth.Commands.Builder;
using Application.Product.Service;
using Application.User.Services;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Domain.ProductAgg.Interfaces.Services;
using Domain.UserAgg.Interfaces.Builder;
using Domain.UserAgg.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Runtime.Serialization.Formatters;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static IServiceCollection ApplicationInject(this IServiceCollection services)
        {
            //Builders
            services.AddScoped<IUserBuilder, UserBuilder>();



            services.AddScoped<IUserDomainService, UserDomainService>();
            services.AddScoped<IProductDomainService, ProductDomainService>();

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
