using Reski.Domain.Entity;
using Xunit;

namespace ReskiTests.EntityTests;

public class UsuarioSenhaTests
{
    [Fact]
    public void Deve_RetornarFalse_QuandoSenhaIncorreta()
    {
        var usuario = new Usuario(
            nome: "Maria Eduarda Lima",
            email: "mariaapinha@gmail.com",
            senha: "mamonasassassinas",
            cpf: "06085488637");

        var resultado = usuario.VerificarSenha("SenhaErrada!");

        Assert.False(resultado);
    }
}