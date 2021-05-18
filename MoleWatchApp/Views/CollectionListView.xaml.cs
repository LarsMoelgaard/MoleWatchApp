using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataClasses.DTO;
using MoleWatchApp.Extensions.DTO;
using MoleWatchApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionListView : ContentPage
    {

        private List<ImageCell> CollectionCells;

        public CollectionListView()
        {

            InitializeComponent();

        }

        public void UpdateCollectionListView(object sender, PropertyChangedEventArgs e)
        {
            if (HiddenCollectionListView.ItemsSource != null)
            {
                UpdateTable();
            }
        }

        public void UpdateTable()
        {
            CollectionCells = new List<ImageCell>();
            

            foreach (CollectionWithThumbnail item in HiddenCollectionListView.ItemsSource)
            {
                ImageCell NewCell = new ImageCell
                {
                    Text = item.Collection.CollectionName,


                    
                    ImageSource = ConvertByteArrayToImageSource(item.CollectionPictureData),
                    //Detail = item.Collection.PictureList[item.Collection.PictureList.Count].DateOfUpload.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                    //    CultureInfo.CreateSpecificCulture("da-DA"))

                };
                NewCell.CommandParameter = item.Collection.CollectionID;

                NewCell.SetBinding(ImageCell.CommandProperty, new Binding("ExistingCollectionClicked"));



                CollectionCells.Add(NewCell);
            }


            TableSection Section = new TableSection();

            foreach (ImageCell CollectionPictureControl in CollectionCells)
            {
                Section.Add(CollectionPictureControl);
            }




            CollectionListTableView.Root = new TableRoot
            {
                Section
            };
        }

        private void CollectionListView_OnAppearing(object sender, EventArgs e)
        {
            if (HiddenCollectionListView.ItemsSource != null)
            {
                UpdateTable();
            }
        }



        private ImageSource ConvertByteArrayToImageSource(byte[] PictureData)
        {

            ImageSource NewPhoto = ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream(PictureData);

                return ms;
            });
            return NewPhoto;
        }
    }
}