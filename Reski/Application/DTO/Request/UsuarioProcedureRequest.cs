namespace Reski.Application.DTO.Request;

public record UsuarioProcedureRequest(
    string Nome,
    string Email,
    string Senha,
    string Cpf
);