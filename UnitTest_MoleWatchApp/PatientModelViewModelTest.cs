using System.Collections.Generic;
using System.Threading;
using APIWebServiesConnector;
using DataClasses.DTO;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Interfaces.IModel;
using MoleWatchApp.Models;
using MoleWatchApp.ViewModels;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Constraints;
using Xamarin.Forms;

namespace UnitTest_MoleWatchApp
{
    public class PatientModelViewModelTest
    {


        [SetUp]
        public void Setup()
        {
        }

        [TearDown]
        public void TearDown()
        {
            
        }


        [TestCase("000000-1234", "Test123")]
        [TestCase("123456-7890", "123456")]
        public void ExistingCollectionClicked_LoadExistingCollectionIsCalled(string cpr, string code)
        {
            //Arrange
            PatientModelViewModel PatVM = new PatientModelViewModel();
            IPatientModel fakePatientModel = new FakePatientModel();
            ILogin fakeLoginModel = new FakeLoginModel();

            PatVM.patientModelClass = fakePatientModel;
            PatVM.loginModel = fakeLoginModel;


            //Act
            PatVM.ExistingCollectionClicked.Execute(null);
            Thread.Sleep(100);

            //Assert
            Assert.That(((FakePatientModel)fakePatientModel).LoadExistingCollectionIsCalled,Is.EqualTo(true));
        }


        [TestCase("000000-1234", "Test123")]
        [TestCase("123456-7890", "123456")]
        public void onPlusClicked_LoadNewCollectionIsCalled(string cpr, string code)
        {
            //Arrange
            PatientModelViewModel PatVM = new PatientModelViewModel();
            IPatientModel fakePatientModel = new FakePatientModel();
            ILogin fakeLoginModel = new FakeLoginModel();

            PatVM.patientModelClass = fakePatientModel;
            PatVM.loginModel = fakeLoginModel;
            CollectionDTO collection = new CollectionDTO();
            collection.Location = new LocationOnBodyDTO();

            //Act
            PatVM.CreateOkClicked.Execute(collection);
            Thread.Sleep(100);

            //Assert
            Assert.That(((FakePatientModel)fakePatientModel).LoadNewCollectionIsCalled, Is.EqualTo(true));
        }

        [TestCase("TestCollection")]
        [TestCase("TestCollection1")]
        public void LoadPatient_(string patientName)
        {
            //Arrange
            PatientModelViewModel PatVM = new PatientModelViewModel();
            IPatientModel fakePatientModel = new FakePatientModel();
            ILogin fakeLoginModel = new FakeLoginModel();

            PatVM.patientModelClass = fakePatientModel;
            PatVM.loginModel = fakeLoginModel;

            CollectionDTO collection = new CollectionDTO();
            List<CollectionDTO> collectionList = new List<CollectionDTO>();
            PatientInfoDTO patInfo = new PatientInfoDTO()
            {
                Gender = "b",Name = patientName
            };
            ((FakeLoginModel) fakeLoginModel).PatientData = new PatientDataDTO()
            {
                PatientInfo = patInfo, CollectionList = collectionList
            };


            //Act
            PatVM.OnPageAppearingCommand.Execute(null);
            Thread.Sleep(100);

            //Assert
            Assert.That(((FakePatientModel)fakePatientModel).CurrentPatient.Name, Is.EqualTo(patientName));
        }

    }

    internal class FakeLoginModel : ILogin
    {
        public PatientDataDTO PatientData { get; set; }
        public bool IsPatientLoadedFromAPI { get; set; }

        public bool VerifyPassword(string Username, string Password)
        {
            return true;
        }

        public bool VerifySmartLoginPassword()
        {
            return true;
        }
    }

    public class FakePatientModel : IPatientModel
    {
        public bool LoadExistingCollectionIsCalled = false;
        public bool LoadNewCollectionIsCalled = false;
        public bool UpdateCollectionIsCalled = false;
        public bool RemoveCollectionIsCalled = false;

        public PatientInfoDTO CurrentPatient { get; set; }
        public PatientDataDTO CurrentPatientData { get; set; }
        public CollectionDTO CollectionOnPage { get; }
        public IAPIService api { get; }
        private bool IsPatientFrontFacing = true;

        public void LoadExistingCollection(CollectionDTO Collection)
        {
            LoadExistingCollectionIsCalled = true;
        }

        public int LoadNewCollection(CollectionDTO Collection)
        {
            LoadNewCollectionIsCalled = true;
            return 1;
        }

        public void UpdateCollection(CollectionDTO collection)
        {
            UpdateCollectionIsCalled = true;
        }

        public void RemoveCollection()
        {
            RemoveCollectionIsCalled = true;
        }
    }
}