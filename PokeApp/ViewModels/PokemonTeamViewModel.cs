using PokeApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using PokeApiNet;

namespace PokeApp.ViewModels
{
    /* Classe PokemonTeamViewModel
    ** Cette classe est un ViewModel et va gérer le contenu de la View Team. */
    public class PokemonTeamViewModel : BaseViewModel
    {
        /* Variable TeamList de type ObservableCollection<MyPokemon>
        ** Cette variable contiendra la liste des Pokémon ajoutés par l'utilisateur. */
        public ObservableCollection<MyPokemon> TeamList
        {
            get { return GetValue<ObservableCollection<MyPokemon>>(); }
            set { SetValue(value); }
        }
        private static PokemonTeamViewModel _instance = new PokemonTeamViewModel();
        public static PokemonTeamViewModel Instance { get { return _instance; } }

        /* Constructeur de la classe
        ** Initialise le champ TeamList et utilise la procédure LinkData() pour le remplir. */
        public PokemonTeamViewModel()
        {
            TeamList = new ObservableCollection<MyPokemon>();
            LinkData();
        }
        /* Procédure LinkData()
        ** Cette procédure prends les Pokémon existants dans la base de données et les mets dans la variable TeamList. */
        async void LinkData()
        {
            PokemonDatabase database = await PokemonDatabase.Instance;

            TeamList = new ObservableCollection<MyPokemon>(await database.GetItemsAsync());
        }
    }
}