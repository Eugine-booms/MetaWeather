using MetaWeather.Models;

using System.Collections.Generic;
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

        public async Task<WheatherLocation[]> GetLocationByName(string name, CancellationToken cancel = default)
        {
            return await _client
                .GetFromJsonAsync<WheatherLocation[]>($"api/location/search/?query={name}"/*, __jsonOptions*/, cancel)
                .ConfigureAwait(false);
        }

    }
}
