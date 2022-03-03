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
    public partial class Items : ContentPage
    {
        public Items(int id)
        {
            InitializeComponent();
            try
            {
                DataList.ItemsSource = App.db.Table<Models.Items>().Where(X => X.CatFID == id).ToList();
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", " Some thing wrong...\n\n errors Details.." + ex.Message, "Ok");


            }

        }


        private void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var item = e.Item as Items;

        }

        private void DataList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var item = btn.CommandParameter as Models.Items;


            int Quantity = 0;
            var QtyRaw = await DisplayActionSheet("Select Quantity", "Close", "", "1", "2", "3", "4", "5", "6", "7", "10", "15");
            if (QtyRaw != "Other" && QtyRaw != "Close" && QtyRaw != null)
            {
                Quantity = int.Parse(QtyRaw);
            }
            else
            {
                await DisplayAlert("Message", "Please select quantity at least 1", "OK");
                return;
            }

            int index = -1;
            for (int i = 0; i < App.Cart.Count; i++)
            {
                if (item.ItemID == App.Cart[i].ProductFID)
                {
                    index = i;
                    var ques = await DisplayAlert("Message", item.ItemName + " is already entered in Cart do you want to increase the quantity of already entered item?", "Yes", "No");
                    if (ques)
                    {
                        App.Cart[index].UnitQuantity += Quantity;
                        await DisplayAlert("Message", item.ItemName + " quantity increased... ", "OK");

                    }
                }
            }

            if (index == -1)
            {
                App.Cart.Add(new OrderDetail { ProductFID = item.ItemID, UnitQuantity = Quantity });
                await DisplayAlert("Message", item.ItemName + " is added to cart... ", "OK");

            }


        }

        private async void Button_Clicked2(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            var item = btn.CommandParameter as Models.Items;
            await DisplayAlert("Category Details:",

                                "Name:" + item.ItemName +
                                "\nDetails:" + item.ItemDetail +
                                "\nstatus:" + item.ItemStatus,

                                "ok");
        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CartPageControls());

        }

    }
}




