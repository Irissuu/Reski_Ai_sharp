using Reski.Application.DTO.Hateoas;

namespace Reski.Application.DTO.Response;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Nome  { get; set; } = "";
    public string Email { get; set; } = "";
    public string Cpf   { get; set; } = "";
    public List<Link> Links { get; set; } = new(); 
}
