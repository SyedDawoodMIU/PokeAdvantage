using PokeAdvantage.Interfaces;

namespace PokeAdvantage
{
    public class ApiService : IApiService
    {
        public async Task<string> Fetch(string url)
        {
            HttpResponseMessage response = await SingletonHttpClient.Instance.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
