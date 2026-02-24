using Model.Db;
using Model.Interfaces;

namespace Model.Models
{
    public class ReportContext : IReportContext<TableSKU, BasePlannerModel>
    {
        public IEnumerable<TableSKU> Data { get; init; }
        public BasePlannerModel Model { get;} = new();
        public object Result => Model;
        public FilterModel Filter { get; init; }
        public ReportMetaData MetaData { get; } = new();
    }
}
