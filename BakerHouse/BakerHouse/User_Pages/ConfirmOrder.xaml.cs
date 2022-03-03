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

        //public async void GetLocationMap()
        //{
        //    try
        //    {
        //        await DisplayAlert("Message", "GPS and Internet will be used to access your current location please confirm that you have enabled the GPS and Internet.", "OK");

        //        var locator = CrossGeolocator.Current;
        //        var position = await locator.GetPositionAsync();

        //        Position TempPos = new Position(position.Latitude, position.Longitude);
        //        Pos = TempPos;

        //        MapView.Pins.Add(new Pin { Label = "Your Location", Position = Pos });

        //        MapView.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));


        //    }
        //    catch (Exception ex)
        //    {

        //        await DisplayAlert("Message", "Something went wrong it may be due to internet or location permission please check that you have an active internet connection and also enabled GPS. Sorry for the inconveninace", "OK");
        //    }
        //}

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



                // SMS SENDING ================================================================
                //var uri = "https://lifetimesms.com/json?api_token=a976f26420e66b7ed08aac4034b32b7f68ec633295&api_secret=Testing&to=03042409294&from=Sport&message=Booking Confirmation\n\n\nDear is Conformed your TicketID = 2020-SPEvents-000";

                // WebRequest request = WebRequest.Create(uri);
                // WebResponse response = request.GetResponse();



                // EMAIL SENDING ================================================================
                //MailMessage mail = new MailMessage();
                //mail.To.Add("rthegraetu125@gmail.com");
                //mail.From = new MailAddress("hayaashahzadi@gmail.com", "Order Conirmation", System.Text.Encoding.UTF8);
                //mail.Subject = "Order Confirmation";
                //mail.SubjectEncoding = System.Text.Encoding.UTF8;

                //mail.Body = "Booking Confirmation\n\n\nDear Your Order is Confirmed...";
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                //mail.IsBodyHtml = true;
                //mail.Priority = MailPriority.High;

                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                //client.Credentials = new System.Net.NetworkCredential("hayaashahzadi@gmail.com", "iQRA@124");
                //client.EnableSsl = true;

                //client.Send(mail);


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