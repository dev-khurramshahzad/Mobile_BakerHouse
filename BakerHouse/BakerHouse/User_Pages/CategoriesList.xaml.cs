using BakerHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CategoriesList : ContentPage
    {
        public CategoriesList()
        {
            InitializeComponent();
            //App.db.CreateTable<Models.Categories>();
            //if (App.db.Table<Categories>().ToList().Count == 0)
            //{
            //    App.db.Insert(new Models.Categories { CatID = 1, CatName = "Pastries", CatDetails = " Details Here", CatImage = "Copy.jpg" });
            //    App.db.Insert(new Models.Categories { CatID = 2, CatName = "Cakes", CatDetails = " Details Here", CatImage = "Copy.jpg" });
            //    App.db.Insert(new Models.Categories { CatID = 3, CatName = "CupCakes", CatDetails = " Details Here", CatImage = "Copy.jpg" });


            //}
            try
            {
               DataList.ItemsSource = App.db.Table<Categories>().ToList();

            }
            catch (Exception ex)
            {
                DisplayAlert("Message", " Some thing went wrong...\n\n errors Details.." + ex.Message, "Ok");


            }


        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {


            try
            {
                var item = (Models.Categories)e.Item;
                await Navigation.PushAsync(new Items(item.CatID));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", " Some thing wrong...\n\n errors Details.." + ex.Message, "Ok");

            }
        }
             protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        



        }
    }
}
