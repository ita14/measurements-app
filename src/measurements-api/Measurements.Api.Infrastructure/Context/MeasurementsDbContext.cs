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
        builder.Entity<Sensor>(entity =>
        {
            entity.ToTable("Sensors");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).IsRequired();
        });

        builder.Entity<Measurement>(entity =>
        {
            entity.ToTable("Measurements");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Time).IsRequired();
            entity.Property(e => e.Source).IsRequired();
            entity.HasIndex(e => new { e.Source, e.Time });
            entity.OwnsOne(e => e.Acceleration);
        });
    }
}
