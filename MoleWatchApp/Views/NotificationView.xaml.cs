using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationView : ContentPage
    {
        private NotificationViewModel vm;

        public NotificationView()
        {
            InitializeComponent();
            vm = new NotificationViewModel();
            BindingContext = vm;
        }

        private void NotificationPicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker) sender;
            vm.PickedIndex = picker.SelectedIndex;
        }

        private void DatePicker_OnDateSelected(object sender, DateChangedEventArgs e)
        {
            var datepicker = (DatePicker) sender;
            vm.PickedDate = datepicker.Date;
        }

        private void SaveNotificationButton_OnClicked(object sender, EventArgs e)
        {
            vm.SaveClickedCommand.Execute(null);
        }
    }
}