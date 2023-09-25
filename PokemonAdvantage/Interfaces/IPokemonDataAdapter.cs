using PokemonAdvantage.DTOs;
using PokemonAdvantage.Models;

namespace PokemonAdvantage.Interfaces
{
    public interface IPokemonDataAdapter
{
    Pokemon AdaptPokemon(PokemonDTO apiPokemon);
    TypeRelations AdaptTypeRelations(TypeRelationsDTO dto);
}

}
