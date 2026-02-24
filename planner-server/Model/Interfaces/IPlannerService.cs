using Model.Models;

namespace Model.Interfaces
{
    public interface IPlannerService
    {
        public Task<IReportContext> GetReport(FilterModel? filter = null);
    }
}
