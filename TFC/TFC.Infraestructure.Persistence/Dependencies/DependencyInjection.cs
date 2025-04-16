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
            services.AddScoped<IRoutineApplication, RoutineApplication>();
            services.AddScoped<ISplitDayApplication, SplitDayApplication>();
            services.AddScoped<IExerciseApplication, ExerciseApplication>();

            // Repository
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoutineRepository, RoutineRepository>();
            services.AddScoped<ISplitDayRepository, SplitDayRepository>();
            services.AddScoped<IExerciseRepository, ExerciseRepository>();


            return services;
        }
    }
}
