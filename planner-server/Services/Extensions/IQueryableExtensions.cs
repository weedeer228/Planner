using Model.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace DbService.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplySpecification<T>(this IQueryable<T> query, ISpecification<T> spec) where T : class
        {
            if (spec.Filter != null)
                query = query.Where(spec.Filter);
            if (spec.Includes != null)
                query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));
            if (spec.IncludeStrings != null)
                query = spec.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
