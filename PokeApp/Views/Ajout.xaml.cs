﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
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
            if (Name.Text != "" && PokemonImage.Source != null)
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
            var pokemon = new MyPokemon
            {
                Name = Name.Text,
                Image = PokemonImage.Source.ToString().Split(' ')[1],
                HP = (int)HP.Value,
                Attack = (int)Attack.Value,
                Defense = (int)Defense.Value,
                SpeAttack = (int)SpeAttack.Value,
                SpeDefense = (int)SpeDefense.Value,
                Speed = (int)Speed.Value
            };

            PokemonDatabase database = await PokemonDatabase.Instance;
            await database.SaveItemAsync(pokemon);

            Name.Text = "";
            PokemonImage.Source = null;
            BoutonAjoutImage.Text = "Ajouter une Image";
            Label_HP.Text = "HP : (1)";
            Label_Attack.Text = "Attaque : (1)";
            Label_Defense.Text = "Défense : (1)";
            Label_SpeAttack.Text = "Attaque Spéciale : (1)";
            Label_SpeDefense.Text = "Défense Spéciale : (1)";
            Label_Speed.Text = "Vitesse : (1)";
            HP.Value = 1;
            Attack.Value = 1;
            Defense.Value = 1;
            SpeAttack.Value = 1;
            SpeDefense.Value = 1;
            Speed.Value = 1;
            AddPokemon.IsEnabled = false;

            await Navigation.PushAsync(new DetailsPokemon(await database.GetItemAsync(pokemon.Id)));
        }

        async void AjoutImage(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Non supporté", "Votre telephone ne " +
                    "supporte pas cette fonctionnalitée", "Ok");
                return;
            }

            var mediaOptions = new PickMediaOptions
            {
                PhotoSize = PhotoSize.Full,
                CompressionQuality = 40
            };

            var selectedImageFile = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

            if (selectedImageFile != null)
            {
                PokemonImage.Source = ImageSource.FromFile(selectedImageFile.Path);
                BoutonAjoutImage.Text = "Changer l'Image"; 
                Checking(sender, e);
            }
        }
    }
}