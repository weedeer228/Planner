using Model.Db;
using Model.Models;
using System.Linq.Expressions;

namespace Model.Interfaces.Helpers
{
    public interface IRepositoryHelper
    {
        public Task<IEnumerable<TableSKU>> GetSkuInfo(Expression<Func<TableSKU, bool>>? filterExpr, FilterModel filter);
        public Task<bool> UpdateY1ById(Guid id, BasePeriodInfo value);
    }
}
