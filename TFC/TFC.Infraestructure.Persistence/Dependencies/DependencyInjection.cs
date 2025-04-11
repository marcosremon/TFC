using Microsoft.Extensions.DependencyInjection;
using TFC.Application.Interface.Application;
using TFC.Application.Interface.Persistence;
using TFC.Application.Main;
using TFC.Infraestructure.Persistence.Repository;

namespace TFC.Infrastructure.Persistence.Dependencies
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Application
            services.AddScoped<IUserApplication, UserApplication>();

            // Repository
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
