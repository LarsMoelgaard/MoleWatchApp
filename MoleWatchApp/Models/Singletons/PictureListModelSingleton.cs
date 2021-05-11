using System;
using System.Collections.Generic;
using System.Text;
using MoleWatchApp.Interfaces;

namespace MoleWatchApp.Models
{
    public class PictureListModelSingleton
    {
        private static IPictureListModel _pictureListModel;

        private static IPictureListModel pictureListModel
        {
            get
            {
                if (_pictureListModel == null)
                {
                    _pictureListModel = new PictureListModel();
                }

                return _pictureListModel;
            }
        }

        public static IPictureListModel GetPictureListModel()
        {
            return pictureListModel;
        }
    }
}
