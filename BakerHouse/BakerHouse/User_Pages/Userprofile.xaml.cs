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
    public partial class Userprofile : ContentPage
    {
        public Userprofile()
        {
            InitializeComponent();

            lblEmail.Text = App.LoggedInUser.Email;
            lblName.Text = App.LoggedInUser.Name;
            lblPassword.Text = App.LoggedInUser.Password;
            lblPhone.Text = App.LoggedInUser.phone;
            lblAddress.Text = App.LoggedInUser.Address;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Clicked(object sender, EventArgs e)
        {

        }

        private void btnManagecat_Clicked(object sender, EventArgs e)
        {

        }

        private void btnAddCategory_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}