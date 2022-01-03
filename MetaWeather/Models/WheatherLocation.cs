
using MetaWeather.Service;

using System.Text.Json.Serialization;

namespace MetaWeather.Models
{
    public class WheatherLocation
    {
        [JsonPropertyName("woeid")]
        public int Id { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("location_type")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LocationType Type { get; set; }
        [JsonPropertyName("latt_long")]
        [JsonConverter(typeof(JsonCoordinateConverter))]
        public (double Latitude, double Longitude) Location { get; set; }
        [JsonPropertyName("distance")]
        public int Distance { get; set; }

    }
}
