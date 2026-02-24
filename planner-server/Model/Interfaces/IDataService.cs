using Model.Models;

namespace Model.Interfaces
{
    public interface IDataService
    {
        public Task<bool> UpdateY1ById(Guid id, BasePeriodInfo value);
    }
}
