using Application;
using Application.Auth.Shared.Abstractions.Interfaces;
using Domain.NotificationAgg.Interfaces.Repository;
using Domain.OrderAgg.Interfaces.Repository;
using Domain.ProductAgg.Interfaces.Repository;
using Domain.PurchaseReportAgg.Interfaces.Repository;
using Domain.RoleAgg.Interfaces.Repository;
using Domain.UserAgg;
using Domain.UserAgg.Interfaces.Repository;
using Infrastructure.Mapping.Service;
using Infrastructure.Persistent.Ef.Notification.Repository;
using Infrastructure.Persistent.Ef.Order.Repository;
using Infrastructure.Persistent.Ef.Product.Repository;
using Infrastructure.Persistent.Ef.PurchaseReport.Repository;
using Infrastructure.Persistent.Ef.Role.Repository;
using Infrastructure.Persistent.Ef.User.Repository;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureBootStrapper
    {
        public static IServiceCollection InfrastructureInject(this IServiceCollection services, IConfiguration configuration) 
        {
            services.DataBaseConfig(configuration);
            services.ApplicationInject();


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPurchaseReportRepository, PurchaseReportRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddTransient<System.Random>();
            services.AddScoped<IOtpService, OtpService>();
            services.ConfigureJwtAuthentication(configuration);
            return services;
        }
    }
}
