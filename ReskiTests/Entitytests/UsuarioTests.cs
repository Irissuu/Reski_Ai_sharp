using Reski.Domain.Entity;
using Xunit;

namespace ReskiTests;

public class UsuarioTests
{
    [Fact]
    public void Deve_CriarUsuario_QuandoDadosValidos()
    {
        var nome = "Karina Almeida";
        var email = "karinaa@gmail.com";
        var cpf = "31159916659";
        var senha = "Bolindqueijoo";

        var usuario = new Usuario(nome, email, senha, cpf);

        Assert.Equal(nome, usuario.Nome);
        Assert.Equal(email, usuario.Email);
        Assert.Equal(cpf, usuario.Cpf);

        Assert.False(string.IsNullOrWhiteSpace(usuario.SenhaHash));

        Assert.True(usuario.VerificarSenha(senha));
    }
}