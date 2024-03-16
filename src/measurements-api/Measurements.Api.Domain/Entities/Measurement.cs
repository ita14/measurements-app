namespace Measurements.Api.Domain.Entities;

#nullable disable

public class Measurement : BaseEntity
{
    public DateTime Time { get; set; }
    public string Source { get; set; }
    public double Temperature { get; set; }
    public double Pressure { get; set; }
    public double Humidity { get; set; }
    public double Battery { get; set; }
    public Acceleration Acceleration { get; set; }
}
