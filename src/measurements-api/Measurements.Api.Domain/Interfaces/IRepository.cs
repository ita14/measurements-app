using Ardalis.Specification;

namespace Measurements.Api.Domain.Interfaces;

public interface IRepository<T> : IRepositoryBase<T> where T : class
{
}
