using Model.Attributes;
using Model.Enums;

namespace Model.Models
{
    public class BaseSkuSubInfo
    {

        public string SkuSubName { get; set; }
        public double SkuRatio { get; set; }
        public Dictionary< PeriodDataTypes, BasePeriodInfo> PeriodData { get; set; } = new();
        public BasePeriodInfo ContributionGrowth { get; set; } = new();
        public Dictionary<PeriodDataTypes, BasePeriodInfo> SkuSubTotal { get; set; } = new();
    }
}
