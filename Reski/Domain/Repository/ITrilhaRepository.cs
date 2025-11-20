using Reski.Domain.Entity;

namespace Reski.Domain.Repository;

public interface ITrilhaRepository
{
    Task<Trilha?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Trilha trilha, CancellationToken ct = default);
}