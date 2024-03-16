using Ardalis.Specification;

namespace Measurements.Api.Domain.Interfaces;

public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class
{
}
