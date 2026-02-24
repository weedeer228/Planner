using Model.Db;
using Model.Models;
using System.Linq.Expressions;

namespace Model.Spec
{
    public class SKUWithSubsAndPeriodSpec : BaseSpecification<TableSKU>
    {
        public SKUWithSubsAndPeriodSpec(FilterModel? filter,Expression<Func<TableSKU, bool>>? filterExpr = null) : base(filterExpr)
        {
            if(filter is null || filter.SkuSubNames == null)
                AddInclude(x => x.SKUSubs);
            else
                AddInclude(x => x.SKUSubs.Where(s=> filter.SkuSubNames.Contains(s.SKUSubName)));
            AddInclude("SKUSubs.PeriodData");
        }
    }
}
