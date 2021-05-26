using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface ICreateCollectionViewModel
    {
        ImageSource LastCollectionPhoto { get; set; }
        bool BaseIsBusy { get; set; }
        string DateText { get; set; }
        bool NoImagesInCollection { get; set; }
        string MarkCollectionImage { get; set; }
        string CollectionTitle { get; set; }
        Command MarkCommand { get; }
        Command CameraButtonClicked { get; }
        Command GalleryButtonClicked { get; }
        Command ShowPictureCollectionCommand { get; }
        Command DeleteCollectionCommand { get; }

        ICommand ChangeNameCommand { get; }
    }
}
