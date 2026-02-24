using Model.Enums;

namespace Model.Models
{
    public class ReportMetaData
    {
        public List<FieldColumnData> ColumnData { get;  } = new();
        public List<RowDefinition> RowDefinitions { get; } = new();
    }
    public record FieldColumnData(
      string Key,
      string Title,
      bool IsEditable,
      RowType Type = RowType.Static);

    public record RowDefinition(string Label, string Path);
}
