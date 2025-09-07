using System.Text.Json;
using System.Text.Json.Serialization;

namespace Measurements.Api.Converters;

public class UtcDateTimeConverter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateTimeString = reader.GetString();
        
        if (DateTime.TryParse(dateTimeString, out var dateTime))
        {
            // If the DateTime doesn't have timezone info, assume it's UTC
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }
            
            // If it's local time, convert to UTC
            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }
            
            // Already UTC
            return dateTime;
        }
        
        throw new JsonException($"Unable to parse DateTime: {dateTimeString}");
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        // Always write DateTime as UTC in ISO 8601 format
        writer.WriteStringValue(value.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
    }
}