using Infrastructure.Mapping.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Mapping.Registration
{
    public static class AutoMapperDependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(
            //    typeof(Infrastructure.AssemblyReference).Assembly,    // Profileهای Infrastructure
            //    typeof(Application.AssemblyReference).Assembly,       // Profileهای Application (اگر وجود دارن)
            //    typeof(Web.AssemblyReference).Assembly               // Profileهای Presentation (اگر وجود دارن)
            //);
            services.AddScoped<IMapperService, AutoMapperService>();
            return services;
        }
    }
}
