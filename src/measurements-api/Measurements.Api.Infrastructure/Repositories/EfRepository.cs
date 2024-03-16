using Ardalis.Specification.EntityFrameworkCore;
using Measurements.Api.Domain.Interfaces;
using Measurements.Api.Infrastructure.Context;

namespace Measurements.Api.Infrastructure.Repositories;

public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class
{
    public EfRepository(MeasurementsDbContext dbContext) : base(dbContext)
    {
    }
}
