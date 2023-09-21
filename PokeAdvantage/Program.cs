using Newtonsoft.Json;
using PokeAdvantage.Business;
using PokeAdvantage.DTOs;
using PokeAdvantage.Models;

namespace PokeAdvantage
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Enter the name of the Pokemon:");
            string? pokemonName = Console.ReadLine();
            if (pokemonName != null)
            {
                string jsonResponse = PokemonApiClient.GetPokemonJsonAsync(pokemonName).Result;
                if (jsonResponse != null)
                {
                    PokemonDTO apiPokemon = JsonConvert.DeserializeObject<PokemonDTO>(jsonResponse);
                    if (apiPokemon != null)
                    {
                        Pokemon pokemon = PokemonAdapter.AdaptPokemon(apiPokemon);
                        Console.WriteLine($"Pokemon: {pokemon.Name}");
                        foreach (string type in pokemon.Types)
                        {
                            string types = PokemonApiClient.GetTypeRelationsJsonAsync(type).Result;
                            if (types != null)
                            {
                                TypeRelationsDTO response = JsonConvert.DeserializeObject<TypeRelationsDTO>(types);
                                if (response != null)
                                {
                                    TypeRelations typeRelations = PokemonAdapter.AdaptTypeRelations(response.DamageRelations);

                                    List<string> strengths = CalculateDamage.DetermineStrengths(typeRelations);
                                    List<string> weaknesses = CalculateDamage.DetermineWeaknesses(typeRelations);

                                    Console.WriteLine($"For type {type}:");
                                    Console.WriteLine($"Strengths against: {string.Join(", ", strengths)}");
                                    Console.WriteLine($"Weaknesses against: {string.Join(", ", weaknesses)}");
                                    Console.WriteLine("\n\n");
                                }
                            }

                        }


                    }
                }
            }
        }
    }
}