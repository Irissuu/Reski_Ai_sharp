namespace Reski.Domain.Entity;

public class Objetivo
{
    public int Id { get; private set; }

    public string Cargo     { get; private set; } = "";
    public string Area      { get; private set; } = "";
    public string Descricao { get; private set; } = "";
    public string Demanda   { get; private set; } = "";

    private Objetivo() { }

    public Objetivo(string cargo, string area, string descricao, string demanda)
    {
        AtualizarDados(cargo, area, descricao, demanda);
    }

    public void AtualizarDados(string cargo, string area, string descricao, string demanda)
    {
        if (string.IsNullOrWhiteSpace(cargo))
            throw new ArgumentException("Cargo é obrigatório.", nameof(cargo));

        if (string.IsNullOrWhiteSpace(area))
            throw new ArgumentException("Área é obrigatória.", nameof(area));

        if (string.IsNullOrWhiteSpace(descricao))
            throw new ArgumentException("Descrição é obrigatória.", nameof(descricao));

        if (string.IsNullOrWhiteSpace(demanda))
            throw new ArgumentException("Demanda é obrigatória.", nameof(demanda));

        Cargo     = cargo.Trim();
        Area      = area.Trim();
        Descricao = descricao.Trim();
        Demanda   = demanda.Trim();
    }
}