using PokeAdvantage.DTOs;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation.Data
{
    public class PokemonDataAdapter : IPokemonDataAdapter
    {
        public Pokemon AdaptPokemon(PokemonDTO apiPokemon)
        {
            return PokemonAdapter.AdaptPokemon(apiPokemon);
        }

        public TypeRelations AdaptTypeRelations(TypeRelationsDTO dto)
        {
            return PokemonAdapter.AdaptTypeRelations(dto);
        }
    }

}