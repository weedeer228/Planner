using Model.Db;
using Model.Enums;
using Model.Models;

namespace Model.Interfaces.Helpers
{
    public interface IPeriodDataHelper
    {
        public PeriodDataTypes DataType { get; }
        public (PeriodDataTypes dataType, BasePeriodInfo info) GetPeriodData(TableSKUSub sKUSub);
    }
}
