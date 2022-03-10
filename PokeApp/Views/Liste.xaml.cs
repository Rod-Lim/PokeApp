using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PokeApp.ViewModels;
using PokeApp.Models;

using Xamarin.Forms;
using System.Linq;

namespace PokeApp.Views
{
    public partial class Liste : ContentPage
    {
        public Liste()
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
            await Navigation.PushAsync(new DetailsPokemon(current));   
        }
    }
}