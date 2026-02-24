using Model.Db;
using Model.Enums;
using Model.Interfaces.Helpers;
using Model.Models;

namespace ReportService.Helpers.PeriodHelpers
{
    public class Y0PeriodHelper : IPeriodDataHelper
    {
        public PeriodDataTypes DataType { get => PeriodDataTypes.Y0; }
        public (PeriodDataTypes dataType, BasePeriodInfo info) GetPeriodData(TableSKUSub sKUSub)
        {
            var dbPeriodInfo = sKUSub.PeriodData.FirstOrDefault(x => x.PeriodCode == (int)DataType);
            if (dbPeriodInfo == null) return (DataType, new BasePeriodInfo());
            var periodInfo = new BasePeriodInfo
            {
                Price = dbPeriodInfo.Amount / dbPeriodInfo.Units,
                Units = dbPeriodInfo.Units,
                Amount = sKUSub.SKUPRICE * dbPeriodInfo.Units,
            };
            return (DataType, periodInfo);
        }
    }
}
