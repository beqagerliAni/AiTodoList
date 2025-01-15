using Microsoft.ML.Data;

namespace todolist.src.Ai.source.@interface
{
    public class SourceType
    {
        [LoadColumn(0)]
        public required string Task { get; set; }
        [LoadColumn(1)]
        public string? Label { get; set; }
    }
}
