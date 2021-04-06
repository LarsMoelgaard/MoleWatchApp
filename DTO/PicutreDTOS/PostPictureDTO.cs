using System;
using System.Collections.Generic;
using System.Text;

namespace DataClasses.DTO
{
    public class PostPictureDTO
    {
        public PictureInfoDTO Info { get; set; }
        public PictureDataDTO Data { get; set; }
        public PictureCommentDTO Comment { get; set; }
    }
}
