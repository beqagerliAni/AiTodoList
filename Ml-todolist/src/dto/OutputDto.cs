using Microsoft.ML.Data;

namespace Ml_todolist.src.dto
{
    public class OutputDto
    {
        [ColumnName("PredictedLabel")]
        public string prediction { get; set; } = string.Empty;
    }
}
