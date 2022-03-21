using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using PokeApp.ViewModels;
using PokeApp.Models;

using Xamarin.Forms;
using System.Linq;

namespace PokeApp.Views
{
    // Classe de la page de l'Équipe.
    public partial class Team : ContentPage
    {
        // Constructeur de la classe initialisant la page , il set également le BindingContext de cette dernière via le ViewModel PokemonTeamViewModel.
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
            SearchBar.Text = "";
        }

        /* Procédure OnCollectionViewSelectionChanged
        ** Procédure appelée lorsque'un pokémon est sélectionné dans la CollectionView (= dans la liste).
        ** Cette dernière affiche les détails du pokémon sélectionné. */
        private void OnSearch(object sender, TextChangedEventArgs e)
        {
            var database = new PokemonDatabase();
            TeamList.ItemsSource = database.GetItemsAsync().Result.Where(s => s.Name.ToLower().StartsWith(e.NewTextValue.ToLower()));
        }

        /* Procédure OnAppearing
        ** Override de la procédure OnAppearing qui s'exécute à chaque fois que la page concernée apparaît à l'écran.
        ** Ici, on met tout simplement à jour la source de la liste (= les données de la base de données) afin qu'après un ajout ou une suppression réalisée par l'utilisateur, la liste soit juste. */
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var database = new PokemonDatabase();
            TeamList.ItemsSource = database.GetItemsAsync().Result;
        }
    }
}