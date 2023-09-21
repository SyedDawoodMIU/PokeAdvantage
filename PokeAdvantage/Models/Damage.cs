namespace PokeAdvantage.Models
{
    public class Damage
    {
        public Damage(string name, string uRL)
        {
            Name = name;
            URL = uRL;
        }

        public string Name { get; set; }

        public string URL { get; set; }
    }
}