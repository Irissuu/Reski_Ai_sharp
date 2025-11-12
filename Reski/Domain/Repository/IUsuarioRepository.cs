using Reski.Domain.Entity;

namespace Reski.Domain.Repository;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Usuario usuario, CancellationToken ct = default);
        
    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);
    Task<bool> CpfExistsAsync(string cpf, CancellationToken ct = default);
}