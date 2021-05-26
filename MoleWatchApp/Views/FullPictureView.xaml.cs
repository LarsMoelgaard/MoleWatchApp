using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoleWatchApp.Interfaces.IViewModel;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    /// <summary>
    /// Opretter view til at vise det fulde billede af et modermærke 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullPictureView : ContentPage
    {
        /// <summary>
        /// Reference til databinding
        /// </summary>
        private IFullPictureViewModel FullPictureVM;

        /// <summary>
        /// Default constructor
        /// </summary>
        public FullPictureView()
        {
            FullPictureVM = new FullPictureViewModel();
            this.BindingContext = FullPictureVM;
            InitializeComponent();
        }


        /// <summary>
        /// Metoden sletter det valgte billede af modermærket 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void DeleteButton_OnClicked(object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("Slet modermærke", "Ønsker du at slette dette billede af modermærket?", "Ja", "Nej");
            Debug.WriteLine("Answer: " + answer);
            if (answer == true)
            {
                FullPictureVM.DeleteButtonClicked.Execute(null);
            }
        }


        /// <summary>
        /// Metoden lader brugeren oprette en kommentar til billedet 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void AddCommentButton_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert("Kommentar opdateret", "Kommentaren er nu opdateret og gemt", "OK");
        }
    }
}