using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Threading.Tasks;

namespace MetaWeather.TestConsole
{
    class Program
    {
        private static IHost __hosting;
        public static IHost Hosting => __hosting ??= CheateHostBuilder(Environment.GetCommandLineArgs()).Build();

        public static IServiceProvider Services => Hosting.Services;
        private static IHostBuilder CheateHostBuilder(string[] args) => Host
            .CreateDefaultBuilder(args)
            .ConfigureServices(ConfigureServises);

        private static void ConfigureServises(HostBuilderContext host, IServiceCollection services)
        {
            services.AddHttpClient<MetaWeatherClient>(client => client.BaseAddress = new Uri(host.Configuration["MetaWeather"]));

        }

        static async Task Main(string[] args)
        {
            using var host = Hosting;
            await host.StartAsync();
            var weather = Services.GetRequiredService<MetaWeatherClient>();

            var moscow = await weather.GetLocation("Moscow");

            var locations = await weather.GetLocation(moscow[0].Location);

            foreach (var item in locations)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("Завершено");
            Console.ReadLine();
            await host.StopAsync();
        }
    }
}
