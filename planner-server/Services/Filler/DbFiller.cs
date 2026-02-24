using DbService.Db;
using Microsoft.EntityFrameworkCore;
using Model.Db;
using Model.Enums;
using Model.Interfaces;
using Services.Db;

namespace DbService.Filler
{
    /// <summary>
    /// класс для заполнения бд данными
    /// </summary>
    public class DbFiller
    {
        private List<string> skuNames = new List<string>()
        {"Мука","крупы","макароны","хлеб","Мясо","Рыба",
        "Бакалея","Сахар","Молоко","чай","соки","Яйца",
        "Печенье","шоколад","торты","хлеб","консервы","Вода"
        };

        private readonly PlannerContext _context;
        private readonly IRepository<TableSKU> _sku;
        private readonly IRepository<TableSKUSub> _skuSub;
        private readonly IRepository<SKUPeriodData> _skuPeriodData;
        public DbFiller(PlannerContext context)
        {
            _context = context;
            _sku = new EFRepository<TableSKU>(_context);
            _skuSub = new EFRepository<TableSKUSub>(_context);
            _skuPeriodData = new EFRepository<SKUPeriodData>(_context);
        }

        public async Task FillDb(int count, bool clearDb)
        {
            if (clearDb)
            {
                await _context.Database.EnsureDeletedAsync();
                await _context.Database.EnsureCreatedAsync();
            }
            if (count > skuNames.Count)
                count = skuNames.Count;
            var rand = new Random();
            var ids = new HashSet<Guid>();
            for (int i = 0; i < count; i++)
            {
                var id = Guid.NewGuid();
                ids.Add(id);
                var index = rand.Next(0, skuNames.Count - 1);
                await _sku.AddAsync(new()
                {
                    Id = id,
                    SKUName = skuNames[index]
                });

                var subCount = rand.Next(1, 4);

                for (int j = 0; j < subCount; j++)
                {
                    var skuSubId = Guid.NewGuid();
                    await _skuSub.AddAsync(new()
                    {
                        Id = skuSubId,
                        SKUId = id,
                        SKUPRICE = rand.Next(50, 15000),
                        SKURatio = rand.NextDouble() + 1,
                        SKUSubName = $"{skuNames[index]}_{j}"
                    });
                    await _skuPeriodData.AddAsync(new()
                    {
                        Id  = Guid.NewGuid(),
                        SKUSubId = skuSubId,
                        Amount = rand.NextDouble() * 100,
                        Units = rand.Next(1, 200),
                        PeriodCode = (int)PeriodDataTypes.Y0
                    });
                    await _skuPeriodData.AddAsync(new()
                    {
                        Id  = Guid.NewGuid(),
                        SKUSubId = skuSubId,
                        Amount = rand.NextDouble() * 100,
                        Units = rand.Next(1, 200),
                        PeriodCode = (int)PeriodDataTypes.Y1
                    });
                }

                skuNames.RemoveAt(index);
            }

           await _context.SaveChangesAsync();
            
        }




    }
}
