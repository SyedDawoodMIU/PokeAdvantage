namespace PokeAdvantage
{
    public class SingletonHttpClient
    {
        public static HttpClient Instance { get; } = new();
    }
}