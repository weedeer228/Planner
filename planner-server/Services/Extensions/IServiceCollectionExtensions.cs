using DbService.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Model.Db;
using Model.Interfaces;
using Model.Interfaces.Helpers;
using Planner.Helpers;
using Services.Db;

namespace DbService.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddPlannerDbContext(this IServiceCollection services)
        {
            return services.AddDbContext<PlannerContext>(options =>
                options.UseInMemoryDatabase(databaseName: "PlannerDb"));
        }

        public static IServiceCollection AddPlannerRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IRepository<TableSKU>, EFRepository<TableSKU>>()
                .AddScoped<IRepository<TableSKUSub>, EFRepository<TableSKUSub>>()
                .AddScoped<IRepository<SKUPeriodData>, EFRepository<SKUPeriodData>>();
        }
        public static IServiceCollection AddFilterHelper(this IServiceCollection services)
        {
            return services.AddTransient<IFilterHelper, FilterHelper>();
        }
    }
}
