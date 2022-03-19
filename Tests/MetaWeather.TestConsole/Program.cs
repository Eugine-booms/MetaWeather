using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Polly;
using Polly.Extensions.Http;

using System;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;

namespace MetaWeather.TestConsole
{
    class Program
    {
        private static IHost __hosting;
        public static IHost Hosting => __hosting ??= CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;
        private static IHostBuilder CreateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServises);

        private static void ConfigureServises(HostBuilderContext host, IServiceCollection services)
        {
            services
                .AddHttpClient<MetaWeatherClient>(client => 
                    client.BaseAddress = new Uri(host.Configuration["MetaWeather"])
                    )
                .SetHandlerLifetime(TimeSpan.FromMinutes(5))
                .AddPolicyHandler(GetRetryPolicy());
        }

        private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            var jitter = new Random();
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .WaitAndRetryAsync(6, retry_attept
                    => TimeSpan.FromSeconds(Math.Pow(2, retry_attept))+
                       TimeSpan.FromMilliseconds(jitter.Next(0, 1000)))
                ;

        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();
            var weather = Services.GetRequiredService<MetaWeatherClient>();

            var moscow = await weather.GetLocation("Moscow");

            //  var locations = await weather.GetLocation(moscow[0].Location);
            //foreach (var item in locations)
            //{
            //    Console.WriteLine(item);
            //}

            var info = await weather.GetInfo(moscow[0]);
            Console.WriteLine(info);
            var weather_info = await weather.GetWeather(moscow[0].Id, DateTime.Now);
            Console.WriteLine(weather_info);
            Console.WriteLine("Завершено");
            Console.ReadLine();
            await host.StopAsync();
        }
    }
}
