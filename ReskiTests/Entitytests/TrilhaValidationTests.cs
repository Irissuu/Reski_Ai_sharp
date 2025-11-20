using Reski.Domain.Entity;
using Xunit;

namespace ReskiTests.EntityTests;

public class TrilhaValidationTests
{
    [Fact]
    public void Deve_LancarExcecao_QuandoStatusVazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new Trilha("", "conteudo", "competencia"));
    }

    [Fact]
    public void Deve_LancarExcecao_QuandoConteudoVazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new Trilha("Ativa", "", "competencia"));
    }

    [Fact]
    public void Deve_LancarExcecao_QuandoCompetenciaVazia()
    {
        Assert.Throws<ArgumentException>(() =>
            new Trilha("Ativa", "conteudo", ""));
    }
}