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

namespace BakerHouse.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderItems : ContentPage
    {

        public OrderItems(int id)
        {
            InitializeComponent();
            try
            {
                UpdateCartAsync(id);
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }

        }

       
        void  UpdateCartAsync(int id)
        {

            var AllOrdersDetails = App.db.Table<Models.OrderDetail>().Where(x => x.OrderFID == id).ToList();
            List<imageCell_VM> CartItems = new List<imageCell_VM>();
            double? Amount = 0;
            foreach (var item in AllOrdersDetails)
            {
                var prod = App.db.Table<Models.Items>().Where(a => a.ItemID == item.ProductFID).FirstOrDefault();
                double? total = prod.SPrice * (double.Parse(item.UnitQuantity.ToString()));
                Amount += total;

                

                CartItems.Add(new imageCell_VM
                 {
                    ID = prod.ItemID,
                    image = prod.ItemImage,
                    Name = prod.ItemName + "  - Rating: " + prod.Rating,
                    Detail = "Rs. " + prod.SPrice + " X  " + item.UnitQuantity + " = Total Rs. " + total.ToString()
                });
            }

            lblTotal.Text = Amount.ToString();
            DataList.ItemsSource = CartItems;


        }
        
    }
}