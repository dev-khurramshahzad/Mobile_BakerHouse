using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfirmOrder : ContentPage
    {
        //public static Position Pos = new Position();
        public ConfirmOrder()
        {
            InitializeComponent();

        }

      

        private async void btnCurrentLocation_Clicked(object sender, EventArgs e)
        {
            try
            {

                Models.Order order = new Models.Order()
                {
                    OrderDate = DateTime.Now.Date,
                    OrderTime = DateTime.Now.TimeOfDay,
                    Status = "Pending",
                    UserID = 1,
                };
                App.db.CreateTable<Models.Order>();
                App.db.CreateTable<Models.OrderDetail>();
                App.db.Insert(order);

                int LastOrderID = App.db.Table<Models.Order>().Max(x => x.OrderID);
                var Usr = App.db.Table<Models.Users>().Where(x => x.UserID == App.LoggedInUser.UserID).FirstOrDefault();

                foreach (var item in App.Cart)
                {
                    item.OrderFID = LastOrderID;
                    App.db.Insert(item);
                }

              

                await Navigation.PushAsync(new Success());
                //await Navigation.PushAsync(new Payment());

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }


        }
    }
}