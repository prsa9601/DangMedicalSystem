using Infrastructure.Mapping.Profiles;
using Infrastructure.Mapping.Service;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Query.Mapping.Registration
{
    public static class AutoMapperDependencyInjection
    {
        public static IServiceCollection AddMapping(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            //services.AddAutoMapper(
            //typeof(Infrastructure.AssemblyReference).Assembly,    // Profileهای Infrastructure
            //typeof(Query.Mapping.Profiles.UserProfile).Assembly      // Profileهای Application (اگر وجود دارن)
            //typeof(Web.AssemblyReference).Assembly               // Profileهای Presentation (اگر وجود دارن)
            //);

            //services.AddAutoMapper(config =>
            //{
            //}, typeof(Query.Mapping.Profiles.UserProfile).Assembly);
            services.AddAutoMapper(config =>
            {
                config.AddProfile<UserProfile>();
            }, typeof(UserProfile).Assembly);
            services.AddScoped<IMapperService, AutoMapperService>();
            return services;
        }
    }
}
