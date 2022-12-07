using Ardalis.Specification;
using Measurements.Api.Domain.Entities;

namespace Measurements.Api.Domain.Interfaces.Persistence;

public static class RepositoryExtensions
{
    public static async Task<IEnumerable<T>> SearchItemsAsync<T>(this IRepository<T> repository, CancellationToken ct = default)
        where T : BaseEntity
    {
        return await repository.SearchItemsAsync(new ISpecification<T>[]{}, ct);
    }
}
