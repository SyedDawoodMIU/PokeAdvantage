using PokeAdvantage.Interfaces;

namespace PokeAdvantage
{
    public class ApiService : IApiService
    {

        private readonly IErrorHandler _errorHandler;
        public ApiService(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;

        }


        public async Task<string> Fetch(string url)
        {
            try
            {
                HttpResponseMessage response = await SingletonHttpClient.Instance.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error fetching data from API"));
                return null;
            }
        }
    }
}
