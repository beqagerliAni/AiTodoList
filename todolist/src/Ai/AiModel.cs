using Microsoft.ML;
using todolist.src.Ai.source.@interface;

namespace AiModel.src.Ai
{
    public class AiModel
    {
        public static string MLModel(SourceType task)
        {
            var context = new MLContext();
            // prepare ai pipline
            var pipeline = context.Transforms.Text.FeaturizeText("Features", "Task")
                .Append((context.Transforms.Conversion.MapValueToKey("Label")))
                .Append(context.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(context.Transforms.Conversion.MapKeyToValue("PredictedLabel"));
            //prepare data
            var data = context.Data.LoadFromTextFile<SourceType>("src\\Ai\\source\\source.txt", hasHeader: true, separatorChar: ',');
            var trainTestSplit = context.Data.TrainTestSplit(data, testFraction: 0.2);
            var model = pipeline.Fit(trainTestSplit.TrainSet);
            //predict
            
            var predictionEngine = context.Model.CreatePredictionEngine<SourceType, Prediction>(model);
            
            var prediction = predictionEngine.Predict(task);

            return $"{prediction.PredictedLabel}";
        }
    }
}
