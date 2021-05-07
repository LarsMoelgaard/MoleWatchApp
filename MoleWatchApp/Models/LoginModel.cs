using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class LoginModel: ILogin
    {
        private IAPIService API;

        private PatientInfoDTO newPatientInfoDto;

        public PatientDataDTO PatientData { get; private set; }

        public bool IsPatientLoadedFromAPI { get; set; } = false;

        public LoginModel()
        {
            API = APISingleton.GetAPI();
        }

        public bool VerifyPassword(string Username, string Password)
        {
            LoginInfoDTO NewLogin = new LoginInfoDTO();

            NewLogin.Password = Password;
            NewLogin.Username = Username;

            IsPatientLoadedFromAPI = false;


            try
            {
                newPatientInfoDto = API.GetObject<PatientInfoDTO, LoginInfoDTO>
                    ("PatientLogin", NewLogin);

                
                PatientData = API.GetObject<PatientDataDTO, PatientInfoRequestDTO>
                    ("GetPatientData", new PatientInfoRequestDTO() { LoginID = newPatientInfoDto.PatientID });
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

        public bool VerifySmartLoginPassword()
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
