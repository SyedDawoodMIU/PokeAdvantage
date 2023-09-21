using PokeAdvantage.DTOs;
using PokeAdvantage.Models;

namespace PokeAdvantage
{
    public class PokemonAdapter
    {
        public static Pokemon AdaptPokemon(PokemonDTO apiResponse)
        {
            return new Pokemon(
                apiResponse.Name,
                apiResponse.Types.Select(t => t.Type.Name).ToList()
            );

        }

        public static TypeRelations AdaptTypeRelations(DamageRelationsDTO damageRelationsDTO)
        {
            TypeRelations typeRelations = new(
                        damageRelationsDTO.DoubleDamageTo.Select(AdaptDamage).ToList(),
                        damageRelationsDTO.DoubleDamageFrom.Select(AdaptDamage).ToList(),
                        damageRelationsDTO.HalfDamageTo.Select(AdaptDamage).ToList(),
                        damageRelationsDTO.HalfDamageFrom.Select(AdaptDamage).ToList(),
                        damageRelationsDTO.NoDamageTo.Select(AdaptDamage).ToList(),
                        damageRelationsDTO.NoDamageFrom.Select(AdaptDamage).ToList()
                    );
            return typeRelations;

        }

        public static Damage AdaptDamage(DamageTypeDTO damageTypeDTO)
        {
            return new Damage(
                damageTypeDTO.Name,
                damageTypeDTO.Url
            );

        }
    }
}