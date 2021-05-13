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
    public class FullPictureViewModel: BaseViewModel
    {
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

        public FullPictureViewModel()
        {
            listModelRef = PictureListModelSingleton.GetPictureListModel();

            DeleteButtonClicked = new Command(DeletePicture);
            

            PictureTitle = listModelRef.PictureOnPage.DateOfUpload.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                CultureInfo.CreateSpecificCulture("da-DA"));

            CommentText = listModelRef.PictureOnPage.PictureComment;


            if (listModelRef.PictureOnPage.PictureData.Length > 10)
            {
                FullImageSource = ImageSource.FromStream(() => new MemoryStream(listModelRef.PictureOnPage.PictureData));
            }
            else
            {
               Thread loadPictureThread = new Thread(LoadUnloadedPicture);
               loadPictureThread.Start();
            }

        }

        private void LoadUnloadedPicture()
        {
            byte[] imageBytes = listModelRef.LoadSpecificPicture(listModelRef.PictureOnPage.PictureID);

            listModelRef.PictureOnPage.PictureData = imageBytes;

            FullImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));

        }

        private async void DeletePicture()
        {
            listModelRef.DeleteSpecificPicture();

            await Shell.Current.GoToAsync("..");
        }

    }
}
