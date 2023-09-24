using PokeAdvantage.DTOs;
using PokeAdvantage.Interfaces;
using PokeAdvantage.Interfaces.Logging;
using PokeAdvantage.Models;
using static PokeAdvantage.Models.DamageRelations;

namespace PokeAdvantage.Tests
{
    public class ProgramEntryTests
    {
        private readonly Mock<IUserInputManager> mockInputManager;
        private readonly Mock<IPokemonApiManager> mockApiManager;
        private readonly Mock<IPokemonDataAdapter> mockDataAdapter;
        private readonly Mock<IPokemonBusinessLogic> mockBusinessLogic;
        private readonly Mock<IErrorHandler> mockErrorHandler;
        private readonly Mock<ILogger> mockLogger;
        private readonly Mock<IObserver> mockObserver;
        private ProgramEntry programEntry;

        public ProgramEntryTests()
        {
            mockInputManager = new Mock<IUserInputManager>();
            mockApiManager = new Mock<IPokemonApiManager>();
            mockDataAdapter = new Mock<IPokemonDataAdapter>();
            mockBusinessLogic = new Mock<IPokemonBusinessLogic>();
            mockErrorHandler = new Mock<IErrorHandler>();
            mockLogger = new Mock<ILogger>();
            mockObserver = new Mock<IObserver>();
            Setup(mockInputManager, mockApiManager, mockDataAdapter, mockBusinessLogic, mockErrorHandler, mockLogger);


        }

        private void Setup(Mock<IUserInputManager> mockInputManager, Mock<IPokemonApiManager> mockApiManager, Mock<IPokemonDataAdapter> mockDataAdapter, Mock<IPokemonBusinessLogic> mockBusinessLogic, Mock<IErrorHandler> mockErrorHandler, Mock<ILogger> mockLogger)
        {

            programEntry = new ProgramEntry(
                mockInputManager.Object,
                mockApiManager.Object,
                mockDataAdapter.Object,
                mockBusinessLogic.Object,
                mockErrorHandler.Object,
                mockLogger.Object
            );

            mockInputManager.Setup(m => m.GetPokemonName()).Returns("fire");
        }

        [Fact]
        public async Task RunAsync_PokemonNameIsEmpty_ThrowsException()
        {
            // Arrange
            mockInputManager.Setup(m => m.GetPokemonName()).Returns(string.Empty);

            // Act and Assert
            await programEntry.RunAsync();
            mockErrorHandler.Verify(e => e.HandleError(It.Is<Exception>(ex => ex.Message == "The Pokemon name is null or empty")), Times.Once);
        }

        [Fact]
        public async Task RunAsync_FetchAndAdaptPokemonDataRetries()
        {
            // Arrange
            mockInputManager.Setup(m => m.GetPokemonName()).Returns("fire");
            mockApiManager.SetupSequence(m => m.FetchPokemonData(It.IsAny<string>()))
                         .ReturnsAsync((PokemonDTO)null)
                         .ReturnsAsync(new PokemonDTO());

            // Act
            await programEntry.RunAsync();

            // Assert
            mockApiManager.Verify(m => m.FetchPokemonData(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public async Task RunAsync_FetchAndAdaptTypeDataRetries()
        {
            // Arrange
            Initiate(out PokemonDTO pokemonDTO, out Pokemon pokemon, out TypeRelationsDTO typeRelationsDTO);

            mockInputManager.Setup(m => m.GetPokemonName()).Returns("fire");
            mockDataAdapter.Setup(m => m.AdaptPokemon(pokemonDTO)).Returns(pokemon);
            mockDataAdapter.Setup(m => m.AdaptTypeRelations(It.IsAny<TypeRelationsDTO>())).Returns(new TypeRelations(
                new DamageRelationsBuilder()
                .WithDoubleDamageTo(new List<Damage>())
                .WithDoubleDamageFrom(new List<Damage>())
                .WithHalfDamageTo(new List<Damage>())
                .WithHalfDamageFrom(new List<Damage>())
                .WithNoDamageTo(new List<Damage>())
                .WithNoDamageFrom(new List<Damage>())
                .Build()
                ));


            mockApiManager.Setup(m => m.FetchPokemonData(It.IsAny<string>())).ReturnsAsync(pokemonDTO);
            mockApiManager.SetupSequence(m => m.FetchTypeRelationsAsync(It.IsAny<string>()))
                                    .ReturnsAsync((TypeRelationsDTO)null)
                                    .ReturnsAsync(new TypeRelationsDTO());

            Setup(mockInputManager, mockApiManager, mockDataAdapter, mockBusinessLogic, mockErrorHandler, mockLogger);
            programEntry.AttachObserver(mockObserver.Object);
            // Act
            await programEntry.RunAsync();

            // Assert
            mockApiManager.Verify(m => m.FetchTypeRelationsAsync(It.IsAny<string>()), Times.Exactly(2));
        }

        [Fact]
        public async Task RunAsync_NotifyAllObservers_IsCalled()
        {
            // Arrange
            Initiate(out PokemonDTO pokemonDTO, out Pokemon pokemon, out TypeRelationsDTO typeRelationsDTO);

            mockInputManager.Setup(m => m.GetPokemonName()).Returns("fire");
            mockApiManager.Setup(m => m.FetchPokemonData(It.IsAny<string>())).ReturnsAsync(pokemonDTO);
            mockDataAdapter.Setup(m => m.AdaptPokemon(It.IsAny<PokemonDTO>())).Returns(pokemon);
            mockApiManager.Setup(m => m.FetchTypeRelationsAsync(It.IsAny<string>())).ReturnsAsync(typeRelationsDTO);
            mockDataAdapter.Setup(m => m.AdaptTypeRelations(It.IsAny<TypeRelationsDTO>())).Returns(new TypeRelations(
                new DamageRelationsBuilder()
                .WithDoubleDamageTo(new List<Damage>())
                .WithDoubleDamageFrom(new List<Damage>())
                .WithHalfDamageTo(new List<Damage>())
                .WithHalfDamageFrom(new List<Damage>())
                .WithNoDamageTo(new List<Damage>())
                .WithNoDamageFrom(new List<Damage>())
                .Build()
                ));


            Setup(mockInputManager, mockApiManager, mockDataAdapter, mockBusinessLogic, mockErrorHandler, mockLogger);
            programEntry.AttachObserver(mockObserver.Object);


            // Act
            await programEntry.RunAsync();

            // Assert
            mockObserver.Verify(o => o.Update(It.IsAny<PokemonContext>()), Times.Once);
        }

        private static void Initiate(out PokemonDTO pokemonDTO, out Pokemon pokemon, out TypeRelationsDTO typeRelationsDTO)
        {
            pokemonDTO = new PokemonDTO()
            {
                Name = "fire",
                Types = new List<ApiPokemonType>()
                {
                    new()
                    {
                        Type = new ApiPokemonTypeDetail()
                        {
                            Name = "fire",
                            Url = "https://pokeapi.co/api/v2/type/10/"
                        }
                    }
                }
            };
            pokemon = new Pokemon("fire", new List<string>() { "fire" });
            typeRelationsDTO = new TypeRelationsDTO()
            {
                DamageRelations = new DamageRelationsDTO()
                {
                    DoubleDamageFrom = new List<DamageTypeDTO>(),
                    DoubleDamageTo = new List<DamageTypeDTO>(),
                    HalfDamageFrom = new List<DamageTypeDTO>(),
                    HalfDamageTo = new List<DamageTypeDTO>(),
                    NoDamageFrom = new List<DamageTypeDTO>(),
                    NoDamageTo = new List<DamageTypeDTO>()
                }
            };
        }
    }
}
