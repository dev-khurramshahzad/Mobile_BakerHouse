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
    public partial class Payment : ContentPage
    {
        public static bool PaymentStatus = false;
        public Payment()
        {
            InitializeComponent();

            string uri = "https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_xclick&amount=" + (App.Total / 160.45) + "&business=JanjuaTailors@Shop.com&item_name=Electronics_items";
            WebPageViewer.Source = uri;
        }

        private async void WebPageViewer_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.Contains("commit"))
            {
                PaymentStatus = true;
                await DisplayAlert("Message", "Payment is successfully", "OK");
            }
            else
            {
                PaymentStatus = false;
            }
            Loading.IsVisible = false;
        }

        private void WebPageViewer_Navigating(object sender, WebNavigatingEventArgs e)
        {
            
            Loading.IsVisible = true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (PaymentStatus == true)
            {
                await Navigation.PushAsync(new Success());
            }
            else
            {
                await DisplayAlert("Message", "Payment is Not Confirmed. Please Try Again..", "OK");
            }
        }
    }
}