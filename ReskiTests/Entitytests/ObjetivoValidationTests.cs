using Reski.Domain.Entity;
using Xunit;

namespace ReskiTests.EntityTests;

public class ObjetivoValidationTests
{
    [Fact]
    public void Deve_LancarExcecao_QuandoCargoVazio()
    {
        Assert.Throws<ArgumentException>(() =>
            new Objetivo("", "Tecnologia", "descricao", "Alta"));
    }

    [Fact]
    public void Deve_LancarExcecao_QuandoAreaVazia()
    {
        Assert.Throws<ArgumentException>(() =>
            new Objetivo("Dev", "", "descricao", "Alta"));
    }

    [Fact]
    public void Deve_LancarExcecao_QuandoDemandaVazia()
    {
        Assert.Throws<ArgumentException>(() =>
            new Objetivo("Dev", "Tecnologia", "descricao", ""));
    }
}