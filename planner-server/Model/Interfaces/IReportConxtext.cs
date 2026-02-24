using Model.Db;
using Model.Models;

namespace Model.Interfaces
{
    public interface IReportContext
    {
        public FilterModel Filter { get; }
        public object Result { get; }
        public ReportMetaData MetaData { get; }

    }
    public interface IReportContext<T, M> : IReportContext
    {
        public IEnumerable<T> Data { get; init; }
        public M Model { get; }
    }
}
