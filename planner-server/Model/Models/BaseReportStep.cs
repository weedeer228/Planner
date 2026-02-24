using Model.Interfaces;

namespace Model.Models
{
    public abstract class BaseReportStep<T> : IReportStep where T : class
    {
        protected T GetContext(IReportContext context)
        {
            return context as T ?? throw new InvalidCastException();
        }
        public abstract Task Process(IReportContext context);
    }
}
