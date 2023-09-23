using PokeAdvantage.DTOs;
using PokeAdvantage.Models;

namespace PokeAdvantage.Interfaces
{
    public interface IPokemonDataAdapter
{
    Pokemon AdaptPokemon(PokemonDTO apiPokemon);
    TypeRelations AdaptTypeRelations(TypeRelationsDTO dto);
}

}
