using Newtonsoft.Json;
using PokeAdvantage.Interfaces;

namespace PokeAdvantage.Implementation
{
    public class NewtonSoftJsonHelper : IJsonHelper
    {
        private readonly IErrorHandler _errorHandler;
        public NewtonSoftJsonHelper(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }
        public T Deserialize<T>(string json)
        {
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    _errorHandler.HandleError(new Exception("Error deserializing data, json is null or empty"));
                    return default!;
                }

                T result = JsonConvert.DeserializeObject<T>(json);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    _errorHandler.HandleError(new Exception("Error deserializing Pokemon data from API"));
                    return default!;
                }
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error deserializing Pokemon data from API"));
                return default!;
            }
        }

    }

}