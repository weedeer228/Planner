using Model.Interfaces;
using Model.Interfaces.Helpers;
using Model.Models;

namespace ReportService.Serices
{
    public class SkuReportService : IPlannerService
    {
        private readonly IRepositoryHelper _repository;
        private readonly IFilterHelper _filterHelper;

        private readonly IEnumerable<IReportStep> _steps;

        public SkuReportService(IFilterHelper filterHelper, IEnumerable<IReportStep> steps, IRepositoryHelper repository)
        {
            _repository = repository;
            _filterHelper = filterHelper;
            _steps = steps;
        }

        public async Task<IReportContext> GetReport(FilterModel? filter)
        {
            _filterHelper.GetDbFilter(filter, out var filterExpression);
            if (filter is null)
                filter = new();
            var info = await _repository.GetSkuInfo(filterExpression,filter);

            var context = new ReportContext()
            {
                Filter = filter,
                Data = info
            };
            foreach (var step in _steps)
            {
                if (step is null) continue;
                await step.Process(context);
            }

            return context;
        }

    }
}

