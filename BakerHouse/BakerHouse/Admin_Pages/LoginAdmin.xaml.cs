using BakerHouse.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.Admin_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginAdmin : ContentPage
    {

        public LoginAdmin()
        {
            InitializeComponent();
        }


        private async void btn_Clicked(object sender, EventArgs e)
        {

            try
            {

                //Email Validation =======================================================================
                var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
                if (!(Regex.IsMatch(txtEmail.Text, emailPattern)))
                {
                    await DisplayAlert("Error!", "Please Enter a Valid Email", "Close");
                    return;
                }


                // Required Field Validator =======================================================================
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
                {
                    await DisplayAlert("Message", "Please Enter Email and Password", "OK");
                    return;
                }


                App.db.CreateTable<Users>();
                var t = App.db.Table<Users>().ToList();

                var check = App.db.Table<Users>().FirstOrDefault(x => x.Email == txtEmail.Text && x.Password == txtPass.Text && x.Type == "Admin");
                if (check == null)
                {
                    await DisplayAlert("Message", "Email or  password incorrect. Please Try Again", "OK");
                    return;
                }
                else
                {
                    App.Current.MainPage = new AdminSidebar();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "OK");
            }
        }


        private async void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            try
            {
                 var check = App.db.Table<Models.Users>().FirstOrDefault(x => x.Email == txtEmail.Text);

                if (check == null)
                {
                    await DisplayAlert("Message", "The email you have entered is not registered.", "OK");
                    return;
                }

                
            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connectiony . \nError Details : " + ex.Message, "OK");
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new CreateAccount());
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {

        }
    }
}