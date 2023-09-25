using PokemonAdvantage.Models;

namespace PokemonAdvantage.Models
{
    public class PokemonContext
    {
        public Pokemon Pokemon { get; set; }
        public TypeRelations TypeRelations { get; set; }

        public string CurrentType { get; set; }

    }


}