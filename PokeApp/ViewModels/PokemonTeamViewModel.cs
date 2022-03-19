using PokeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using PokeApiNet;

namespace PokeApp.ViewModels
{
    public class PokemonTeamViewModel : BaseViewModel
    {
        public ObservableCollection<MyPokemon> TeamList
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }
        private static PokemonTeamViewModel _instance = new PokemonTeamViewModel();
        public static PokemonTeamViewModel Instance { get { return _instance; } }

        public PokemonTeamViewModel()
        {
            TeamList = new ObservableCollection<MyPokemon>();
            LinkData();
        }

        async void LinkData()
        {
            PokemonDatabase database = await PokemonDatabase.Instance;

            TeamList = new ObservableCollection<MyPokemon>(await database.GetItemsAsync());
        }
    }
}