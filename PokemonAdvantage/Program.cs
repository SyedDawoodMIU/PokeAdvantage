

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokemonAdvantage.Implementation;
using PokemonAdvantage.Implementation.Business;
using PokemonAdvantage.Implementation.Data;
using PokemonAdvantage.Implementation.ErrorHandler;
using PokemonAdvantage.Implementations.Logging;
using PokemonAdvantage.Interfaces;
using PokemonAdvantage.Interfaces.Logging;
using PokemonAdvantage.Models;
using PokemonAdvantage.Strategy;

namespace PokemonAdvantage
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            IConfigurationRoot _configuration = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json").Build();


            ConfigureServices(services, _configuration);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var programEntry = serviceProvider.GetService<ProgramEntry>();
            await programEntry.RunAsync();
            }

        private static void ConfigureServices(IServiceCollection services, IConfigurationRoot _configuration)
        {
            services.Configure<PokemonApiSettings>(c =>
            {
                c.BaseUrl = _configuration.GetSection("PokemonApiSettings:BaseUrl")?.Value;
            });

            services.AddScoped<IObserver, ConsoleObserver>();

            services.AddSingleton<ISingletonHttpClient, SingletonHttpClient>();
            services.AddSingleton<IErrorHandler, ConsoleErrorHandler>();
            services.AddSingleton<ILogger, ConsoleLogger>();

            services.AddTransient<IJsonHelper, NewtonSoftJsonHelper>();
            services.AddTransient<IUserInputManager, ConsoleInputManager>();
            services.AddTransient<IDamageCalculator, BasicDamageCalculator>();
            services.AddTransient<IPokemonStrategy, CalculatePowerStrategy>();
            services.AddTransient<IPokemonDataAdapter, PokemonAdapter>();
            services.AddTransient<IPokemonApiClient, PokemonApiClient>();
            services.AddTransient<IPokemonApiManager, PokemonApiManager>();
            services.AddTransient<IPokemonBusinessLogic, PokemonBusinessLogic>();
            services.AddTransient<IApiService, ApiService>();

            services.AddTransient<ProgramEntry>();
        }
    }
}