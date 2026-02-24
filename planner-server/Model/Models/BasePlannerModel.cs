using Model.Enums;

namespace Model.Models
{
    public class BasePlannerSkuModel
    {
        public string Sku { get; set; }
        public List<BaseSkuSubInfo> SubInfos { get; set; } = new();
        public Dictionary<PeriodDataTypes, BasePeriodInfo> SkuTotal { get; set; } = new();
        public BasePeriodInfo ContributionGrowth { get; set; } = new();
    }

    public class BasePlannerModel
    {
        public List<BasePlannerSkuModel> SkuModels { get; set; } = new();
        public Dictionary<PeriodDataTypes, BasePeriodInfo> Total { get; set; } = new();
        public BasePeriodInfo ContributionGrowth { get; set; } = new();

    }
}
