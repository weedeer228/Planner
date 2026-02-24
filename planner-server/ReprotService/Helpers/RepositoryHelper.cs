using Model.Db;
using Model.Enums;
using Model.Interfaces;
using Model.Interfaces.Helpers;
using Model.Models;
using Model.Spec;
using System.Linq.Expressions;

namespace ReportService.Helpers
{

    /// <summary>
    /// Хелпер на случай если бизнесу понадобится другая логика перед получением получения данных
    /// </summary>
    public class RepositoryHelper : IRepositoryHelper
    {
        private readonly IRepository<TableSKU> _sku;
        private readonly IRepository<TableSKUSub> _skuSub;
        private readonly IRepository<SKUPeriodData> _skuPeriodData;
        public RepositoryHelper(IRepository<TableSKU> sku, IRepository<TableSKUSub> skuSub, IRepository<SKUPeriodData> skuPeriodData)
        {
            _sku = sku;
            _skuSub = skuSub;
            _skuPeriodData = skuPeriodData;
        }

        public async Task<IEnumerable<TableSKU>> GetSkuInfo(Expression<Func<TableSKU, bool>>? filterExpr,FilterModel filter)
        {
            var spec = new SKUWithSubsAndPeriodSpec(filter,filterExpr);
            return await _sku.GetByFilterAsync(spec);
        }

        public async  Task<bool> UpdateY1ById(Guid id, BasePeriodInfo value)
        {
            var dbData = await _skuPeriodData.GetByIdAsync(id);
            if (dbData == null || dbData.PeriodCode != (int)PeriodDataTypes.Y1) return false;
            dbData.Units = value.Units;
            dbData.Amount = value.Amount;
            await _skuPeriodData.SaveAsync();
            return true;
        }
    }
}
