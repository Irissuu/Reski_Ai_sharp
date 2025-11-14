namespace Reski.Domain.Entity;

public class Trilha
{
    public int Id { get; private set; }

    public string Status      { get; private set; } = "";
    public string Conteudo    { get; private set; } = "";
    public string Competencia { get; private set; } = "";

    private Trilha() { }

    public Trilha(string status, string conteudo, string competencia)
    {
        AtualizarDados(status, conteudo, competencia);
    }

    public void AtualizarDados(string status, string conteudo, string competencia)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("Status é obrigatório.", nameof(status));

        if (string.IsNullOrWhiteSpace(conteudo))
            throw new ArgumentException("Conteúdo é obrigatório.", nameof(conteudo));

        if (string.IsNullOrWhiteSpace(competencia))
            throw new ArgumentException("Competência é obrigatória.", nameof(competencia));

        Status      = status.Trim();
        Conteudo    = conteudo.Trim();
        Competencia = competencia.Trim();
    }
}