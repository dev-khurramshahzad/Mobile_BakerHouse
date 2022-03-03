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


                var db = new SQLiteConnection(App.dbPath);
                db.CreateTable<Users>();

                var check = db.Table<Users>().FirstOrDefault(x => x.Email == txtEmail.Text && x.Password == txtPass.Text && x.Type=="Customer");
                if (check == null)
                {
                    await DisplayAlert("Message", "Email or  password incorrect. Please Try Again", "OK");
                    return;
                }
                else
                {
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
            try
            {
                var check = App.db.Table<Models.Users>().FirstOrDefault(x => x.Email == txtEmail.Text);

                if (check == null)
                {
                    await DisplayAlert("Message", "The email you have entered is not registered.", "OK");
                    return;
                }

                // EMAIL SENDING ================================================================

                //MailMessage mail = new MailMessage();
                //mail.To.Add(check.Email);
                //mail.From = new MailAddress("foodsbaba.suplier@gmail.com", "Password Forgotton", System.Text.Encoding.UTF8);
                //mail.Subject = "Password Forgot Request";
                //mail.SubjectEncoding = System.Text.Encoding.UTF8;

                //mail.Body = "Dear Customer Your Current Login Details are as Follows : <br><br><br>Username = " + check.Email + " <br>Password = " + check.Password + " <br><br>Foods Baba";
                ////mail.Body = "Dear Customer Your Current Login Details are as Follows : <br><br><br>Username =   <br><br>Foods Baba";
                //mail.BodyEncoding = System.Text.Encoding.UTF8;
                //mail.IsBodyHtml = true;
                //mail.Priority = MailPriority.High;

                //SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                //client.Credentials = new System.Net.NetworkCredential("foodsbaba.suplier@gmail.com", "juttfood");
                //client.EnableSsl = true;

                //client.Send(mail);

                //await DisplayAlert("Message", "Your Login Details are sent to your email address please find that in your inbox", "OK");

                stkLogin.IsVisible = true;
                stkReset.IsVisible = false;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connectiony . \nError Details : " + ex.Message, "OK");
            }
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