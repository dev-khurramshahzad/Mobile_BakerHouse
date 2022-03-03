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
    public partial class ViewItems : ContentPage
    {
        public ViewItems()
        {
            InitializeComponent();

            App.db.CreateTable<Models.Categories>();
            if (App.db.Table<Categories>().ToList().Count == 0)
            {
                App.db.Insert(new Models.Categories { CatID = 1, CatName = "pastries", CatDetails = " Details Here", CatImage = "Copy.jpg" });
                //App.db.Insert(new Models.Categories { CatID = 2, CatName = "Cakes", CatDetails = " Details Here", CatImage = "Copy.jpg" });
                //App.db.Insert(new Models.Categories { CatID = 3, CatName = "Cookies", CatDetails = " Details Here", CatImage = "Copy.jpg" });


            }
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
    }
}