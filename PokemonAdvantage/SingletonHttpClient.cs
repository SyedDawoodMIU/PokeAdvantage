using PokemonAdvantage.Interfaces;

namespace PokemonAdvantage
{
    public class SingletonHttpClient : ISingletonHttpClient
    {
        public HttpClient Instance => new();
    }
}