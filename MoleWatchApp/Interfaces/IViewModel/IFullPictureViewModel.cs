using System;
using System.Collections.Generic;
using System.Text;

namespace MoleWatchApp.Interfaces.IViewModel
{
    public interface IFullPictureViewModel
    {
        string CommentText { get; set; }
        string PictureTitle { get; set; }
    }
}
