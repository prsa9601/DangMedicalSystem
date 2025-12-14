using Facade.Auth;
using Facade.Contract;
using Facade.Notification;
using Facade.Order;
using Facade.Product;
using Facade.Profit;
using Facade.PurchaseReport;
using Facade.Role;
using Facade.Session;
using Facade.SitEntities;
using Facade.SiteSetting;
using Facade.Stock;
using Facade.User;
using Microsoft.Extensions.DependencyInjection;
using Query.Mapping.Registration;

namespace Facade
{
    public static class FacadeBootstrapper
    {
        public static void InitFacadeDependency(this IServiceCollection services)
        {

            services.AddScoped<IAuthFacade, AuthFacade>();

            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<IPurchaseReportFacade, PurchaseReportFacade>();
            services.AddScoped<IOrderFacade, OrderFacade>();
            services.AddScoped<IInventoryFacade, InventoryFacade>();
            services.AddScoped<IUserFacade, UserFacade>();
            services.AddScoped<IStockFacade, StockFacade>();
            services.AddScoped<IProfitFacade, ProfitFacade>();
            services.AddScoped<INotificationFacade, NotificationFacade>();
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<ISiteSettingFacade, SiteSettingFacade>();
            services.AddScoped<IContractFacade, ContractFacade>();

            services.AddScoped<IUserSessionFacade, UserSessionFacade>();

            services.AddScoped<ISiteEntityFacade, SiteEntityFacade>();

            services.AddMapping();


        }
    }
}
