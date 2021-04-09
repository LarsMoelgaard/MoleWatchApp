using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO;
using APIWebServiesConnector;


namespace MoleWatchApp.Models
{
    public class APICommunication
    {
        private PatientInfoDTO newPatientInfoDto;
        private IAPIService API;

        public APICommunication()
        {
            //API = new ApiService(APIWebServiesConnector.APIStringFabrics.APIStringFabric.GetDeveloperAPIString());  // For at skifte API ændre getDevString til getProductionString
            API = new StubApiService(); //Skal slettes til produktion
        }

        public bool VerifyPasswordWithAPI(string Username, string Password)
        {
            LoginInfoDTO NewLogin = new LoginInfoDTO();
            
            NewLogin.Password = Password;
            NewLogin.Username = Username;


            try
            {
                newPatientInfoDto = API.GetObject<PatientInfoDTO, LoginInfoDTO>("PatientLogin", NewLogin); //Skal hedde PostPatientLogin
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            if (newPatientInfoDto != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
