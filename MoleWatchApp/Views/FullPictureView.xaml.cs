using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPictureView : ContentPage
    {
        public FullPictureView()
        {
            InitializeComponent();
        }

        private async void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Slet modermærke", "Ønsker du at slette dette billede af modermærket?", "Ja", "Nej");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true)
            {
                FullPictureViewModel.DeleteButtonClicked.Execute(null);
            }
        }


    }
}