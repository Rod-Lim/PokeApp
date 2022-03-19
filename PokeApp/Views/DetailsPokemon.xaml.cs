using PokeApiNet;
using PokeApp.Models;
using PokeApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPokemon : ContentPage
    {
        public DetailsPokemon(MyPokemon pokemon, Boolean test)
        {
            InitializeComponent();
            BindingContext = pokemon;
            if (test) {
                Button boutonSupprimer = new Button
                {
                    Text = "Supprimer le Pokémon de l'Équipe",
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    BackgroundColor = Color.Red,
                    TextColor = Color.White,
                };
                boutonSupprimer.Clicked += async (sender, args) => SupprimerPokemon(sender, args);
                Body.Children.Add(boutonSupprimer);
            };
        }
        void SupprimerPokemon(object sender, EventArgs args)
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await DisplayAlert("Voulez vous vraiment supprimer ce pokémon de votre équipe ?","", "Oui", "Non");
                if (result) {
                    var database = await PokemonDatabase.Instance;
                    await database.DeleteItemAsync((MyPokemon)BindingContext);
                    await Navigation.PopAsync();
                }
            });
        }
    }
}