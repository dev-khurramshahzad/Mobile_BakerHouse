

using BakerHouse.Models;
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
    public partial class OrdersReport : ContentPage
    {
        public OrdersReport()
        {
            InitializeComponent();

            try
            {
                LoadOrders();
            }
            catch (Exception ex)
            {
                DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Details : " + ex.Message, "OK");
            }
        }

    
        public void LoadOrders()
        {
            var AllOrders = App.db.Table<Models.Order>().ToList().OrderByDescending(x => x.OrderDate).ThenByDescending(x => x.OrderTime);


            List<imageCell_VM> DataLst = new List<imageCell_VM>();
            foreach (var item in AllOrders)
            {
               var user = App.db.Table<Models.Users>().FirstOrDefault(X => X.UserID == item.UserID);

                DataLst.Add(new imageCell_VM
                {
                    ID = item.OrderID,
                    Name =user.Name+ " - " + item.Status,
                    Detail = DateTime.Parse(item.OrderDate.ToString()).ToShortDateString() + "  " + DateTime.Parse(item.OrderTime.ToString()).ToShortTimeString(),
                    image = "camera.png"
                });
            }

            DataList.ItemsSource = DataLst;

        }



        private async void DataList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var item = e.Item as imageCell_VM;

                var response = await DisplayActionSheet("Action", "Close", "", "Order Details", "Delivered", "Cancelled", "Pending", "Delete Order");
               
                if (response == "Order Details")
                {
                    await Navigation.PushAsync(new OrderItems(item.ID));
                }

                if (response == "Delivered")
                {
                    var order = App.db.Table<Order>().FirstOrDefault(X => X.OrderID == item.ID);
                    order.Status = "Delivered";

                    App.db.Update(order);
                    await DisplayAlert("Message", "Order status changed to Delivered", "OK");
                    LoadOrders();
                }

                if (response == "Cancelled")
                {
                    var order = App.db.Table<Order>().FirstOrDefault(X => X.OrderID == item.ID);
                    order.Status = "Cancelled";

                    App.db.Update(order);
                    await DisplayAlert("Message", "Order status changed to Cancelled", "OK");
                    LoadOrders();
                }

                if (response == "Pending")
                {
                    var order = App.db.Table<Order>().FirstOrDefault(X => X.OrderID == item.ID);
                    order.Status = "Pending";

                    App.db.Update(order);
                    await DisplayAlert("Message", "Order status changed to Pending", "OK");
                    LoadOrders();
                }

                if (response == "Delete Order")
                {
                    var order = App.db.Table<Order>().FirstOrDefault(X => X.OrderID == item.ID);
                    var OrderItems = App.db.Table<OrderDetail>().Where(X => X.OrderFID == order.OrderID).ToList();

                    App.db.Delete(order);
                    foreach (var i in OrderItems)
                    {
                        App.db.Delete(i);
                    }

                    LoadOrders();
                    await DisplayAlert("Message", "Order Deleted Successfully..", "OK");
                }





            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connection and GPS enabled. \nError Detals : " + ex.Message, "OK");
            }


        }
    }
}