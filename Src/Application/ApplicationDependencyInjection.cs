using Application.Auth.Commands.Builder;
using Application.Notification.Services;
using Application.Order.Services;
using Application.Product.Service;
using Application.Profit;
using Application.PurchaseReport.Services;
using Application.User.Services;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using Domain.NotificationAgg.Interfaces.Services;
using Domain.OrderAgg.Interfaces.Services;
using Domain.ProductAgg.Interfaces.Services;
using Domain.ProfitAgg.Service;
using Domain.PurchaseReportAgg.Interfaces.Services;
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
            services.AddScoped<IPurchaseReportDomainService, PurchaseReportDomainService>();
            services.AddScoped<IProfitService, ProfitService>();
            services.AddScoped<IOrderDomainService, OrderDomainService>();
            services.AddScoped<INotificationDomainService, NotificationDomainService>();

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
