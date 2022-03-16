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

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            App.db.DropTable<Remember>();
            App.LoggedInUser = null;
            App.Current.MainPage = new Home_Page();
        }
    }
}
