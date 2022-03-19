using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PokeApp.ViewModels;
using PokeApp.Models;

using Xamarin.Forms;
using System.Linq;

namespace PokeApp.Views
{
    public partial class Team : ContentPage
    {
        public Team()
        {
            InitializeComponent();
            BindingContext = PokemonTeamViewModel.Instance;
        }

        async void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MyPokemon current = (e.CurrentSelection.FirstOrDefault() as MyPokemon);
            if (current == null)
            {
                return;
            }
            (sender as CollectionView).SelectedItem = null;
            await Navigation.PushAsync(new DetailsPokemon(current,true));
        }

        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var database = new PokemonDatabase();
            TeamList.ItemsSource = database.GetItemsAsync().Result.Where(s => s.Name.ToLower().StartsWith(e.NewTextValue.ToLower()));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var database = new PokemonDatabase();
            TeamList.ItemsSource = database.GetItemsAsync().Result;
        }
    }
}