using BakerHouse.Models;
using BakerHouse.UserPages;
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

namespace BakerHouse.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginUser : ContentPage
    {

        public LoginUser()
        {
            InitializeComponent();
        }


        private async void btn_Clicked(object sender, EventArgs e)
        {


            try
            {

                // Required Field Validator =======================================================================
                if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPass.Text))
                {
                    await DisplayAlert("Message", "Please Enter Email and Password", "OK");
                    return;
                }

                //Email Validation =======================================================================
                var emailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
                if (!Regex.IsMatch(txtEmail.Text, emailPattern))
                {
                    await DisplayAlert("Error!", "Please Enter a Valid Email", "Close");
                    return;
                }


                App.db.CreateTable<Users>();

                var check = App.db.Table<Users>().FirstOrDefault(x => x.Email == txtEmail.Text && x.Password == txtPass.Text && x.Type == "Customer");
                if (check == null)
                {
                    await DisplayAlert("Message", "Email or  password incorrect. Please Try Again", "OK");
                    return;
                }
                else
                {
                    if (cbRemember.IsChecked == true)
                    {
                        App.db.DropTable<Remember>();
                        App.db.CreateTable<Remember>();
                        App.db.Insert(new Remember { UserID = check.UserID });
                    }

                    App.LoggedInUser = check;
                    App.Current.MainPage = new UserSidebar();
                }

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", ex.Message, "OK");
            }
        }


        private async void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ForgotPassword());
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateAccount());
        }

        private void btnReset_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Start_your_business());
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            App.Current.MainPage = new UserSidebar();
        }
        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await this.DisplayAlert("Alert!", "Do you really want to exit?", "Yes", "No");

                if (result)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }
    }
}