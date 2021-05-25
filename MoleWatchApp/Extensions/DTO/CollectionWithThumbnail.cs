using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO;
using Xamarin.Forms;

namespace MoleWatchApp.Extensions.DTO
{
    /// <summary>
    /// Klasse der indeholder dataen som en Collection sammen med en ImageSource som indeholder billeddata
    /// </summary>
    public class CollectionWithThumbnail
    {
        public ImageSource CollectionPictureData { get; set; }

        public CollectionDTO Collection { get; set; }

        public CollectionWithThumbnail(CollectionDTO collection)
        {
            Collection = collection;
            CollectionPictureData = null;
        }
    }
}
