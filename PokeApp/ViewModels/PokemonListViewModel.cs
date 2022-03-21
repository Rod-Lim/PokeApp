using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using PokeApiNet;
using PokeApp.Models; 

namespace PokeApp.ViewModels
{
    /* Classe PokemonListViewModel
    ** Cette classe est un ViewModel et va gérer le contenu de la View Pokedex. */
    public class PokemonListViewModel : BaseViewModel
    {       
        /* Variable PokemonsList de type ObservableCollection<MyPokemon>
        ** Cette variable contiendra la liste des 898 Pokémon du Pokédex. */
        public ObservableCollection<MyPokemon> PokemonsList
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }
        private static PokemonListViewModel _instance = new PokemonListViewModel();
        public static PokemonListViewModel Instance { get { return _instance; } }

        /* Constructeur de la classe
        ** Initialise le champ PokemonsList et utilise la procédure LinkApi() pour le remplir. */
        public PokemonListViewModel()
        {
            PokemonsList = new ObservableCollection<MyPokemon>();
            LinkApi();
        }

        /* Procédure LinkApi()
        ** Cette procédure prends les 898 Pokémon existants dans la PokeAPI, en récupère les données correspondantes à la classe MyPokemon et les mets dans la variable PokemonsList. */
        async void LinkApi()
        {
            PokeApiClient pokeClient = new PokeApiClient();

            for (int i = 1; i <= 898; i++)
            {
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));

                MyPokemon myPokemon = new MyPokemon
                {
                    Id = pokemon.Id,
                    Name = pokemon.Name[0].ToString().ToUpper() + pokemon.Name.Substring(1),
                    Image = pokemon.Sprites.FrontDefault,
                    HP = pokemon.Stats[0].BaseStat,
                    Attack = pokemon.Stats[1].BaseStat,
                    Defense = pokemon.Stats[2].BaseStat,
                    SpeAttack = pokemon.Stats[3].BaseStat,
                    SpeDefense = pokemon.Stats[4].BaseStat,
                    Speed = pokemon.Stats[5].BaseStat,
                    Type1 = pokemon.Types[0].Type.Name[0].ToString().ToUpper() + pokemon.Types[0].Type.Name.Substring(1)
                };
                if (pokemon.Types.Count == 2)
                {
                    myPokemon.Type2 = pokemon.Types[1].Type.Name[0].ToString().ToUpper() + pokemon.Types[1].Type.Name.Substring(1);
                } else
                {
                    myPokemon.Type2 = "";
                }
                PokemonsList.Add(myPokemon);
            }
        }
    }
}
