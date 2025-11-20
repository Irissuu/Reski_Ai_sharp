using Reski.Domain.Entity;

namespace Reski.Domain.Repository;

public interface IObjetivoRepository
{
    Task<Objetivo?> GetByIdAsync(int id, CancellationToken ct = default);
    Task AddAsync(Objetivo objetivo, CancellationToken ct = default);
}