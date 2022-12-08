using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Measurements.Api.Domain.Entities;
using Measurements.Api.Domain.Interfaces.Persistence;
using Measurements.Api.Infrastructure.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Infrastructure.CosmosDbData.Repositories;

public abstract class CosmosDbRepository<T> : IRepository<T>, IContainerContext<T> where T : BaseEntity
{
    private readonly Container _container;
    private readonly ILogger _logger;

    protected CosmosDbRepository(ICosmosDbContainerFactory cosmosDbContainerFactory, ILogger logger,
        string containerName)
    {
        ArgumentNullException.ThrowIfNull(cosmosDbContainerFactory);

        _container = cosmosDbContainerFactory.GetContainer(containerName).Container;
        _logger = logger;
    }

    public abstract string  GenerateId(T entity);
    public abstract PartitionKey ResolvePartitionKey(string entityId);

    public async Task<T> AddItemAsync(T item, CancellationToken ct)
    {
        item.Id = GenerateId(item);
        var response = await _container.CreateItemAsync<T>(item, ResolvePartitionKey(item.Id), cancellationToken: ct);
        return response.Resource;
    }

    public async Task<T> UpdateItemAsync(string id, T item, CancellationToken ct)
    {
        _logger.LogInformation("Repository updating item: {Id}", id);

        var response = await _container.ReplaceItemAsync(item, id, ResolvePartitionKey(id), cancellationToken: ct);
        return response.Resource;
    }

    public async Task<T?> GetItemAsync(string id, CancellationToken ct)
    {
        _logger.LogInformation("Repository get item by id: {Id}", id);

        try
        {
            ItemResponse<T> response = await _container.ReadItemAsync<T>(id, ResolvePartitionKey(id), cancellationToken: ct);
            return response.Resource;
        }
        catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }
    }

    public async Task DeleteItemAsync(string id, CancellationToken ct)
    {
        _logger.LogInformation("Repository delete item: {Id}", id);

        await _container.DeleteItemAsync<T>(id, ResolvePartitionKey(id), cancellationToken: ct);
    }

    public async Task<IEnumerable<T>> SearchItemsAsync(ISpecification<T>? specification, CancellationToken ct)
    {
        var queryable = ApplySpecification(specification);

        using var iterator = queryable.ToFeedIterator<T>();

        var results = new List<T>();
        while (iterator.HasMoreResults)
        {
            var response = await iterator.ReadNextAsync(cancellationToken: ct);

            results.AddRange(response.ToList());
        }

        return results;
    }

    /// <summary>
    /// Insert multiple items. This takes advantage of
    /// <a href="https://devblogs.microsoft.com/cosmosdb/introducing-bulk-support-in-the-net-sdk">cosmos db bulk insert.</a>
    /// </summary>
    /// <param name="items"></param>
    /// <param name="ct"></param>
    public async Task BatchInsertAsync(IEnumerable<T> items, CancellationToken ct = default)
    {
        _logger.LogInformation("Starting to batch insert [{Type}] items...", typeof(T));

        var tasks = items.Select(item => AddItemAsync(item, ct));

        await Task.WhenAll(tasks);
    }

    public async Task<int> GetCountAsync(CancellationToken ct = default)
    {
        using var iterator = _container.GetItemQueryIterator<int>("SELECT VALUE COUNT(1) FROM c");

        var response = await iterator.ReadNextAsync(ct);

        return response.First();
    }

    public async Task<int> GetItemsCountAsync(ISpecification<T> specification, CancellationToken ct)
    {
        var queryable = ApplySpecification(specification, true);
        var response = await queryable.CountAsync(ct);
        return response;
    }

    private IQueryable<T>? ApplySpecification(ISpecification<T>? specification, bool evaluateCriteriaOnly = false)
    {
        if (specification is null)
        {
            return _container.GetItemLinqQueryable<T>();
        }

        var evaluator = new SpecificationEvaluator();
        return evaluator.GetQuery(_container.GetItemLinqQueryable<T>(), specification, evaluateCriteriaOnly);
    }
}
