using PokeAdvantage.Interfaces;

namespace PokeAdvantage.Models
{
    public class BasicPokemonContext : PokemonContext
    {
        public Pokemon Pokemon { get; set; }
        public TypeRelations TypeRelations { get; set; }
    }

}