using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class CreateCollectionViewModel : BaseViewModel
    {

        public string DateText { get; set; } = "lol";

        public string MarkCollectionImage { get; set; } = "NotFlagged.png";


        public Command MarkCommand { get; }

        public Command ShowPictureCollectionCommand { get; }
    }
}
