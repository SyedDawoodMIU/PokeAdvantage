namespace PokeAdvantage.Interfaces
{
    public interface IJsonHelper
    {
        T Deserialize<T>(string json);

    }
    
}