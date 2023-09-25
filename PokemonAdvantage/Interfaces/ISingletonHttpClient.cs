namespace PokemonAdvantage.Interfaces
{
    public interface ISingletonHttpClient
    {
        HttpClient Instance { get; }
    }
}