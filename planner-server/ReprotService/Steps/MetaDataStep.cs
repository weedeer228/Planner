using Model.Attributes;
using Model.Enums;
using Model.Interfaces;
using Model.Models;
using System.Reflection;
using System.Text.Json;

namespace ReportService.Steps
{
    public class MetaDataStep : BaseReportStep<ReportContext>
    {
        public override Task Process(IReportContext context)
        {
            var reportContext = GetContext(context);
            reportContext.MetaData.ColumnData.AddRange(GetStaticColumnData(reportContext.Model.GetType()));
            reportContext.MetaData.RowDefinitions.AddRange(GetRowDefinitions());
            return Task.CompletedTask;
        }

        private IEnumerable<FieldColumnData> GetStaticColumnData(Type type, string prefix = "")
        {
            return new FieldColumnData[]
            {
              new("sku", "SKU",false),
              new("skuSubName", "Sub SKU", false) ,
              new("rowTitle","Тип значения", false, RowType.RowLabel)
            };

        }

        private List<RowDefinition> GetRowDefinitions()
        {
            var props = typeof(BasePeriodInfo).GetProperties();

            return props.Select(p => new RowDefinition(
                p.GetCustomAttribute<ReportFieldAttribute>()?.Title ?? p.Name.ToLower(),
                $"{JsonNamingPolicy.CamelCase.ConvertName(p.Name)}"
            )).ToList();
        }

    }
}
