
using DbService.Db;
using DbService.Extensions;
using Microsoft.EntityFrameworkCore;
using Model.Interfaces;

namespace Services.Db
{
    /// <summary>
    /// Используем репозиторий в  связке с Ef для тестирования и потенциальной смены бд
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly PlannerContext _context;
        public EFRepository(PlannerContext context) { _context = context; }

        public async Task<IEnumerable<T>> GetAllAsync() => await _context.Set<T>().AsQueryable().AsNoTracking().ToListAsync();
        public async Task<T?> GetByIdAsync(Guid id) => await _context.Set<T>().FindAsync(id);

        public async Task AddAsync(T entity) => await _context.Set<T>().AddAsync(entity);
        public void Update(T entity) => _context.Set<T>().Update(entity);
        public void Delete(T entity) => _context.Set<T>().Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<IEnumerable<T>> GetByFilterAsync(ISpecification<T>? spec)
        {
            if (spec == null) return await GetAllAsync();
            return await _context.Set<T>().AsQueryable().ApplySpecification(spec).AsNoTracking().ToListAsync();
        }

    }

}
