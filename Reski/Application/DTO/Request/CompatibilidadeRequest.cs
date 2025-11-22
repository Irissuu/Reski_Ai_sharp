namespace Reski.Application.DTO.Response;

public record CompatibilidadeRequest(
    string Email,
    string Cpf,
    int PontuacaoUsuario,
    int PontuacaoVaga
);