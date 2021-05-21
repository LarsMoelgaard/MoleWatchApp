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
    /// <summary>
    /// Opretter view for at vise samtlige billeder for en samling 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PictureListView : ContentPage
    {
        public List<ImageCell> TableList { get; set; }

        public PictureListView()
        {

            TableList = new List<ImageCell>();

            InitializeComponent();
        }

        private bool NoItemsInTableView = true;

        /// <summary>
        /// Konverterer bytearray til et imagesource
        /// </summary>
        /// <param name="PictureData"></param>
        /// <returns></returns>
        private ImageSource ConvertByteArrayToImageSource(byte[] PictureData)
        {
            
            ImageSource NewPhoto = ImageSource.FromStream(() =>
            {
                MemoryStream ms = new MemoryStream(PictureData);

                return ms;
            });
            return NewPhoto;
        }


        /// <summary>
        /// Opdatere indholdet på siden, således alle nye billeder også loades 
        /// </summary>

        private void UpdateTable()
        {
            TableList = new List<ImageCell>();
            

            foreach (CompletePicture item in HiddenPictureListView.ItemsSource)
            {
                ImageCell NewCell = new ImageCell
                {
                    Text = item.DateOfUpload.ToLocalTime().ToString("dd MMM yyyy HH:mm",
                        CultureInfo.CreateSpecificCulture("da-DA")),

                    ImageSource = ConvertByteArrayToImageSource(item.PictureData),
                    Detail = item.PictureComment

                };
                NewCell.CommandParameter = item.PictureID;

                NewCell.SetBinding(ImageCell.CommandProperty,new Binding("OpenFullPictureView"));
                


                TableList.Add(NewCell);
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

        /// <summary>
        /// Binding mellem den synlige liste og den skjulte liste 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiddenPictureListView_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (HiddenPictureListView.ItemsSource != null)
                {
                    UpdateTable();
                }

        }

        /// <summary>
        /// Metoden kaldes når nye elementer tilføjes til listen - derefter opdates hele listen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void HiddenPictureListView_OnItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
                UpdateTable();
                NoItemsInTableView = false;
        }


        /// <summary>
        /// Metode som kaldes når siden laves 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PictureListView_OnAppearing(object sender, EventArgs e)
        {
            PictureListViewModel.UpdateTableOnAppearingCommand.Execute(null);
        }

        /// <summary>
        /// Metoden kaldes når der tilføjes et nyt billede til listen 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HiddenPictureListView_OnChildAdded(object sender, ElementEventArgs e)
        {
            UpdateTable();
            NoItemsInTableView = false;
        }
    }
}