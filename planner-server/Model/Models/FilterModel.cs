using Model.Enums;

namespace Model.Models
{
    public class FilterModel
    {
        public DetailLevel Level { get; set; } = DetailLevel.SkuSub;
        public string[]? SkuSubNames { get; set; }

        public bool SkipUnnamed { get; set; } = true;

        public int[] PeriodCodes { get; set; } = { 0, 1};

    }
}
