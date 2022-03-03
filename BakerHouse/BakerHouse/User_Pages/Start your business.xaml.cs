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
    public partial class Start_your_business : ContentPage
    {
        public Start_your_business()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //App.Current.MainPage = new Profile();
            Navigation.PushAsync(new Profile());
        }
    }
}