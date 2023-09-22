using PokeAdvantage.DTOs;

namespace PokeAdvantage
{
    public interface IPokemonApiManager
{
    Task<PokemonDTO?> FetchPokemonData(string pokemonName);
    Task<TypeRelationsDTO> FetchTypeRelationsAsync(string type);
}

}
