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
    // Classe de la page des Détails.
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPokemon : ContentPage
    {
        /* Constructeur de la classe initialisant la page, il prends en entré un Pokémon de type MyPokémon et un Booléen.
        ** Il set le BindingContext la classe au Pokémon qu'il reçoit en entré et, si le booléen est vrai, il fait apparaître un bouton permettant de supprimer le pokémon dont les détails sont affichés de la base de données.
        ** Le Booléen n'étant supposé être vrai que lorsque les détails sont appelés depuis la liste de l'Équipe, et non depuis celle du Pokédex. */
        public DetailsPokemon(MyPokemon pokemon, Boolean fromEquipe)
        {
            InitializeComponent();
            BindingContext = pokemon;
            if (fromEquipe) {
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
        /* Procédure SupprimerPokemon
        ** Procésure appelée lors de l'appui sur le bouton supprimer.
        ** Elle permet de supprimer le Pokémon qui est actuellement affiché sur la page de la base de données.*/
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