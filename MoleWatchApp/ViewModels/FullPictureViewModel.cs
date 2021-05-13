using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoleWatchApp.ViewModels
{
    public class FullPictureViewModel: BaseViewModel
    {

        private ImageSource fullImageSource;

        public string PictureTitle { get; set; }
        public string CommentText { get; set; }



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
            FullImageSource = "settings.png";
        }


    }
}
