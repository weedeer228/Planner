namespace Model.Db
{
    public class TableSKUSub
    {
        
        public Guid Id { get; set; }
        public Guid SKUId { get; set; }
        public string? SKUSubName { get; set; }
        public double SKUPRICE { get; set; }
        public double SKURatio { get; set; }

        public TableSKU SKU { get; set; }
        public List<SKUPeriodData> PeriodData{ get; set; }
    } 
}
