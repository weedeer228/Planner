using Model.Enums;
using Model.Interfaces;
using Model.Models;

namespace ReportService.Steps
{
    public class ContributionGrowthTotalStep : BaseReportStep<ReportContext>
    {
        public override Task Process(IReportContext context)
        {
            var reportContext = GetContext(context);
            var total = reportContext.Model.Total;
            reportContext.Model.ContributionGrowth = new()
            {
                Units = (total[PeriodDataTypes.Y1].Units - total[PeriodDataTypes.Y0].Units) / total[PeriodDataTypes.Y0].Units,
                Price = (total[PeriodDataTypes.Y1].Price - total[PeriodDataTypes.Y0].Price) / total[PeriodDataTypes.Y0].Price,
                Amount = (total[PeriodDataTypes.Y1].Amount - total[PeriodDataTypes.Y0].Amount) / total[PeriodDataTypes.Y0].Amount
            };
            reportContext.MetaData.ColumnData.Add(new(
                  Key: "contributionGrowth",
                  Title: "ContributionGrowth",
                  IsEditable: false,
                  Type: RowType.Data
                  ));
            return Task.CompletedTask;
        }
    }

    public class ContributionGrowthSkuStep : BaseReportStep<ReportContext>
    {
        public override async Task Process(IReportContext context)
        {
            var reportContext = GetContext(context);
            var skuHistory = reportContext.Model.Total[Model.Enums.PeriodDataTypes.Y0];
            await Task.Run(() => Parallel.ForEach(reportContext.Model.SkuModels, skuInfo =>
            {
                var plan = skuInfo.SkuTotal[Model.Enums.PeriodDataTypes.Y1];
                var history = skuInfo.SkuTotal[Model.Enums.PeriodDataTypes.Y0];
                skuInfo.ContributionGrowth.Price = (plan.Price - history.Price) / skuHistory.Price;
                skuInfo.ContributionGrowth.Units = (plan.Units - history.Units) / skuHistory.Units;
                skuInfo.ContributionGrowth.Amount = (plan.Amount - history.Amount) / skuHistory.Amount;
                SetSkuSubContributionGrowth(skuInfo);
            }
            ));
        }

        private void SetSkuSubContributionGrowth(BasePlannerSkuModel model)
        {
            var skuHistory = model.SkuTotal[Model.Enums.PeriodDataTypes.Y0];
            model.SubInfos.ForEach(subInfo =>
            {
                var plan = subInfo.PeriodData[Model.Enums.PeriodDataTypes.Y1];
                var history = subInfo.PeriodData[Model.Enums.PeriodDataTypes.Y0];
                subInfo.ContributionGrowth.Price = (plan.Price - history.Price) / skuHistory.Price;
                subInfo.ContributionGrowth.Units = (plan.Units - history.Units) / skuHistory.Units;
                subInfo.ContributionGrowth.Amount = (plan.Amount - history.Amount) / skuHistory.Amount;
            });
        }
    }
}
