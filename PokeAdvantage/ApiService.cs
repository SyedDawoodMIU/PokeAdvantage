namespace PokeAdvantage
{
    public class ApiService
    {
        public static async Task<string> Fetch(string url)
        {
            HttpResponseMessage response = await SingletonHttpClient.Instance.GetAsync(url);
            return await response.Content.ReadAsStringAsync();
        }
    }
}