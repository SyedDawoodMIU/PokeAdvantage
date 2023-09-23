﻿

using Microsoft.Extensions.DependencyInjection;
using PokeAdvantage.Implementation;
using PokeAdvantage.Implementation.Business;
using PokeAdvantage.Implementation.Data;
using PokeAdvantage.Implementation.ErrorHandler;
using PokeAdvantage.Interfaces;
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
            services.AddSingleton<IApiService, ApiService>();
            services.AddSingleton<IDamageCalculator, BasicDamageCalculator>();
            services.AddSingleton<IPokemonApiClient, PokemonApiClient>();
            services.AddSingleton<IPokemonStrategy, CalculatePowerStrategy>();
            services.AddSingleton<IUserInputManager, ConsoleInputManager>();
            services.AddSingleton<IPokemonApiManager, PokemonApiManager>();
            services.AddSingleton<IPokemonDataAdapter, PokemonAdapter>();
            services.AddSingleton<IPokemonBusinessLogic, PokemonBusinessLogic>();
            services.AddSingleton<IErrorHandler, ConsoleErrorHandler>();
            services.AddSingleton<IJsonHelper, NewtonSoftJsonHelper>();

            services.AddTransient<ProgramEntry>();
        }
    }
}