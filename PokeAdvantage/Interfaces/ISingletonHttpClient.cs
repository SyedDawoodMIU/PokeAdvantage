namespace PokeAdvantage.Interfaces
{
    public interface ISingletonHttpClient
    {
        HttpClient Instance { get; }
    }
}