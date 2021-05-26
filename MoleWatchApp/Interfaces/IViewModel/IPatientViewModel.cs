using System.Collections.ObjectModel;
using System.Windows.Input;
using DataClasses.DTO;
using Xamarin.Forms;

namespace MoleWatchApp.Interfaces
{
    public interface IPatientViewModel
    {
        ObservableCollection<CollectionDTO> PatientCollection { get; set; }

        string Checkmark { get; set; }
        string PlusIcon { get; set; }
        string NewPinAdded { get; set; }
        string PatientPicture { get; set; }
        string RotatePlaceholder { get; set; }
        bool IsAnimationPlaying { get; set; }

        bool BaseIsBusy { get; set; }
    


        Command RotateClicked { get; }
        Command PlusClicked { get; }
        Command BackCommand { get; }
        Command OnPageAppearingCommand { get; }
        ICommand CreateOkClicked
        { get; }

        ICommand ExistingCollectionClicked
        { get; }


    }
}