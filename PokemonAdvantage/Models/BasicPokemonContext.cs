using PokemonAdvantage.Interfaces;

namespace PokemonAdvantage.Models
{
    public class BasicPokemonContext : PokemonContext
    {
        public Pokemon Pokemon { get; set; }
        public TypeRelations TypeRelations { get; set; }
    }

}