using Ardalis.Specification;

namespace Measurements.Api.Domain.Interfaces.Persistence;

public interface IRepository<T>
{
    public Task<T> AddItemAsync(T item, CancellationToken ct = default);

    public Task<T> UpdateItemAsync(string id, T item, CancellationToken ct = default);

    public Task<T?> GetItemAsync(string id, CancellationToken ct = default);

    public Task DeleteItemAsync(string id, CancellationToken ct = default);

    public Task<IEnumerable<T>> SearchItemsAsync(ISpecification<T>? specification = null, CancellationToken ct = default);

    public Task BatchInsertAsync(IEnumerable<T> items, CancellationToken ct = default);

    public Task<int> GetCountAsync(CancellationToken ct = default);
}
