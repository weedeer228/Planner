using Model.Enums;
using Model.Interfaces.Helpers;
using Model.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using ReportService.Helpers;
using ReportService.Serices;
using ReportService.Helpers.PeriodHelpers;
using ReportService.Services;
using ReportService.Steps;

namespace ReportService.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddRepositoryHelper(this IServiceCollection services)
        {
            return services.AddScoped<IRepositoryHelper, RepositoryHelper>();
        }
        public static IServiceCollection AddPlannerService(this IServiceCollection services)
        {
            return services.AddTransient<IPlannerService, SkuReportService>();
        }

        public static IServiceCollection AddPeriodDataHelpers(this IServiceCollection services)
        {
            return services.AddKeyedTransient<IPeriodDataHelper, Y0PeriodHelper>(PeriodDataTypes.Y0)
                .AddKeyedTransient<IPeriodDataHelper, Y1PeriodHelper>(PeriodDataTypes.Y1);
        }
        public static IServiceCollection AddDataService(this IServiceCollection services)
        {
            return services.AddTransient<IDataService, DataService>();
        }
        public static IServiceCollection AddReportSteps(this IServiceCollection services)
        {
            return services
                .AddTransient<IReportStep, MetaDataStep>()
                .AddTransient<IReportStep, SkuStep>()
                .AddTransient<IReportStep, ModelStep>()
                .AddTransient<IReportStep, ContributionGrowthTotalStep>()
                .AddTransient<IReportStep, ContributionGrowthSkuStep>()
                .AddTransient<IReportStep, LevelFilterStep>();
        }

    }
}
