using System;
using System.Collections.Generic;
using System.Text;
using DataClasses.DTO;

namespace MoleWatchApp.Extensions.DTO
{
    public class CollectionWithThumbnail
    {
        public byte[] CollectionPictureData { get; set; }

        public CollectionDTO Collection { get; set; }

        public CollectionWithThumbnail(CollectionDTO collection)
        {
            Collection = collection;
            CollectionPictureData = new byte[1];
        }
    }
}
