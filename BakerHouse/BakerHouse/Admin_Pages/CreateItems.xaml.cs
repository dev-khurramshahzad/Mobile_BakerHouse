  using BakerHouse.Models;
using BakerHouse.User_Pages;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Items = BakerHouse.Models.Items;

namespace BakerHouse.Admin_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateItems : ContentPage
    {
        public static string PicPath = "camera.png";
        public CreateItems()
        {
            InitializeComponent();
            try
            {
                ddlCat.ItemsSource = App.db.Table<Categories>().Select(x => x.CatName).ToList();

            }
            catch (Exception ex)
            {
                DisplayAlert("Message", " Something went wrong\n" + ex.Message, "Ok");

            }
        }
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                var response = await DisplayActionSheet("Select Image Source", "Close", "", "From Gallery", "From Camera");
                if (response == "From Camera")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsTakePhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Take Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new StoreCameraMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
                if (response == "From Gallery")
                {
                    await CrossMedia.Current.Initialize();
                    if (!CrossMedia.Current.IsPickPhotoSupported)
                    {
                        await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
                        return;
                    }

                    var mediaOptions = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Medium
                    };

                    var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

                    if (SelectedImg == null)
                    {
                        await DisplayAlert("Error", "Error Picking Image...", "OK");
                        return;
                    }

                    PicPath = SelectedImg.Path;
                    PreviewPic.Source = SelectedImg.Path;


                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Something Went wrong \n" + ex.Message, "OK");

            }
        }




        private void Button_Clicked(object sender, EventArgs e)
        {
            if (ddlCat.SelectedItem == null)
            {
                DisplayAlert("Message", " Please Enter Category ", "Ok");
                return;

            }
            if (txtPPrice.Text == null || txtSprice.Text == null)
            {
                DisplayAlert("Message", " Please fill the required fields ", "Ok");
                return;

            }
            var selected = ddlCat.SelectedItem.ToString();
            var Cat = App.db.Table<Categories>().FirstOrDefault(X => X.CatName == selected);

           Items newItems = new Items
            {

                ItemDetail = txtDetail.Text,
                ItemName = txtName.Text,
                ItemImage = PicPath,
                PPrice = float.Parse(txtPPrice.Text),
                CatFID = Cat.CatID,

                SPrice = float.Parse(txtSprice.Text),
                Rating = int.Parse(txtRating.Text),
                ItemStatus = txtStatus.Text
            };
            App.db.CreateTable<Items>();
            App.db.Insert(newItems);
            DisplayAlert("Message", "Item Added successfully ", "Ok");
        }


    }
}