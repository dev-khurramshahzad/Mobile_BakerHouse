using BakerHouse.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.Admin_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Sub_SellerList : ContentPage
    {

        seller item = new seller();
        public Sub_SellerList()
        {
            InitializeComponent();
            var db = new SQLiteConnection(App.dbPath);
            DataList.ItemsSource = db.Table<seller>().ToList();
        }

        private async void MenuItem_ClickedView(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            var item = menu.CommandParameter as seller;

            await DisplayAlert("Details",

                "Name : " + item.Name + "\n" +
               
                "Address : " + item.Address + "\n" +
                "Phone : " + item.phone + "\n"

                , "OK");

        }

        private async void MenuItem_Clicked(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            var item = menu.CommandParameter as seller;

            var q = await DisplayAlert("Question", "Are you sure you want to delete "  + "?", "Yes", "No");
            if (q)
            {
                var db = new SQLiteConnection(App.dbPath);
                db.Delete(item);

                DataList.ItemsSource = db.Table<seller>().ToList();
                await DisplayAlert("Message", " deleted successfully...", "Ok");
            }
        }
    }
}