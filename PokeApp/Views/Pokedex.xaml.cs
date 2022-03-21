using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PokeApp.ViewModels;
using PokeApp.Models;

using Xamarin.Forms;
using System.Linq;

namespace PokeApp.Views
{
    // Classe de la page du Pokédex.
    public partial class Pokedex : ContentPage
    {
        // Constructeur de la classe initialisant la page , il set également le BindingContext de cette dernière via le ViewModel PokemonListViewModel.
        public Pokedex()
        {
            InitializeComponent();
            BindingContext = PokemonListViewModel.Instance;
        }

        /* Procédure OnCollectionViewSelectionChanged
        ** Procédure appelée lorsque'un pokémon est sélectionné dans la CollectionView (= dans la liste).
        ** Cette dernière affiche les détails du pokémon sélectionné. */
        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            if (current == null)
            {
                return;
            }
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new DetailsPokemon(current,false));
            SearchBar.Text = "";
        }

        /* Procédure OnSearch
        ** Procédure appelée lors d'une recherche, elle fait tout simplement ressortir les résultat correspondants à la recherche. */
        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            PokemonsList.ItemsSource = PokemonListViewModel.Instance.PokemonsList.Where(s => s.Name.ToLower().StartsWith(e.NewTextValue.ToLower()));
        }
    }
}