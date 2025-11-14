using Reski.Application.DTO.Hateoas;

namespace Reski.Application.DTO.Response;

public class ObjetivoResponse
{
    public int Id { get; set; }

    public string Cargo     { get; set; } = "";
    public string Area      { get; set; } = "";
    public string Descricao { get; set; } = "";
    public string Demanda   { get; set; } = "";

    public List<Link> Links { get; set; } = new();
}