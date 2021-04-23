using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using DataClasses.DTO.MISCDTOS;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class LoginModel: ILogin
    {
        private IAPIService API;

        private PatientInfoDTO newPatientInfoDto;

        public PatientDataDTO PatientData { get; private set; }



        public LoginModel()
        {
            API = APIFactory.GetAPI();
        }

        public bool VerifyPassword(string Username, string Password)
        {
            LoginInfoDTO NewLogin = new LoginInfoDTO();

            NewLogin.Password = Password;
            NewLogin.Username = Username;


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
                throw e;
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
