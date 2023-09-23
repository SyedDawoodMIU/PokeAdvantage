

using Microsoft.Extensions.DependencyInjection;
using PokeAdvantage.Implementation;
using PokeAdvantage.Implementation.Business;
using PokeAdvantage.Implementation.Data;
using PokeAdvantage.Implementation.ErrorHandler;
using PokeAdvantage.Implementations.Logging;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Interfaces.Logging;
using PokeAdvantage.Models;
using PokeAdvantage.Strategy;

namespace PokeAdvantage
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            ConfigureServices(services);

            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var programEntry = serviceProvider.GetService<ProgramEntry>();
            await programEntry.RunAsync();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
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