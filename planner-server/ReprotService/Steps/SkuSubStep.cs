using Microsoft.Extensions.DependencyInjection;
using Model.Db;
using Model.Enums;
using Model.Interfaces;
using Model.Interfaces.Helpers;
using Model.Models;


namespace ReportService.Steps
{
    public class SkuStep : BaseReportStep<ReportContext>
    {
        private readonly IServiceProvider _sp;

        public SkuStep(IServiceProvider sp)
        {
            _sp = sp;
        }
        public override async Task Process(IReportContext context)
        {
            var reportContext = GetContext(context);
            if (reportContext is null) throw new ArgumentNullException(nameof(reportContext));

            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = 4
            };
            await Task.Run(() => Parallel.ForEach(reportContext.Data, options,sku =>
            {
                var model = new BasePlannerSkuModel();
                model.Sku = sku.SKUName;
                model.SubInfos.AddRange(GetSkuSubInfo(sku, context.Filter));
                foreach (var dataType in context.Filter.PeriodCodes)
                {
                    var type = (PeriodDataTypes)dataType;
                    model.SkuTotal.Add(type, GetSkuTotal(model.SubInfos, type));
                }
                reportContext.Model.SkuModels.Add(model);
            }));

            reportContext.Model.SkuModels = reportContext.Model.SkuModels.OrderBy(x => x.Sku).ToList();
        }

        private IEnumerable<BaseSkuSubInfo> GetSkuSubInfo(TableSKU sku, FilterModel filter)
        {
            foreach (var skuSub in sku.SKUSubs)
            {
                if (skuSub.SKUSubName is null && (filter is null || filter.SkipUnnamed))
                    continue;
                var subInfo = new BaseSkuSubInfo()
                {
                    SkuSubName = skuSub.SKUSubName ?? "Name not found",
                    SkuRatio = skuSub.SKURatio
                };
                foreach (var periodInfo in skuSub.PeriodData)
                {
                    if (!filter.PeriodCodes.Contains(periodInfo.PeriodCode))
                        continue;
                    var periodService = _sp.GetRequiredKeyedService<IPeriodDataHelper>((PeriodDataTypes)periodInfo.PeriodCode);
                    if (periodService is not null)
                    {
                        var typedPeriodInfo = periodService.GetPeriodData(skuSub);
                        subInfo.PeriodData.Add(typedPeriodInfo.dataType, typedPeriodInfo.info);
                        subInfo.SkuSubTotal.Add(typedPeriodInfo.dataType, GetSkuSubTotal(skuSub, typedPeriodInfo));
                    }
                }

                yield return subInfo;
            }
        }

        private BasePeriodInfo GetSkuSubTotal(TableSKUSub skuSub, (PeriodDataTypes dataType, BasePeriodInfo info) periodInfo)
        {
            var result = new BasePeriodInfo()
            {
                Units = skuSub.SKURatio * periodInfo.info.Units,
                Amount = periodInfo.info.Amount,
            };
            return result;
        }

        private BasePeriodInfo GetSkuTotal(IEnumerable<BaseSkuSubInfo> info, PeriodDataTypes periodType)
        {
            BasePeriodInfo result = new()
            {
                Units = info.Sum(x => x.SkuSubTotal[periodType].Units),
                Amount = info.Sum(x => x.SkuSubTotal[periodType].Amount)
            };
            result.Price = result.Amount / result.Units;
            return result;
        }
    }
}
