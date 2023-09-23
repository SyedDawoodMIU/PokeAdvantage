using PokeAdvantage.DTOs;

namespace PokeAdvantage.Interfaces
{
    public interface IPokemonApiManager
{
    Task<PokemonDTO?> FetchPokemonData(string pokemonName);
    Task<TypeRelationsDTO> FetchTypeRelationsAsync(string type);
}

}
