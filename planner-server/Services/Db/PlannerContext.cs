using Microsoft.EntityFrameworkCore;
using Model.Db;

namespace DbService.Db
{
    public class PlannerContext : DbContext
    {
        public PlannerContext(DbContextOptions<PlannerContext> options) : base(options)
        {

        }
        public DbSet<TableSKU> Sku { get; set; }
        public DbSet<TableSKUSub> SkuSub { get; set; }
        //тк таблицы имеют одинаковую роль, функционал и данные, можно использовать 1 таблицу для TableHistoryY0  и TablePlanningY1,
        //используя доп свойство отражающее принадлежность к периоду
        public DbSet<SKUPeriodData> TableHistoryPlanning { get; set; }
    }
}
