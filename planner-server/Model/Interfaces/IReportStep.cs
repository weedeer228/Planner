using Model.Models;

namespace Model.Interfaces
{
    public interface IReportStep
    {
        public Task Process(IReportContext context);
    }
}
