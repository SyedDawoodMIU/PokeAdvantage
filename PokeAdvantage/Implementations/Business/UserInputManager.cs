using PokeAdvantage.Interfaces;

namespace PokeAdvantage.Implementation.Business
{
    public class ConsoleInputManager : IUserInputManager
    {
        public string GetPokemonName()
        {
            Console.WriteLine("Enter the name of the Pokemon:");
            return Console.ReadLine();
        }
    }

}