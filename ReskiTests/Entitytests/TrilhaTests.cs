using Reski.Domain.Entity;
using Xunit;

namespace ReskiTests;

public class TrilhaTests
{
    [Fact]
    public void Deve_CriarTrilha_QuandoDadosValidos()
    {
        var status = "Ativa";
        var conteudo = "Introdução a APIs REST";
        var competencia = "API REST";

        var trilha = new Trilha(status, conteudo, competencia);

        Assert.Equal(status, trilha.Status);
        Assert.Equal(conteudo, trilha.Conteudo);
        Assert.Equal(competencia, trilha.Competencia);
    }
}