using APIWebServiesConnector;
using DataClasses.DTO;

namespace MoleWatchApp.Interfaces.IModel
{
    public interface IPatientModel
    {
         PatientInfoDTO CurrentPatient { get; set; }
         PatientDataDTO CurrentPatientData { get; set; }
         CollectionDTO CollectionOnPage { get; }


        IAPIService api { get; }



        void LoadExistingCollection(CollectionDTO Collection);


        int LoadNewCollection(CollectionDTO Collection);

        void UpdateCollection(CollectionDTO collection);

        void RemoveCollection();

    }
}
