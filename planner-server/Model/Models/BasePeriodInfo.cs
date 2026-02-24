using Model.Attributes;
using Model.Enums;

namespace Model.Models
{
    public class BasePeriodInfo
    {
        [ReportField]
        public double Units { get; set; } = -1;
        [ReportField]
        public double Price { get; set; } = -1;
        [ReportField]
        public double Amount { get; set; } = -1;
    }

}
