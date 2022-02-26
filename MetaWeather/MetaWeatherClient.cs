using MetaWeather.Models;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MetaWeather
{
    public class MetaWeatherClient
    {
        private readonly HttpClient _client;

        public MetaWeatherClient(HttpClient Client) => _client = Client;
        //private static readonly JsonSerializerOptions __jsonOptions = new()
        //{
        //    Converters =
        //    {
        //        new JsonStringEnumConverter() ,
        //        new    JsonCoordinateConverter(),

        //    }
        //};

        public async Task<WheatherLocation[]> GetLocation(string name, CancellationToken cancel = default)
        {
            return await _client
                .GetFromJsonAsync<WheatherLocation[]>($"api/location/search/?query={name}"/*, __jsonOptions*/, cancel)
                .ConfigureAwait(false);
        }
        public async Task<WheatherLocation[]> GetLocation((double Latitude, double Longitude) location, CancellationToken cancel = default)
        {
            var latlong = $"{location.Latitude.ToString(CultureInfo.InvariantCulture)},{location.Longitude.ToString(CultureInfo.InvariantCulture)}";

            return await _client
                .GetFromJsonAsync<WheatherLocation[]>($"api/location/search/?lattlong={latlong}", cancel)
                .ConfigureAwait(false);
        }

        public async Task<LocationInfo> GetInfo(int woeId, CancellationToken cansel = default)
        {
            return await _client
                .GetFromJsonAsync<LocationInfo>($"/api/location/{woeId}/")
                .ConfigureAwait(false);
        }

        public Task<LocationInfo> GetInfo(Location location, CancellationToken cancellation = default) =>
          GetInfo(location.Id, cancellation);

        public async Task<WeatherInfo[]> GetWeather(int woeId, DateTime time, CancellationToken cancellation = default)
        {
            return await _client
                .GetFromJsonAsync<WeatherInfo[]>($"/api/location/{woeId}/{time:yyyy}/{time:MM}/{time:dd}", cancellation)
                .ConfigureAwait(false);
        }
    }
}
                                                                            