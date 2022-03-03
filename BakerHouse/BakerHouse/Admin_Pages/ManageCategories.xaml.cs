using BakerHouse.Models;
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
    public partial class ManageCategories : ContentPage
    {
        public ManageCategories()
        {
            InitializeComponent();
            try
            {
                DataList.ItemsSource = App.db.Table<Categories>().ToList();
            }
            catch (Exception)
            {


            }


        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Categories;
            var response = await DisplayActionSheet("Action", "Close", "", "View", "Edit", "Delete", "Available", "No Available", "Short");
            if (response == "View")
            {


                await DisplayAlert("Category Details:",

                    " Category Name:" + item.CatName +
                    "\nCategory Details:" + item.CatDetails +
                    "\nCategory status:" + item.Status,

                    "ok");

            }
            if (response == "edit")
            {

            }
            if (response == "Available")
            {
                item.Status = "Available";
                App.db.Update(item);
                await DisplayAlert("Message", item.CatName + " status is updated  successfully", "OK");


            }
            if (response == "Not Available")
            {
                item.Status = "Not Available";
                App.db.Update(item);
                await DisplayAlert("Message", item.CatName + " status is updated  successfully", "OK");

            }
            if (response == "Short")
            {
                item.Status = "short";
                App.db.Update(item);
                await DisplayAlert("Message", item.CatName + " status is updated  successfully", "OK");

            }
            if (response == "Delete")
            {

                var resp = await DisplayAlert("question", "Are you sure you want to delete" + item.CatName, "YES", "NO");

                if (resp)
                {
                    App.db.Delete(item);
                    DataList.ItemsSource = App.db.Table<Categories>().ToList();
                    await DisplayAlert("Message", item.CatName + " Deleted successfully", "OK");

                }
               
            }



        }
    }
}
        