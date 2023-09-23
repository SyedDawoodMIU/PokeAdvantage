namespace PokeAdvantage
{
    public interface ISingletonHttpClient
    {
        HttpClient Instance { get; }
    }
}