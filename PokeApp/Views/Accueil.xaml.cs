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
    // Classe de la page d'accueil.
    public partial class Accueil : ContentPage
    {
        // Constructeur de la classe initialisant la page.
        public Accueil()
        {
            InitializeComponent();
        }
    }
}