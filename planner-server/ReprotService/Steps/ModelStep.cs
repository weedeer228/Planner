using Model.Enums;
using Model.Interfaces;
using Model.Models;
using ReportService.Extensions;

namespace ReportService.Steps
{
    public class ModelStep : BaseReportStep<ReportContext>
    {
        public override Task Process(IReportContext context)
        {
            var reportContext = GetContext(context);
            foreach (var dataType in context.Filter.PeriodCodes)
            {
                var type = (PeriodDataTypes)dataType;
                reportContext.Model.Total.Add(type, GetTotal(reportContext.Model.SkuModels, type));
                reportContext.MetaData.ColumnData.Add(new(
                       Key: type.ToString(),
                       Title: type.GetPeriodDataTypeTitle(),
                       IsEditable: type ==  PeriodDataTypes.Y1 ? true: false, //хардкод такого в реальных проектах быть не должно, но тк тестовый не стал заморачиваться над конфигами
                       Type:RowType.Data
                       )) ;
            }
            return Task.CompletedTask;
        }
        private BasePeriodInfo GetTotal(IEnumerable<BasePlannerSkuModel> info, PeriodDataTypes periodType)
        {
            BasePeriodInfo result = new()
            {
                Units = info.Sum(x => x.SkuTotal[periodType].Units),
                Amount = info.Sum(x => x.SkuTotal[periodType].Amount)
            };
            result.Price = result.Amount / result.Units;
            return result;
        }
    }
}
