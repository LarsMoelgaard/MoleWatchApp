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
    /// <summary>
    /// Opretter view for listen med billeder for et modermærke 
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionListView : ContentPage
    {

        private List<ImageCell> CollectionCells;

        public CollectionListView()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Metoden opdatere listen med billeder 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void UpdateCollectionListView(object sender, PropertyChangedEventArgs e)
        {
            if (HiddenCollectionListView.ItemsSource != null)
            {
                UpdateTable();
            }
        }


        /// <summary>
        /// Metoden opdatere tablesection i view (XAML)
        /// </summary>
        public void UpdateTable()
        {
            CollectionCells = new List<ImageCell>();
            

            foreach (CollectionWithThumbnail item in HiddenCollectionListView.ItemsSource)
            {
                string DateText = "";
                if (item.Collection.PictureList.Count != 0)
                {
                    DateText = item.Collection.PictureList[item.Collection.PictureList.Count-1].DateOfUpload
                        .ToLocalTime().ToString("dd MMM yyyy HH:mm",
                            CultureInfo.CreateSpecificCulture("da-DA"));
                }
                else
                {
                    DateText = "Ingen billeder i samling";
                }

                Color titleColor = item.Collection.IsMarked ? Color.Crimson : Color.DodgerBlue;


                string DetailText = item.Collection.Location.BodyPart + ": " + DateText;

                ImageCell NewCell = new ImageCell
                {
                    Text = item.Collection.CollectionName,
                    TextColor = titleColor,

                    
                    ImageSource = item.CollectionPictureData,
                    Detail = DetailText,

                };
                
                NewCell.CommandParameter = item.Collection;

                NewCell.SetBinding(ImageCell.CommandProperty, new Binding("ExistingCollectionListClicked"));

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

        /// <summary>
        /// Metoden opdatere tablesektion når der tilføjes elementer til den 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CollectionListView_OnAppearing(object sender, EventArgs e)
        {
            CollectionListVModel.UpdateCollections.Execute(null);
            if (HiddenCollectionListView.ItemsSource != null)
            {
                UpdateTable();
            }
        }
    }
}