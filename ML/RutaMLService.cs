using Microsoft.ML;

namespace CiudApp.ML
{
    public class RutaMLService
    {
        private readonly PredictionEngine<RutaData, RutaPrediction> _predictionEngine;

        public RutaMLService()
        {
            var mlContext = new MLContext();

            var datos = new List<RutaData>
            {
                new() { DistanciaKm = 2, Nivel = "Fácil" },
                new() { DistanciaKm = 3, Nivel = "Fácil" },

                new() { DistanciaKm = 5, Nivel = "Media" },
                new() { DistanciaKm = 6, Nivel = "Media" },

                new() { DistanciaKm = 8, Nivel = "Difícil" },
                new() { DistanciaKm = 10, Nivel = "Difícil" }
            };

            var dataView = mlContext.Data.LoadFromEnumerable(datos);

            var pipeline =
                mlContext.Transforms.Conversion.MapValueToKey("Label", nameof(RutaData.Nivel))
                .Append(mlContext.Transforms.Concatenate("Features", nameof(RutaData.DistanciaKm)))
                .Append(mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

            var model = pipeline.Fit(dataView);

            _predictionEngine =
                mlContext.Model.CreatePredictionEngine<RutaData, RutaPrediction>(model);
        }

        public string PredecirNivel(float distancia)
        {
            var prediction = _predictionEngine.Predict(
                new RutaData
                {
                    DistanciaKm = distancia
                });

            return prediction.PredictedLabel;
        }
    }
}