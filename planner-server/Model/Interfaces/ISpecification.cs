using System.Linq.Expressions;

namespace Model.Interfaces
{
    public interface ISpecification<T>
    {
        public Expression<Func<T, bool>> Filter { get; }
        public List<Expression<Func<T, object>>> Includes { get; }
       public  List<string> IncludeStrings { get; }
    }
}
