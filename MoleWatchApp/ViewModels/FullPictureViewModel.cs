using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using MoleWatchApp.Interfaces;
using MoleWatchApp.Models;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{

    /// <summary>
    /// ViewModel for FullPictureView 
    /// </summary>
    public class FullPictureViewModel: BaseViewModel
    {

        #region Properties mm 
        private IPictureListModel listModelRef;
        private ImageSource fullImageSource;
        private string pictureTitle;
        private string commentText;

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


        public Command DeleteButtonClicked { get; }
        public Command AddCommentButtonClicked { get; }

        #endregion

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
