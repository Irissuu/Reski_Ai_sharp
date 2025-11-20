using Microsoft.ML.Data;

namespace Reski.Application.ML;

public class ObjetivoTreinamento
{
    [LoadColumn(0)]
    public string Cargo { get; set; } = "";

    [LoadColumn(1)]
    public string Area { get; set; } = "";

    [LoadColumn(2)]
    public string Demanda { get; set; } = "";

    [LoadColumn(3)]
    [ColumnName("Label")]
    public string NivelTrilha { get; set; } = "";
}

public class ObjetivoPredicao
{
    [ColumnName("PredictedLabel")]
    public string NivelTrilha { get; set; } = "";
}