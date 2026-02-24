namespace Model.Db
{
    public class SKUPeriodData
    {
        public Guid Id { get; set; }
        public Guid SKUSubId { get; set; }
        public double Units { get; set; }
        public double Amount { get; set; }
        public int PeriodCode { get; set; }

        public TableSKUSub SKUSub { get; set; }
    }
}
