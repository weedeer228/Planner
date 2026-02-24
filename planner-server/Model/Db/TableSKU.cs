namespace Model.Db
{
    public class TableSKU
    {
        public Guid Id { get; set; }
        public string SKUName { get; set; }
        public List<TableSKUSub> SKUSubs { get; set; } = new();
    }
}
