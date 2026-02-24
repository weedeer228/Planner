using DbService.Extensions;
using ReportService.Extensions;
using System.Text.Json.Serialization;

namespace Planner
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            });
            builder.Services.AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddPlannerDbContext()
                .AddPlannerRepositories()
                .AddFilterHelper()
                .AddRepositoryHelper()
                .AddPlannerService()
                .AddPeriodDataHelpers()
                .AddDataService()
                .AddReportSteps()
                .AddCors(options =>
                {
                    options.AddDefaultPolicy(policy =>
                        policy.AllowAnyOrigin()
                              .AllowAnyHeader()
                              .AllowAnyMethod());
                });
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting()
                .UseCors()
                .UseHttpsRedirection();

            app.MapControllers();

            app.Run();

        }
    }
}
