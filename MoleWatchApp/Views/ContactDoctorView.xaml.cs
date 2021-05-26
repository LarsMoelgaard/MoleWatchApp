using System;
using System.Collections.Generic;
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
    /// Opretter view for Contact Doctor
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactDoctorView : ContentPage
    {
        /// <summary>
        /// Reference til databinding
        /// </summary>
        private IContactDoctorViewModel ContactVM;

        /// <summary>
        /// Default constructor til viewet
        /// </summary>
        public ContactDoctorView()
        {
            ContactVM = new ContactDoctorViewModel();
            this.BindingContext = ContactVM;
            InitializeComponent();
        }
    }
}