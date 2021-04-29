using System;
using System.Collections.Generic;
using System.Text;
using APIWebServiesConnector;
using DataClasses.DataObjects.DTO;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using PatientDataDTO = DataClasses.DTO.MISCDTOS.PatientDataDTO;

namespace MoleWatchApp.Models
{
    public class LoginModel: ILogin
    {
        private IAPIService API;

        private PatientInfoDTO newPatientInfoDto;

        public PatientDataDTO PatientData { get; private set; }



        public LoginModel()
        {
            API = APISingleton.GetAPI();
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

                //TODO Duer ikke medmindre det er stubben
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
                
                if (PatientData.PatientInfo == null) //TODO skal fjernes i produktion når API-virker
                {
                    PatientInfoDTO Patient = new PatientInfoDTO();
                    Patient.Gender = "g";

                    PatientData.PatientInfo = Patient;
                    PatientData.CollectionList = new List<CollectionDTO>();

                    CollectionDTO TestCollection = new CollectionDTO();
                    TestCollection.CollectionName = "TestCollection";
                    TestCollection.CollectionID = 1;


                    LocationOnBodyDTO TestLocation = new LocationOnBodyDTO();
                    TestLocation.BodyPart = "arm";
                    TestLocation.BodyPartSide = "right";
                    TestLocation.IsFrontFacing = true;
                    TestLocation.xCoordinate = 200;
                    TestLocation.yCoordinate = 100;

                    TestCollection.Location = TestLocation;
                    

                    PatientData.CollectionList.Add(TestCollection);
                }


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
