
using BakerHouse.View_Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CartPageControls : ContentPage
    {

        public CartPageControls()
        {
            InitializeComponent();
            try
            {
                UpdateCartAsync();
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }


        }

        async Task UpdateCartAsync()
        {

            List<imageCell_VM> CartItems = new List<imageCell_VM>();
            double? Amount = 0;
            foreach (var item in App.Cart)
            {
                var prod = App.db.Table<Models.Items>().FirstOrDefault(x => x.ItemID == item.ProductFID);

                double? total = prod.SPrice * (item.UnitQuantity);
                Amount += total;

                CartItems.Add(new imageCell_VM
                {
                    ID = prod.ItemID,
                    image = prod.ItemImage,
                    Name = prod.ItemName + "  - Rating: " + prod.Rating,
                    Detail = "Rs. " + prod.SPrice + " X  " + item.UnitQuantity + " = Total Rs. " + total.ToString()
                });
            }


            App.Total = Amount;

            lblTotal.Text = Amount.ToString();
            DataList.ItemsSource = CartItems;

        }


        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            if (App.Cart.Count < 1)
            {
                await DisplayAlert("Message", "Cart Page is Empty Please add at least one item in cart", "OK");
                return;
            }

            if (App.LoggedInUser == null)
            {
                var q = await DisplayAlert("Message", "You have to login for to place order.\n\nLog in Now?","Yes","No");
                if (q)
                {
                    await Navigation.PushAsync(new LoginUser());
                }
            }
            else
            {
                await Navigation.PushAsync(new ConfirmOrder());
            }



            
        }


        private async void btnRemove_Clicked(object sender, EventArgs e)
        {
            try
            {
                ImageButton btn = sender as ImageButton;
                var item = btn.CommandParameter as imageCell_VM;

                for (int i = 0; i < App.Cart.Count; i++)
                {
                    if (App.Cart[i].ProductFID == item.ID)
                    {
                        var res = await DisplayAlert("Question", "Are you sure you want to remove " + item.Name + "?", "Yes", "No");
                        if (res)
                        {
                            App.Cart.RemoveAt(i);
                        }
                    }
                }

                UpdateCartAsync();

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }

        }
    }
}