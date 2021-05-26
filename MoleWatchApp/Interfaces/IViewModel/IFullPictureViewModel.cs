using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface IFullPictureViewModel
    {
        string CommentText { get; set; }
        string PictureTitle { get; set; }
        ImageSource FullImageSource { get; set; }
        Command DeleteButtonClicked { get; }
        Command AddCommentButtonClicked { get; }

    }
}
