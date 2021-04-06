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
            API = new StubApiService();

        }

        public bool VerifyPasswordWithAPI(string Username, string Password)
        {
            LoginInfoDTO NewLogin = new LoginInfoDTO();

            NewLogin.Password = Password;
            NewLogin.Username = Username;


            try
            {
                newPatientInfoDto = API.GetObject<PatientInfoDTO, LoginInfoDTO>("PostLoginPatient", NewLogin);
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
