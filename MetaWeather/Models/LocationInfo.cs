using MetaWeather.Service;

using System;
using System.Text.Json.Serialization;

namespace MetaWeather.Models
{

    public class LocationInfo  : Location
    {
        [JsonPropertyName("consolidated_weather")]
        public Consolidated_Weather[] Weather { get; set; }
        [JsonPropertyName("time")]
        public DateTime Time { get; set; }
        [JsonPropertyName("sun_rise")]
        public DateTime SunRiseTime { get; set; }
        [JsonPropertyName("sun_set")]
        public DateTime SunSetTime { get; set; }
        [JsonPropertyName("timezone_name")]
        public string Timezone_name { get; set; }
        [JsonPropertyName("parent")]
        public Location Parent { get; set; }
        [JsonPropertyName("sources")]
        public Source[] Sources { get; set; }
        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }
    }

    public class Consolidated_Weather   
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("weather_state_name")]
        public string Weather_state_name { get; set; }
        [JsonPropertyName("weather_state_abbr")]
        public string Weather_state_abbr { get; set; }
        [JsonPropertyName("wind_direction_compass")]
        public string Wind_direction_compass { get; set; }
        [JsonPropertyName("created")]
        public DateTime Created { get; set; }
        [JsonPropertyName("applicable_date")]
        public DateTime Applicable_date { get; set; }
        [JsonPropertyName("min_temp")]
        public double Min_temp { get; set; }
        [JsonPropertyName("max_temp")]
        public double Max_temp { get; set; }
        [JsonPropertyName("the_temp")]
        public double The_temp { get; set; }
        [JsonPropertyName("wind_speed")]
        public double Wind_speed { get; set; }
        [JsonPropertyName("wind_direction")]
        public double Wind_direction { get; set; }
        [JsonPropertyName("air_pressure")]
        public double Air_pressure { get; set; }
        [JsonPropertyName("humidity")]
        public int Humidity { get; set; }
        [JsonPropertyName("visibility")]
        public double Visibility { get; set; }
        [JsonPropertyName("predictability")]
        public int Predictability { get; set; }

    }

    public class Source
    {
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("slug")]
        public string Slug { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("crawl_rate")]
        public int Crawl_rate { get; set; }
    }
    }
}
