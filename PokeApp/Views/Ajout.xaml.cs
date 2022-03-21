using System;
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
    // Classe de la page d'ajout.
    public partial class Ajout : ContentPage
    {
        // Définition du tableau dans lequel sera récupéré les string des différents types possible à ajouter
        readonly string[] types = { "Acier","Combat","Dragon",
                           "Électrique","Eau","Fée","Feu",
                           "Glace","Insecte","Normal",
                           "Plante","Poison","Psy","Roche","Sol",
                           "Spectre","Ténèbres","Vol" };
        // Constructeur de la classe initialisant la page , il remplit également les deux Picker pour les types avec les données de la variable déclarée ci-dessus.
        public Ajout()
        {
            InitializeComponent();
            foreach (string type in types)
            {
                PickerType1.Items.Add(type);
                PickerType2.Items.Add(type);
            }
        }

        /* Procédure Checking
        ** Cette procédure est appelée à chaque fois que le nom, l'image ou le type (obligatoire) du Pokémon est modifié par l'utilisteur (lors de l'ajout). 
        ** Elle permet de vérifier que ces champs sont bien remplis et, le cas échéant, elle active le bouton pour valider l'ajout, de manière à ce que l'utilisateur puisse cliquer dessus.
        ** A contrario, si le bouton a déjà été activé, elle le désactive si un des 3 "champs" est vidé par l'utilisateur. */
        void Checking(object sender, EventArgs args)
        {
            if (Name.Text != "" && PokemonImage.Source != null && PickerType1.SelectedIndex != -1)
            {
                AddPokemon.IsEnabled = true;
            } else
            {
                AddPokemon.IsEnabled = false;
            }
        }

        /* Procédure OnValueChanged
        ** Cette procédure est appelée lors d'un changement sur n'importe lequel des Slider correspondant aux 6 statistiques du Pokémon (lors de l'ajout) par l'utilisateur.
        ** Elle permet de modifier l'affichage de la valeur du Slider qui est en train d'être modifié afin de voir quelle est la valeur que l'on va définir. */
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

        /* Procédure OnButtonClicked
        ** Cette procédure est appelée lors d'un clique sur le bouton pour valider l'ajout.
        ** Elle permet d'ajouter un Pokémon dans la base de données de l'équipe en récupérant les valeurs saisies par l'utilisateur.
        ** Elle remet également tous les champs de l'ajout "à zéro" (à leur valeurs par défaut) et affiche les détails du pokémon créé. */
        async void OnButtonClicked(object sender, EventArgs args)
        {
            var pokemon = new MyPokemon
            {
                Name = Name.Text[0].ToString().ToUpper() + Name.Text.Substring(1),
                Type1 = PickerType1.SelectedItem.ToString(),
                Image = PokemonImage.Source.ToString().Split(' ')[1],
                HP = (int)HP.Value,
                Attack = (int)Attack.Value,
                Defense = (int)Defense.Value,
                SpeAttack = (int)SpeAttack.Value,
                SpeDefense = (int)SpeDefense.Value,
                Speed = (int)Speed.Value
            };
            if (PickerType2.SelectedIndex != -1)
            {
                pokemon.Type2 = PickerType2.SelectedItem.ToString();
            }

            PokemonDatabase database = await PokemonDatabase.Instance;
            await database.SaveItemAsync(pokemon);

            //Reset de tous les champs pour l'ajout
            Name.Text = "";
            PickerType1.SelectedIndex = -1;
            PickerType2.SelectedIndex = -1;
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

            //Ajout du pokémon dans l'équipe
            await Navigation.PushAsync(new DetailsPokemon(await database.GetItemAsync(pokemon.Id),false));
        }

        /* Procédure AjoutImage
        ** Procédure appelée lors de l'ajout d'une image via un bouton.
        ** A l'aide du nuget Xam.Plugin.Media, cette procédure ouvre la galerie du téléphone afin que l'utilisateur puisse choisir une image pour son Pokémon.
        ** Elle affiche ensuite l'image au dessus du bouton qui l'a appelée, puis appelle la fonction Checking() (voir plus haut) lorsqu'une image est choisie. */
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