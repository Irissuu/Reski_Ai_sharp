namespace Reski.Application.DTO.Request;

public class UsuarioUpdateRequest
{
    public string Nome  { get; set; } = null!;
    public string Email { get; set; } = null!;
    
    public string? Senha { get; set; }
}