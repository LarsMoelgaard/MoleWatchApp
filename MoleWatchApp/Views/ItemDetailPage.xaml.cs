using MoleWatchApp.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace MoleWatchApp.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}