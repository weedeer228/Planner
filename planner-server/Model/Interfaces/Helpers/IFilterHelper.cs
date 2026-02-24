using Model.Db;
using Model.Models;
using System.Linq.Expressions;

namespace Model.Interfaces.Helpers
{
    public interface IFilterHelper
    {
        public bool  GetDbFilter(FilterModel? filterModel, out Expression<Func<TableSKU, bool>>? expression);
    }
}
