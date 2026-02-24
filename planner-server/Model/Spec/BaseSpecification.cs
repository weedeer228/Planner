using Model.Interfaces;
using System.Linq.Expressions;

namespace Model.Spec
{
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Filter { get; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public List<string> IncludeStrings { get; } = new();

        protected BaseSpecification(Expression<Func<T, bool>>? filter)
        {
            if (filter != null)
                Filter = filter;
        }

        protected void AddInclude(Expression<Func<T, object>> includes)
        {
            Includes.Add(includes);
        }

        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }
    }
}
