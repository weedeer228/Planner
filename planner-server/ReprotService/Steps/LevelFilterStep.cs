using Model.Enums;
using Model.Interfaces;
using Model.Models;

namespace ReportService.Steps
{
    public class LevelFilterStep : BaseReportStep<ReportContext>
    {
        public override Task Process(IReportContext context)
        {
            if (context.Filter.Level == DetailLevel.SkuSub)
                return Task.CompletedTask;
            
            var reportContext = GetContext(context);
            reportContext.Model.SkuModels.ForEach(x => x.SubInfos.Clear());
            if(context.Filter.Level == DetailLevel.Sku) return Task.CompletedTask;
            reportContext.Model.SkuModels.Clear();
            return Task.CompletedTask;
        }
    }
}
