using PokeAdvantage.DTOs;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;

namespace PokeAdvantage.Implementation.Data
{
    public class PokemonAdapter : IPokemonDataAdapter
    {
        private readonly IErrorHandler _errorHandler;

        public PokemonAdapter(IErrorHandler errorHandler)
        {
            _errorHandler = errorHandler;
        }


        public Pokemon AdaptPokemon(PokemonDTO apiResponse)
        {
            try
            {
                return new Pokemon(
                    apiResponse.Name,
                    apiResponse.Types.Select(t => t.Type.Name).ToList()
                );
            }
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error adapting pokemon"));
                return default!;
            }

        }

        public TypeRelations AdaptTypeRelations(TypeRelationsDTO typeRelationsDTO)
        {
            try
            {
                return new TypeRelations(
                    AdaptDamageRelations(typeRelationsDTO.DamageRelations)
                );
            }
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error adapting type relations"));
                return default!;
            }

        }

        public DamageRelations AdaptDamageRelations(DamageRelationsDTO damageRelationsDTO)
        {
            try
            {
                return new DamageRelations(
                    damageRelationsDTO.DoubleDamageFrom.Select(AdaptDamage).ToList(),
                    damageRelationsDTO.DoubleDamageTo.Select(AdaptDamage).ToList(),
                    damageRelationsDTO.HalfDamageFrom.Select(AdaptDamage).ToList(),
                    damageRelationsDTO.HalfDamageTo.Select(AdaptDamage).ToList(),
                    damageRelationsDTO.NoDamageFrom.Select(AdaptDamage).ToList(),
                    damageRelationsDTO.NoDamageTo.Select(AdaptDamage).ToList()
                );
            }
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error adapting damage relations"));
                return default!;
            }

        }

        public Damage AdaptDamage(DamageTypeDTO damageTypeDTO)
        {
            try
            {
                return new Damage(
                    damageTypeDTO.Name,
                    damageTypeDTO.Url
                );
            }
            catch (Exception)
            {
                _errorHandler.HandleError(new Exception("Error adapting damage"));
                return default!;

            }
        }
    }
}