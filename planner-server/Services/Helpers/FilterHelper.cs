using Model.Db;
using Model.Interfaces.Helpers;
using Model.Models;
using System.Linq.Expressions;

namespace Planner.Helpers
{
    public class FilterHelper : IFilterHelper
    {
        public bool GetDbFilter(FilterModel? filter, out Expression<Func<TableSKU, bool>>? expression)
        {
            expression = null;
            if (filter is null) return false;
            var skuSubFilter = GetSkuSubNamesFilter(filter!.SkuSubNames);
            if (skuSubFilter != null)
                expression = skuSubFilter;
            return expression != null;
        }
        private Expression<Func<TableSKU, bool>>? GetSkuSubNamesFilter(string[]? skuSubNames)
        {
            if (skuSubNames is null || skuSubNames.Length == 0) return null;
            return (sku) => sku.SKUSubs.Any(x => skuSubNames.Contains(x.SKUSubName));
        }
    }
}
