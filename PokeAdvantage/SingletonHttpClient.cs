namespace PokeAdvantage
{
    public class SingletonHttpClient : ISingletonHttpClient
    {
        public HttpClient Instance => new();
    }
}