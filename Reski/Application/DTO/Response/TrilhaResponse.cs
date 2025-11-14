using Reski.Application.DTO.Hateoas;

namespace Reski.Application.DTO.Response;

public class TrilhaResponse
{
    public int Id { get; set; }

    public string Status      { get; set; } = "";
    public string Conteudo    { get; set; } = "";
    public string Competencia { get; set; } = "";

    public List<Link> Links { get; set; } = new();
}