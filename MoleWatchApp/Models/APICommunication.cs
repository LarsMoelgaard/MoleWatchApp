using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;


namespace MoleWatchApp.Models
{
    public class APICommunication
    {
        private PatientInfoDTO newPatientInfoDto;
        private IAPIService API;

        public APICommunication()
        {
            API = new ApiService(APIWebServiesConnector.APIStringFabrics.APIStringFabric.GetDeveloperAPIString(), 'm');
            // Logintype == mobil
            // For at skifte API ændre getDevString til getProductionString



            //API = new StubApiService(); //Skal slettes til produktion
        }

        public bool VerifyPasswordWithAPI(string Username, string Password)
        {
            LoginInfoDTO NewLogin = new LoginInfoDTO();
            
            NewLogin.Password = Password;
            NewLogin.Username = Username;
            

            try
            {
                newPatientInfoDto = API.GetObject<PatientInfoDTO, LoginInfoDTO>("PatientLogin", NewLogin);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

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

        public bool VerifySmartLogin()
        {
           
            try
            {
                newPatientInfoDto = API.GetObject<PatientInfoDTO, SessionInfoDTO>("PatientSmartLogin", new SessionInfoDTO()); //Login type == Mobil

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;

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
