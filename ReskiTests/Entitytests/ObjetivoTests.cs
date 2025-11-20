using Reski.Domain.Entity;
using Xunit;

namespace ReskiTests;

public class ObjetivoTests
{
    [Fact]
    public void Deve_CriarObjetivo_QuandoDadosValidos()
    {
        var cargo = "Desenvolvedor .NET Jr";
        var area = "Tecnologia";
        var descricao = "Primeiro emprego na área de desenvolvimento";
        var demanda = "Alta"; 

        var objetivo = new Objetivo(cargo, area, descricao, demanda);

        Assert.Equal(cargo, objetivo.Cargo);
        Assert.Equal(area, objetivo.Area);
        Assert.Equal(descricao, objetivo.Descricao);
        Assert.Equal(demanda, objetivo.Demanda);
    }
}