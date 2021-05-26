using System;
using System.Collections.Generic;
using APIWebServiesConnector;
using DataClasses.DTO;
using Microsoft.VisualBasic;
using MoleWatchApp.Interfaces.IModel;
using MoleWatchApp.Interfaces.IViewModel;
using MoleWatchApp.ViewModels;
using NSubstitute;
using NUnit.Framework;

namespace UnitTest_MoleWatchApp
{
    [TestFixture]
    public class CreateCollectionViewModelTest
    {
        private CreateCollectionViewModel uutCollectionViewModel;

        private IPatientModel PatientModelfake;

        private ICollectionModel fakeCollectionModel;

        [SetUp]
        public void Setup()
        {
            uutCollectionViewModel = new CreateCollectionViewModel();

            PatientModelfake = new fakePatientModel();
            fakeCollectionModel = Substitute.For<ICollectionModel>();

            CollectionDTO fakeCollectionDTO = new CollectionDTO();

            fakeCollectionDTO.CollectionID = 0;
            fakeCollectionDTO.Location = new LocationOnBodyDTO();
            fakeCollectionDTO.CollectionName = "fake";
            fakeCollectionDTO.PatientID = 1;

            List<PictureInfoDTO> fakePictureInfoDTOs = new List<PictureInfoDTO>();
            fakePictureInfoDTOs.Add(new PictureInfoDTO() { CollectionID = 0, DateOfUpload = DateTime.Now, PictureID = 0 });
            fakePictureInfoDTOs.Add(new PictureInfoDTO() { CollectionID = 0, DateOfUpload = DateTime.Now, PictureID = 1 });
            fakeCollectionDTO.PictureList = fakePictureInfoDTOs;


            fakeCollectionModel.CollectionOnPage.Returns(fakeCollectionDTO);


            uutCollectionViewModel.patientModelRef = PatientModelfake;
            uutCollectionViewModel.collectionModel = fakeCollectionModel;
        }

        [Test]
        public void MarkCollection_CollectionIsNotMarked_CollectionGetsMarked()
        {


            Assert.That(PatientModelfake.CollectionOnPage.CollectionID,Is.EqualTo(0));
        }

        public class fakePatientModel: IPatientModel
        {
            public fakePatientModel()
            {
                CollectionDTO fakeCollectionDTO = new CollectionDTO();

                fakeCollectionDTO.CollectionID = 0;
                fakeCollectionDTO.Location = new LocationOnBodyDTO();
                fakeCollectionDTO.CollectionName = "fake";
                fakeCollectionDTO.PatientID = 1;

                List<PictureInfoDTO> fakePictureInfoDTOs = new List<PictureInfoDTO>();
                fakePictureInfoDTOs.Add(new PictureInfoDTO(){CollectionID = 0,DateOfUpload = DateTime.Now,PictureID = 0});
                fakePictureInfoDTOs.Add(new PictureInfoDTO(){CollectionID = 0,DateOfUpload = DateTime.Now,PictureID = 1});
                fakeCollectionDTO.PictureList = fakePictureInfoDTOs;

                CollectionOnPage = fakeCollectionDTO;

            }

            public PatientInfoDTO CurrentPatient { get; set; }
            public PatientDataDTO CurrentPatientData { get; set; }
            public CollectionDTO CollectionOnPage { get; private set; }
            public IAPIService api { get; }
            public void LoadExistingCollection(CollectionDTO Collection)
            {
                throw new System.NotImplementedException();
            }

            public int LoadNewCollection(CollectionDTO Collection)
            {
                throw new System.NotImplementedException();
            }

            public void UpdateCollection(CollectionDTO collection)
            {
                throw new System.NotImplementedException();
            }

            public void RemoveCollection()
            {
                throw new System.NotImplementedException();
            }
        }

    }
}