namespace Measurements.Api.Domain.Entities;

public class Sensor : BaseEntity
{
    public string Identifier { get; set; }
    public string Description { get; set; }
}
