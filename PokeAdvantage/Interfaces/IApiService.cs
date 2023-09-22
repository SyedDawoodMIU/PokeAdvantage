namespace PokeAdvantage.Interfaces
{
    public interface IApiService
    {
        Task<string> Fetch(string url);



    }
}