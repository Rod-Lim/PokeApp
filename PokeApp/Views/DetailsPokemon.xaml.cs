using PokeApiNet;
using PokeApp.Models;
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
        public DetailsPokemon(MyPokemon pokemon)
        {
            InitializeComponent();
            BindingContext = pokemon;
        }
    }
}