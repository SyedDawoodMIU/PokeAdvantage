using PokemonAdvantage.Models;
namespace PokemonAdvantage.Models
{

    public class Pokemon
    {
        public string Name { get; private set; }
        public List<string> Types { get; private set; }

        public Pokemon(string name, List<string> type)
        {
            Name = name;
            Types = type;
        }

        public List<string> Strengths { get; set; }
        public List<string> Weaknesses { get; set; }

    }
}