using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Interfaces.IModel;
using MoleWatchApp.Interfaces.IViewModel;
using MoleWatchApp.Models;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{

    /// <summary>
    /// ViewModel for FullPictureView 
    /// </summary>
    public class FullPictureViewModel: BaseViewModel, IFullPictureViewModel
    {

        #region Properties mm 
        /// <summary>
        /// Reference til Viewmodellen's Model.
        /// </summary>
        private IPictureListModel listModelRef;

        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private ImageSource fullImageSource;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string pictureTitle;
        /// <summary>
        /// Privat version af databinded property
        /// </summary>
        private string commentText;


        /// <summary>
        /// Property som bruges til at sætte titlen på viewet.
        /// </summary>
        public string PictureTitle
        {
            get
            {
                return pictureTitle;
            }
            set
            {
                pictureTitle = value;
                OnPropertyChanged();
            }

        }

        /// <summary>
        /// Property som bruges til at databinde billed-kommentarene til billedet.
        /// </summary>
        public string CommentText
        {
            get
            {
                return commentText;
            }
            set
            {
                commentText = value;
                OnPropertyChanged();
            }
        }


        /// <summary>
        /// Databinded property som sætter billedets source på viewet.
        /// </summary>
        public ImageSource FullImageSource
        {
            get
            {
                return fullImageSource;
            }
            set
            {
                fullImageSource = value;
                OnPropertyChanged();
            }
            
        }


        /// <summary>
        /// Kommando der eksekveres når slet billede-knappen trykkes på
        /// </summary>
        public Command DeleteButtonClicked { get; }

        /// <summary>
        /// Kommando der eksekveres når tilføj-kommentar knappen trykkes på
        /// </summary>
        public Command AddCommentButtonClicked { get; }

        #endregion

        /// <summary>
        /// default constructor til viewmodellen
        /// </summary>
        public FullPictureViewModel()
        {
            listModelRef = PictureListModelSingleton.GetPictureListModel();

            DeleteButtonClicked = new Command(DeletePicture);
            AddCommentButtonClicked = new Command(InsertComment);
            

            PictureTitle = listModelRef.PictureOnPage.DateOfUpload.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                CultureInfo.CreateSpecificCulture("da-DA"));

            CommentText = listModelRef.PictureOnPage.PictureComment;


            if (listModelRef.PictureOnPage.PictureData.Length > 10)
            {
                FullImageSource = ImageSource.FromStream(() => new MemoryStream(listModelRef.PictureOnPage.PictureData));
                Thread loadPictureThread = new Thread(LoadUnloadedPicture);
                loadPictureThread.Start();
            }
            else
            {
               Thread loadPictureThread = new Thread(LoadUnloadedPicture);
               loadPictureThread.Start();
            }

        }


        /// <summary>
        /// Metoden henter det valgte billede 
        /// </summary>
        private void LoadUnloadedPicture()
        {
            byte[] imageBytes = listModelRef.LoadSpecificPicture(listModelRef.PictureOnPage.PictureID);

            listModelRef.PictureOnPage.PictureData = imageBytes;

            FullImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));

        }

        /// <summary>
        /// Metoden sletter det valfte billede 
        /// </summary>
        private async void DeletePicture()
        {
            listModelRef.DeleteSpecificPicture();

            await Shell.Current.GoToAsync("..");
        }


        /// <summary>
        /// Metoden indsætter kommentar til billedet 
        /// </summary>
        private void InsertComment()
        {
            listModelRef.UpdatePictureComment(CommentText);
        }
    }
}
