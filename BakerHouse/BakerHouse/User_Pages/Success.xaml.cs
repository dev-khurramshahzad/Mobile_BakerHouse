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
    public partial class Success : ContentPage
    {
        public Success()
        {
            InitializeComponent();
            App.Cart.Clear();




        }

        private void btnCurrentLocation_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserSidebar();
        }
    }
}