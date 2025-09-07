using Measurements.Api.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Measurements.Api.Infrastructure.Extensions;

public static class TimescaleExtensions
{
    public static async Task SetupTimescaleDbAsync(this MeasurementsDbContext context, ILogger logger)
    {
        try
        {
            // First, let's check if we're actually connected to TimescaleDB
            try
            {
                await context.Database.ExecuteSqlRawAsync("SELECT 1");
                logger.LogInformation("Successfully connected to database");
            }
            catch (Exception dbEx)
            {
                logger.LogError(dbEx, "Failed to connect to database");
                return;
            }

            // Check if TimescaleDB extension is available
            try
            {
                await context.Database.ExecuteSqlRawAsync("SELECT 1 FROM pg_extension WHERE extname = 'timescaledb' LIMIT 1");
                logger.LogInformation("TimescaleDB extension found");
            }
            catch
            {
                logger.LogWarning("TimescaleDB extension not found. Skipping hypertable setup.");
                return;
            }

            // Convert Measurements table to hypertable
            bool hypertableExists;
            try
            {
                await context.Database.ExecuteSqlRawAsync("SELECT 1 FROM timescaledb_information.hypertables WHERE hypertable_name = 'Measurements' LIMIT 1");
                hypertableExists = true;
                logger.LogInformation("TimescaleDB hypertable already exists");
            }
            catch
            {
                hypertableExists = false;
                logger.LogInformation("Creating TimescaleDB hypertable for Measurements...");
                await context.Database.ExecuteSqlRawAsync(
                    "SELECT create_hypertable('\"Measurements\"', '\"Time\"', chunk_time_interval => INTERVAL '1 day')"
                );
                logger.LogInformation("Hypertable created successfully");
            }

            // Add compression policy for data older than 7 days
            if (hypertableExists)
            {
                try
                {
                    await context.Database.ExecuteSqlRawAsync(@"SELECT 1 FROM timescaledb_information.jobs j
                        WHERE j.proc_name = 'policy_compression'
                        AND j.config->>'hypertable_name' = 'Measurements' LIMIT 1");
                    logger.LogInformation("Compression policy already exists");
                }
                catch
                {
                    logger.LogInformation("Adding compression policy for Measurements...");
                    await context.Database.ExecuteSqlRawAsync(
                        "SELECT add_compression_policy('\"Measurements\"', INTERVAL '7 days')"
                    );
                    logger.LogInformation("Compression policy added successfully");
                }
            }

            // Add retention policy for data older than 1 year
            if (hypertableExists)
            {
                try
                {
                    await context.Database.ExecuteSqlRawAsync(@"SELECT 1 FROM timescaledb_information.jobs j
                        WHERE j.proc_name = 'policy_retention'
                        AND j.config->>'hypertable_name' = 'Measurements' LIMIT 1");
                    logger.LogInformation("Retention policy already exists");
                }
                catch
                {
                    logger.LogInformation("Adding retention policy for Measurements...");
                    await context.Database.ExecuteSqlRawAsync(
                        "SELECT add_retention_policy('\"Measurements\"', INTERVAL '1 year')"
                    );
                    logger.LogInformation("Retention policy added successfully");
                }
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to setup TimescaleDB configuration");
            throw;
        }
    }
}