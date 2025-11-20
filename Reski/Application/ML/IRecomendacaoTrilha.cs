namespace Reski.Application.ML;

public interface IRecomendacaoTrilha
{
    string RecomendarNivel(string cargo, string area, string demanda);
}