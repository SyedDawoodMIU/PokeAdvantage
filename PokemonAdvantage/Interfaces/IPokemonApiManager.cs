using PokemonAdvantage.DTOs;

namespace PokemonAdvantage.Interfaces
{
    public interface IPokemonApiManager
{
    Task<PokemonDTO?> FetchPokemonData(string pokemonName);
    Task<TypeRelationsDTO> FetchTypeRelationsAsync(string type);
}

}
