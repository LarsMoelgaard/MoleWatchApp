using AppTest.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace AppTest.Views
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