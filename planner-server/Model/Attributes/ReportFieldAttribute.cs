namespace Model.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ReportFieldAttribute : Attribute
    {
        public string Title { get; }
        public bool IsEditable { get; set; } = false;
        public ReportFieldAttribute(string title) => Title = title;
        public ReportFieldAttribute() {
        }
    }
}
