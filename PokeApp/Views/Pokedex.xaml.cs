using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PokeApp.ViewModels;
using PokeApp.Models;

using Xamarin.Forms;
using System.Linq;

namespace PokeApp.Views
{
    public partial class Pokedex : ContentPage
    {
        public Pokedex()
        {
            InitializeComponent();
            BindingContext = PokemonListViewModel.Instance;
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            if (current == null)
            {
                return;
            }
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new DetailsPokemon(current,false));   
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            PokemonsList.ItemsSource = PokemonListViewModel.Instance.PokemonsList.Where(s => s.Name.ToLower().StartsWith(e.NewTextValue.ToLower()));
        }
    }
}