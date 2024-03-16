using Measurements.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Measurements.Api.Infrastructure.Context;

public class MeasurementsDbContext : DbContext
{
    public MeasurementsDbContext(DbContextOptions<MeasurementsDbContext> options) : base(options)
    {
    }

    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<Measurement> Measurements { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Sensor>()
            .ToContainer(nameof(Sensor))
            .HasPartitionKey(x => x.Id)
            .HasNoDiscriminator();

        builder.Entity<Measurement>()
            .ToContainer(nameof(Measurement))
            .HasPartitionKey(x => x.Id)
            .HasNoDiscriminator();
    }
}
