//using PokeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using PokeApiNet;
using PokeApp.Models; 

namespace PokeApp.ViewModels
{
    public class PokemonListViewModel : BaseViewModel
    {       
        public ObservableCollection<MyPokemon> PokemonsList
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }
        private static PokemonListViewModel _instance = new PokemonListViewModel();
        public static PokemonListViewModel Instance { get { return _instance; } }

        public PokemonListViewModel()
        {
            PokemonsList = new ObservableCollection<MyPokemon>();
            LinkApi();
        }

        async void LinkApi()
        {
            PokeApiClient pokeClient = new PokeApiClient();

            for (int i = 1; i <= 151; i++)
            {
                Pokemon pokemon = await Task.Run(() => pokeClient.GetResourceAsync<Pokemon>(i));

                MyPokemon myPokemon = new MyPokemon();
                myPokemon.Id = pokemon.Id;
                myPokemon.Name = pokemon.Name;
                myPokemon.Image = pokemon.Sprites.FrontDefault;
                myPokemon.HP = pokemon.Stats[0].BaseStat;
                myPokemon.Attack = pokemon.Stats[1].BaseStat;
                myPokemon.Defense = pokemon.Stats[2].BaseStat;
                myPokemon.SpeAttack = pokemon.Stats[3].BaseStat;
                myPokemon.SpeDefense = pokemon.Stats[4].BaseStat;
                myPokemon.Speed = pokemon.Stats[5].BaseStat;

                PokemonsList.Add(myPokemon);

            }
        }
    }
}
