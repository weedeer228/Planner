using Model.Enums;

namespace ReportService.Extensions
{
    public static class PeriodDataTypesExtensions
    {
        public static string GetPeriodDataTypeTitle(this PeriodDataTypes dataType)
        {
            switch (dataType)
            {
                case PeriodDataTypes.Y1:
                    return "Planning Y1";
                case PeriodDataTypes.Y0:
                    return "History Y0";
                default: return dataType.ToString();
            }
        }
    }
}
