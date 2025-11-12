namespace Reski.Application.Service;

public interface ITokenService
{
    string GenerateToken(Domain.Entity.Usuario user, DateTime expiresAt);
}