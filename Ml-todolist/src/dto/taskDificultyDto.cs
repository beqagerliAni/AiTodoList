using Microsoft.ML.Data;

namespace Ml_todolist.src.dto
{
    public class taskDificultyDto
    {
        [LoadColumn(0)]
        public string task { get; set; } = string.Empty;

        [LoadColumn(1)]
        public string dificulty { get; set; } =string.Empty;
    }
}
