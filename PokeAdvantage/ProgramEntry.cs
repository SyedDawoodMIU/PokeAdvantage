using PokeAdvantage.Interfaces;
using Newtonsoft.Json;
using PokeAdvantage.DTOs;
using PokeAdvantage.Implementation.Business;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;
using PokeAdvantage.Implementation;
using PokeAdvantage.Strategy;
using PokeAdvantage.@enum;
using PokeAdvantage.Interfaces.Logging;

namespace PokeAdvantage
{
    public class ProgramEntry : ISubject
    {
        private readonly IUserInputManager _inputManager;
        private readonly IPokemonApiManager _apiManager;
        private readonly IPokemonDataAdapter _dataAdapter;
        private readonly IPokemonBusinessLogic _businessLogic;
        private readonly List<IObserver> _observers;

        private readonly IErrorHandler _errorHandler;
        private readonly ILogger _logger;
        private PokemonContext _pokemonContext;
        private List<string> _failedTypes;
        private const int MAX_RETRIES = 3;


        public ProgramEntry(IUserInputManager inputManager,
                            IPokemonApiManager apiManager,
                            IPokemonDataAdapter dataAdapter,
                            IPokemonBusinessLogic businessLogic,
                            IErrorHandler errorHandler,
                            ILogger logger)
        {
            _inputManager = inputManager;
            _apiManager = apiManager;
            _dataAdapter = dataAdapter;
            _businessLogic = businessLogic;
            _errorHandler = errorHandler;
            _logger = logger;
            _observers = new List<IObserver>();
            _pokemonContext = new PokemonContext();
            _failedTypes = new List<string>();
            SetupLogging();
            AttachObserver(new ConsoleObserver());

        }

        public async Task RunAsync()
        {
            _logger.LogInformation("Starting PokeAdvantage");

            string? pokemonName = GetUserInput();

            if (string.IsNullOrEmpty(pokemonName))
            {
                _errorHandler.HandleError(new Exception("The Pokémon name is null or empty"));
                return;
            }

            if (!await FetchAndAdaptPokemonData(pokemonName))
            {
                _errorHandler.HandleError(new Exception("The Pokémon data is null"));
                return;
            }

            foreach (string type in _pokemonContext.Pokemon.Types)
            {
                await FetchAndAdaptTypeData(type);
                ApplyBusinessLogic();
                NotifyObservers();
            }

            _failedTypes.ForEach(type =>
            {
                _errorHandler.HandleError(new Exception($"The type {type} could not be fetched"));
            });

            _logger.LogInformation("Ending PokeAdvantage");
        }

        private void SetupLogging()
        {
            _logger.SetLogLevel(LogLevel.Information);
        }

        private string? GetUserInput()
        {
            return _inputManager.GetPokemonName();
        }

        private async Task<bool> FetchAndAdaptPokemonData(string pokemonName)
        {
            int retries = MAX_RETRIES;
            while (retries > 0)
            {
                PokemonDTO? apiPokemon = await _apiManager.FetchPokemonData(pokemonName);
                if (apiPokemon != null)
                {
                    _pokemonContext.Pokemon = _dataAdapter.AdaptPokemon(apiPokemon);
                    return _pokemonContext.Pokemon != null;
                }

                retries--;
                
            }
            return false;
        }

        private async Task<bool> FetchAndAdaptTypeData(string type)
        {
            int retries = MAX_RETRIES;
            while (retries > 0)
            {
                TypeRelationsDTO typeRelationsDTO = await _apiManager.FetchTypeRelationsAsync(type);
                if (typeRelationsDTO != null)
                {
                    _pokemonContext.TypeRelations = _dataAdapter.AdaptTypeRelations(typeRelationsDTO);
                    _pokemonContext.CurrentType = type;
                    return _pokemonContext.TypeRelations != null;
                }
                retries--;
                
            }

            AddToFailedTypes(type);
            return false;
        }

        public void AddToFailedTypes(string failedType)
        {
            _failedTypes.Add(failedType);
        }

        private void ApplyBusinessLogic()
        {
            _businessLogic.ApplyPokemonStrategy(_pokemonContext);
        }


        public void AttachObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void DetachObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyObservers()
        {
            foreach (var observer in _observers)
            {
                observer.Update(_pokemonContext);
            }
        }
    }


}