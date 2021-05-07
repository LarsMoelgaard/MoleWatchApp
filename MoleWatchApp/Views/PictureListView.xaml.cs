using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MoleWatchApp.Extensions.DTO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MoleWatchApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListView : ContentPage
    {
        public List<ImageCell> TableList { get; set; }
        private bool NoItemsInTableView = true;

        public PictureListView()
        {

            TableList = new List<ImageCell>();

            InitializeComponent();

            //ImageCell testImageCell = new ImageCell();
            //testImageCell.ImageSource = "settings.png";
            //testImageCell.Text = "TestDato her:";
            //testImageCell.Detail = "Indsæt kommentar her";



            //PictureListTableView.BindingContext = this;
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


        private void UpdateTable()
        {
            TableList = new List<ImageCell>();
            

            foreach (CompletePicture item in HiddenPictureListView.ItemsSource)
            {
                TableList.Add(new ImageCell
                    {
                        Text = item.DateOfUpload.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                            CultureInfo.CreateSpecificCulture("da-DA")),

                        ImageSource = ConvertByteArrayToImageSource(item.PictureData),

                        Detail = "Testkommentar"
                    }
                );
            }

            TableSection Section = new TableSection();

            foreach (ImageCell PictureControl in TableList)
            {
                Section.Add(PictureControl);
            }



            PictureListTableView.Root = new TableRoot
            {
                Section
            };
        }

        private void HiddenPictureListView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

                if (!NoItemsInTableView)
                {
                    UpdateTable();
                }
                        //Device.BeginInvokeOnMainThread(() =>
                        //{

                        //});
        }

        private void HiddenPictureListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
                UpdateTable();
                NoItemsInTableView = false;
        }
    }
}