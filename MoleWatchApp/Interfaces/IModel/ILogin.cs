using DataClasses.DTO;

namespace MoleWatchApp.Interfaces.IModel
{
    public interface ILogin
    {
        PatientDataDTO PatientData { get; }
        bool IsPatientLoadedFromAPI { get; set; }

        bool VerifyPassword(string Username, string Password);

        bool VerifySmartLoginPassword(); 
    }
}
