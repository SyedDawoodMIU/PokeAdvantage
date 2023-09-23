using Xunit;
using Moq;
using PokeAdvantage.DTOs;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Implementation.Data;
using System.Collections.Generic;
using PokeAdvantage.Models;

namespace PokeAdvantage.Tests
{
    public class PokemonAdapterTests
    {
        private readonly Mock<IErrorHandler> mockErrorHandler;
        private readonly PokemonAdapter pokemonAdapter;

        public PokemonAdapterTests()
        {
            mockErrorHandler = new Mock<IErrorHandler>();
            pokemonAdapter = new PokemonAdapter(mockErrorHandler.Object);
        }

        [Fact]
        public void AdaptPokemon_ShouldReturnCorrectPokemon()
        {
            // Arrange
            var pokemonDto = new PokemonDTO
            {
                Name = "Pikachu",
                Types = new List<ApiPokemonType>
                {
                    new()
                    {
                        Type = new ApiPokemonTypeDetail()
                        {
                            Name = "Electric"
                        }
                    }
                }
            };

            // Act
            var result = pokemonAdapter.AdaptPokemon(pokemonDto);

            // Assert
            Assert.Equal("Pikachu", result.Name);
            Assert.Single(result.Types);
            Assert.Equal("Electric", result.Types[0]);
        }

        [Fact]
        public void AdaptPokemon_ShouldHandleException()
        {
            // Arrange
            PokemonDTO pokemonDto = null;

            // Act
            var result = pokemonAdapter.AdaptPokemon(pokemonDto);

            // Assert
            mockErrorHandler.Verify(m => m.HandleError(It.IsAny<Exception>()), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void AdaptTypeRelations_ShouldReturnCorrectTypeRelations()
        {
            // Arrange
            var typeRelationsDto = new TypeRelationsDTO()
            {
                DamageRelations = new DamageRelationsDTO()
                {
                    DoubleDamageFrom = new List<DamageTypeDTO>(){
                                        new DamageTypeDTO(){
                                            Name = "Fire"
                                        }
                                    }
                }
            };

            // Act
            var result = pokemonAdapter.AdaptTypeRelations(typeRelationsDto);

            // Assert
            Assert.Equal("Fire", result.DamageRelations.DoubleDamageFrom.First().Name);
        }

        [Fact]
        public void AdaptTypeRelations_ShouldHandleException()
        {
            // Arrange
            TypeRelationsDTO typeRelationsDto = null;

            // Act
            var result = pokemonAdapter.AdaptTypeRelations(typeRelationsDto);

            // Assert
            mockErrorHandler.Verify(m => m.HandleError(It.IsAny<Exception>()), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public void AdaptDamageRelations_ShouldReturnCorrectDamageRelations()
        {
            // Arrange
            var typeRelationsDto = new TypeRelationsDTO
            {
                DamageRelations = new DamageRelationsDTO
                {
                    DoubleDamageFrom = new List<DamageTypeDTO>
                    {
                        new()
                        {
                            Name = "Fire"
                        }
                    }
                }
            };

            // Act
            var result = pokemonAdapter.AdaptTypeRelations(typeRelationsDto);

            // Assert
            Assert.Equal("Fire", result.DamageRelations.DoubleDamageFrom.First().Name);
        }

        [Fact]
        public void AdaptDamageRelations_ShouldHandleException()
        {
            // Arrange
            DamageRelationsDTO damageRelationsDto = null;

            // Act
            var result = pokemonAdapter.AdaptDamageRelations(damageRelationsDto);

            // Assert
            mockErrorHandler.Verify(m => m.HandleError(It.IsAny<Exception>()), Times.Once);
            Assert.Null(result);
        }
    }
}
