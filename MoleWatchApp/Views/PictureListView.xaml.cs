using System;
using System.Collections.Generic;
using System.Globalization;
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
        public PictureListView()
        {

            TableList = new List<ImageCell>();

            InitializeComponent();

            //ImageCell testImageCell = new ImageCell();
            //testImageCell.ImageSource = "settings.png";
            //testImageCell.Text = "TestDato her:";
            //testImageCell.Detail = "Indsæt kommentar her";



            PictureListTableView.BindingContext = this;

            


        }


        private ImageSource ConvertByteArrayToImageSource(byte[] PictureData)
        {

            return null;
        }

        private void HiddenPictureListView_OnChildAdded(object sender, ElementEventArgs e)
        {
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
    }
}