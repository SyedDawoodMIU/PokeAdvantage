using PokeAdvantage.DTOs;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Models;
using static PokeAdvantage.Models.DamageRelations;

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
                    apiResponse.Types?.Select(t => t.Type.Name).ToList() ?? new List<string>()
                );
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error adapting pokemon"));
                return default!;
            }
        }

        public TypeRelations AdaptTypeRelations(TypeRelationsDTO typeRelationsDTO)
        {
            try
            {
                TypeRelations typeRelations = new(
                                    AdaptDamageRelations(typeRelationsDTO.DamageRelations)
                                );
                return typeRelations;
            }
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error adapting type relations"));
                return default!;
            }

        }

        public DamageRelations AdaptDamageRelations(DamageRelationsDTO damageRelationsDTO)
        {
            try
            {
                return new DamageRelationsBuilder()
                    .WithDoubleDamageFrom(damageRelationsDTO.DoubleDamageFrom?.Select(AdaptDamage).ToList() ?? new List<Damage>())
                    .WithDoubleDamageTo(damageRelationsDTO.DoubleDamageTo?.Select(AdaptDamage).ToList() ?? new List<Damage>())
                    .WithHalfDamageFrom(damageRelationsDTO.HalfDamageFrom?.Select(AdaptDamage).ToList() ?? new List<Damage>())
                    .WithHalfDamageTo(damageRelationsDTO.HalfDamageTo?.Select(AdaptDamage).ToList() ?? new List<Damage>())
                    .WithNoDamageFrom(damageRelationsDTO.NoDamageFrom?.Select(AdaptDamage).ToList() ?? new List<Damage>())
                    .WithNoDamageTo(damageRelationsDTO.NoDamageTo?.Select(AdaptDamage).ToList() ?? new List<Damage>())
                    .Build();
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                _errorHandler.HandleError(new Exception("Error adapting damage"));
                return default!;

            }
        }
    }
}