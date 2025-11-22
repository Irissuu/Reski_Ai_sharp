using Microsoft.ML;

namespace Reski.Application.ML;

public class RecomendacaoTrilha : IRecomendacaoTrilha
{
    private readonly MLContext _mlContext;
    private readonly ITransformer _modelo;
    private readonly PredictionEngine<ObjetivoTreinamento, ObjetivoPredicao> _engine;

    public RecomendacaoTrilha()
    {
        _mlContext = new MLContext();
        
        var dadosTreino = new List<ObjetivoTreinamento>
        {
            new() { Cargo = "Estagiario Desenvolvedor", Area = "Tecnologia", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Desenvolvedor .NET Jr", Area = "Tecnologia", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Desenvolvedor .NET Jr", Area = "Tecnologia", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Desenvolvedor .NET Pleno", Area = "Tecnologia", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Desenvolvedor .NET Pleno", Area = "Tecnologia", Demanda = "Alta", NivelTrilha = "Avancado" },
            new() { Cargo = "Desenvolvedor .NET Senior", Area = "Tecnologia", Demanda = "Alta", NivelTrilha = "Avancado" },

            new() { Cargo = "Dev Mobile Jr", Area = "Tecnologia", Demanda = "Media", NivelTrilha = "Basico" },
            new() { Cargo = "Dev Mobile Pleno", Area = "Tecnologia", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Dev Mobile Senior", Area = "Tecnologia", Demanda = "Media", NivelTrilha = "Avancado" },
            
            new() { Cargo = "Estagiario Dados", Area = "Dados", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Dados Jr", Area = "Dados", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Dados Jr", Area = "Dados", Demanda = "Media", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Dados Pleno", Area = "Dados", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Cientista de Dados", Area = "Dados", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Cientista de Dados", Area = "Dados", Demanda = "Alta", NivelTrilha = "Avancado" },
            
            new() { Cargo = "Estagiario Produto", Area = "Produto", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Product Owner Jr", Area = "Produto", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Product Owner Pleno", Area = "Produto", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Product Manager Senior", Area = "Produto", Demanda = "Alta", NivelTrilha = "Avancado" },
            
            new() { Cargo = "Analista de Negocios Jr", Area = "Negocio", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Negocios Pleno", Area = "Negocio", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Gerente de Negocios", Area = "Negocio", Demanda = "Alta", NivelTrilha = "Avancado" },
            
            new() { Cargo = "Estagiario UX", Area = "UX", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "UX Designer Jr", Area = "UX", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "UX Designer Jr", Area = "UX", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "UX Designer Pleno", Area = "UX", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "UX Lead", Area = "UX", Demanda = "Alta", NivelTrilha = "Avancado" },
            
            new() { Cargo = "Estagiario Marketing", Area = "Marketing", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Assistente de Marketing", Area = "Marketing", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Marketing Jr", Area = "Marketing", Demanda = "Media", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Marketing Pleno", Area = "Marketing", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Coordenador de Marketing", Area = "Marketing", Demanda = "Alta", NivelTrilha = "Avancado" },
            
            new() { Cargo = "Perito Forense Digital", Area = "Ciberseguranca", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Perito Forense Digital", Area = "Ciberseguranca", Demanda = "Alta", NivelTrilha = "Avancado" },

            new() { Cargo = "Analista de Seguranca Jr", Area = "Ciberseguranca", Demanda = "Baixa", NivelTrilha = "Basico" },
            new() { Cargo = "Analista de Seguranca Pleno", Area = "Ciberseguranca", Demanda = "Media", NivelTrilha = "Intermediario" },
            new() { Cargo = "Especialista em Seguranca", Area = "Ciberseguranca", Demanda = "Alta", NivelTrilha = "Avancado" },
        };

        var dataView = _mlContext.Data.LoadFromEnumerable(dadosTreino);

        var pipeline =
            _mlContext.Transforms.Categorical.OneHotEncoding("CargoEncoded", nameof(ObjetivoTreinamento.Cargo))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("AreaEncoded", nameof(ObjetivoTreinamento.Area)))
            .Append(_mlContext.Transforms.Categorical.OneHotEncoding("DemandaEncoded", nameof(ObjetivoTreinamento.Demanda)))
            .Append(_mlContext.Transforms.Concatenate("Features", "CargoEncoded", "AreaEncoded", "DemandaEncoded"))
            .Append(_mlContext.Transforms.Conversion.MapValueToKey("Label"))
            .Append(_mlContext.MulticlassClassification.Trainers.SdcaMaximumEntropy())
            .Append(_mlContext.Transforms.Conversion.MapKeyToValue("PredictedLabel"));

        _modelo = pipeline.Fit(dataView);

        _engine = _mlContext.Model.CreatePredictionEngine<ObjetivoTreinamento, ObjetivoPredicao>(_modelo);
    }
    
    private string NormalizarDemanda(string demanda)
    {
        if (string.IsNullOrWhiteSpace(demanda))
            return "Baixa";

        demanda = demanda.ToLowerInvariant();

        if (demanda.Contains("alta"))
            return "Alta";
        if (demanda.Contains("méd") || demanda.Contains("med"))
            return "Media";

        return "Baixa";
    }

    public string RecomendarNivel(string cargo, string area, string demanda)
    {
        var input = new ObjetivoTreinamento
        {
            Cargo = cargo,
            Area = area,
            Demanda = NormalizarDemanda(demanda)
        };

        var pred = _engine.Predict(input);
        return pred.NivelTrilha;
    }
}
