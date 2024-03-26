using Domain.Model;
using Infrastructure.DataAccess.Context;
using Infrastructure.DataAccess.DataBaseConfig.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Api.Utils
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IDatabaseContext, DatabaseContext>();
            services.AddTransient<IGoalRepository, GoalRepository>();
            
            return services;
        }
    }
}