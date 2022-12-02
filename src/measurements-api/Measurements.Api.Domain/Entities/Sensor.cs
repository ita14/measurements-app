namespace Measurements.Api.Domain.Entities;

#nullable disable

public class Sensor : BaseEntity
{
    public string Identifier { get; set; }
    public string Description { get; set; }
}
