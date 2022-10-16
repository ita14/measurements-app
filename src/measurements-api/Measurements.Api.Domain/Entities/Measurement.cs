namespace Measurements.Api.Domain.Entities;

public class Measurement : BaseEntity
{
    public System.DateTime Time { get; set; }
    public string Source { get; set; }
    public double Temperature { get; set; }
    public double Pressure { get; set; }
    public double Humidity { get; set; }
    public double Battery { get; set; }
    public Acceleration Acceleration { get; set; }
}
