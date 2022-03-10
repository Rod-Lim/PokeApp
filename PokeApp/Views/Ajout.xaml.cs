using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeApp.Models;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokeApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Ajout : ContentPage
    {
        public Ajout()
        {
            InitializeComponent();
        }

        void Checking(object sender, EventArgs args)
        {
            if (Name.Text != "" && Image.Text != "")
            {
                AddPokemon.IsEnabled = true;
            } else
            {
                AddPokemon.IsEnabled = false;
            }
        }

        void OnValueChanged(object sender, EventArgs args)
        {
            if (sender == HP)
            {
                Label_HP.Text = "HP : (" + ((int)((Slider)sender).Value).ToString() + ")";
            }
            if (sender == Attack)
            {
                Label_Attack.Text = "Attaque : (" + ((int)((Slider)sender).Value).ToString() + ")";
            }
            if (sender == Defense)
            {
                Label_Defense.Text = "Défense : (" + ((int)((Slider)sender).Value).ToString() + ")";
            }
            if (sender == SpeAttack)
            {
                Label_SpeAttack.Text = "Attaque Spéciale : (" + ((int)((Slider)sender).Value).ToString() + ")";
            }
            if (sender == SpeDefense)
            {
                Label_SpeDefense.Text = "Défense Spéciale : (" + ((int)((Slider)sender).Value).ToString() + ")";
            }
            if (sender == Speed)
            {
                Label_Speed.Text = "Vitesse : (" + ((int)((Slider)sender).Value).ToString() + ")";
            }
        }
        async void OnButtonClicked(object sender, EventArgs args)
        {
            var pokemon = new MyPokemon();
            pokemon.Name = Name.Text;
            pokemon.Image = Image.Text;
            pokemon.HP = (int)HP.Value;
            pokemon.Attack = (int)Attack.Value;
            pokemon.Defense = (int)Defense.Value;
            pokemon.SpeAttack = (int)SpeAttack.Value;
            pokemon.SpeDefense = (int)SpeDefense.Value;
            pokemon.Speed = (int)Speed.Value;

            PokemonDatabase database = await PokemonDatabase.Instance;
            await database.SaveItemAsync(pokemon);

            await Navigation.PushAsync(new DetailsPokemon(await database.GetItemAsync(pokemon.Id)));
        }
    }
}