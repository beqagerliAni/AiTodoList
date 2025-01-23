using Microsoft.ML;
using Ml_todolist.src.dto;
using Newtonsoft.Json;

namespace Ml_todolist.src.ML
{
    public class MLModel
    {
        private static MLContext _mlContext { get; set; } = new MLContext();
        public MLModel(MLContext mLContext)
        {
            _mlContext = mLContext;
        }

        public static string TrainML()
        {
            string datapath = "src\\data\\taskDificultyData.txt";

            var dataWiev = _mlContext.Data.LoadFromTextFile<taskDificultyDto>(datapath, hasHeader: false, separatorChar: ';');

            var trainSplit = _mlContext.Data.TrainTestSplit(dataWiev, testFraction: 0.2);

            var pipeline = _mlContext.Transforms.Text.FeaturizeText("Features", nameof(taskDificultyDto.task))
                .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(taskDificultyDto.dificulty)))
                .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy("Label", "Features"))
                .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(trainSplit.TrainSet);

            _mlContext.Model.Save(model, dataWiev.Schema, "src\\Models\\predictDificulty.zip");

            return "Model trained";
        }

        public static string predictDificulty(string task)
        {
            var model = _mlContext.Model.Load("src\\Models\\predictDificulty.zip", out var schema);
            var predictionEngine = _mlContext.Model.CreatePredictionEngine<taskDificultyDto, OutputDto>(model);
            var prediction = predictionEngine.Predict(new taskDificultyDto { task = $"{task}" });
            Console.WriteLine(JsonConvert.SerializeObject(prediction));
            return "task Dificulty" + prediction.prediction.ToString() ;

        }
    }
}
