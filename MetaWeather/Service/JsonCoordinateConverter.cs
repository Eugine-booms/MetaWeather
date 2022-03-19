
using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MetaWeather.Service
{
    public class JsonCoordinateConverter : JsonConverter<(double Latitude, double Longitude)>
    {
        public override (double Latitude, double Longitude) Read(ref Utf8JsonReader reader,
                                                                 Type typeToConvert,
                                                                 JsonSerializerOptions options) =>

            reader.GetString() is not { Length: >= 3 } str
                || str.Split(',') is not { Length: 2 } component
                || !double.TryParse(component[0], NumberStyles.Any, CultureInfo.InvariantCulture, out var lat)
                || !double.TryParse(component[1], NumberStyles.Any, CultureInfo.InvariantCulture, out var lon)
                ? (double.NaN, double.NaN)
                : (lat, lon);



        public override void Write(Utf8JsonWriter writer,
                                   (double Latitude, double Longitude) value,
                                   JsonSerializerOptions options) =>
            writer.WriteStringValue($"{value.Latitude.ToString(provider: CultureInfo.InvariantCulture)},{value.Longitude.ToString(provider: CultureInfo.InvariantCulture)}");
    }
}
