using Model.Interfaces;
using Model.Interfaces.Helpers;
using Model.Models;

namespace ReportService.Services
{
    public class DataService : IDataService
    {
        private readonly IRepositoryHelper _repository;

        public DataService(IRepositoryHelper repository)
        {
            _repository = repository;
        }
        public async Task<bool> UpdateY1ById(Guid id, BasePeriodInfo value)
        {
            return await _repository.UpdateY1ById(id, value);
        }
    }
}
