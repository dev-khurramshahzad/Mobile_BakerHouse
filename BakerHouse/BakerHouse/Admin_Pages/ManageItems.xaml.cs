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
    public partial class ManageItems : ContentPage
    {
        public ManageItems()
        {
            InitializeComponent();
            try
            {
                DataList.ItemsSource = App.db.Table<Items>().ToList();
            }
            catch (Exception)
            {


            }


        }

        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Items;
            var response = await DisplayActionSheet("Action", "Close",  "", "View", "Edit,", "Delete", "Disable", "Enable");
            if (response == "View")
            {


                await DisplayAlert("Category Details:",

                    " Category Name:" + item.ItemName +
                    "\nCategory Details:" + item.ItemDetail +
                    "\nCategory status:" + item.ItemStatus,

                    "ok");

            }
            if (response == "Delete")
            {

                var resp = await DisplayAlert("question", "Are you sure you want to delete" + item.ItemName, "YES", "NO");

                if (resp)
                {
                    App.db.Delete(item);
                   DataList.ItemsSource = App.db.Table<Categories>().ToList();
                    await DisplayAlert("Message", item.ItemName + " Deleted successfully", "OK");

                }



            }



        }
    }
}