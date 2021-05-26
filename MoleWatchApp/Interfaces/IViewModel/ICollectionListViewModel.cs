using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using MoleWatchApp.Extensions.DTO;
using Xamarin.Forms;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface ICollectionListViewModel
    {
        ObservableCollection<CollectionWithThumbnail> CollectionWithPicture { get; set; }
        bool BaseIsBusy { get; set; }

        ICommand ExistingCollectionListClicked { get; }
        Command UpdateCollections { get; }


    }
}
