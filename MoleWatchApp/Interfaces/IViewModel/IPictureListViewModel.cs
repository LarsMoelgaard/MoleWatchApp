using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using MoleWatchApp.Extensions.DTO;
using Xamarin.Forms;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface IPictureListViewModel
    {
        bool BaseIsBusy { get; set; }
        string PageTitle { get; set; }
        bool IsPicturesFullyLoaded { get; set; }
        ObservableCollection<CompletePicture> CompletePictureList { get; set; }

        Command UpdateTableOnAppearingCommand { get; }
        ICommand OpenFullPictureView { get; }
    }
}
