namespace Measurements.Api.Infrastructure.AppSettings;

#nullable disable

public class CosmosDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
    public List<ContainerInfo> Containers { get; set; }
}
public class ContainerInfo
{
    public string Name { get; set; }
    public string PartitionKey { get; set; }
}
