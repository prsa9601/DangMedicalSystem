using Facade.Auth;
using Facade.Product;
using Facade.Session;
using Facade.User;
using Microsoft.Extensions.DependencyInjection;

namespace Facade
{
    public static class FacadeBootstrapper
    {
        public static void InitFacadeDependency(this IServiceCollection services)
        {

            services.AddScoped<IAuthFacade, AuthFacade>();

            services.AddScoped<IProductFacade, ProductFacade>();
            services.AddScoped<IUserFacade, UserFacade>();

            services.AddScoped<IUserSessionFacade, UserSessionFacade>();


        }
    }
}
