using PokeAdvantage.Interfaces;
using Newtonsoft.Json;
using PokeAdvantage.DTOs;
using PokeAdvantage.Implementation.Business;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;
using PokeAdvantage.Implementation;
using PokeAdvantage.Strategy;

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
        private PokemonContext _pokemonContext;

        public ProgramEntry(IUserInputManager inputManager,
                            IPokemonApiManager apiManager,
                            IPokemonDataAdapter dataAdapter,
                            IPokemonBusinessLogic businessLogic,
                            IErrorHandler errorHandler)
        {
            _inputManager = inputManager;
            _apiManager = apiManager;
            _dataAdapter = dataAdapter;
            _businessLogic = businessLogic;
            _errorHandler = errorHandler;
            _observers = new List<IObserver>();
            _pokemonContext = new PokemonContext();
        }

        public async Task RunAsync()
        {
            AttachObserver(new ConsoleObserver());
            string? pokemonName = _inputManager.GetPokemonName();
            if (!string.IsNullOrEmpty(pokemonName))
            {
                PokemonDTO? apiPokemon = await _apiManager.FetchPokemonData(pokemonName);
                if (apiPokemon != null)
                {
                    _pokemonContext.Pokemon = _dataAdapter.AdaptPokemon(apiPokemon);
                    if (_pokemonContext.Pokemon != null)
                    {
                        foreach (string type in _pokemonContext.Pokemon.Types)
                        {

                            TypeRelationsDTO typeRelationsDTO = await _apiManager.FetchTypeRelationsAsync(type);
                            _pokemonContext.TypeRelations = _dataAdapter.AdaptTypeRelations(typeRelationsDTO);
                            _pokemonContext.CurrentType = type;
                            _businessLogic.ApplyPokemonStrategy(_pokemonContext);
                            NotifyObservers();

                        }
                    }

                }
                else
                {
                    _errorHandler.HandleError(new Exception("The pokemon data is null"));
                }
            }
            else
            {
                _errorHandler.HandleError(new Exception("The pokemon name is null or empty"));
            }
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