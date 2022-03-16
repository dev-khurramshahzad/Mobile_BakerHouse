using BakerHouse;
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
    public partial class ForgotPassword : ContentPage
    {

        public ForgotPassword()
        {
            InitializeComponent();
        }


      

        private async void btnReset_Clicked(object sender, EventArgs e)
        {
            try
            {
                // Required Field Validator =======================================================================
                if (string.IsNullOrEmpty(txtEmailReset.Text))
                {
                    await DisplayAlert("Message", "Please Enter Email", "OK");
                    return;
                }

                var check = App.db.Table<BakerHouse.Models.Users>().FirstOrDefault(x => x.Email == txtEmailReset.Text);

                if (check == null)
                {
                    await DisplayAlert("Message", "The email you have entered is not registered.", "OK");
                    return;
                }

                Progress.IsRunning = true;

                // EMAIL SENDING ================================================================

                MailMessage mail = new MailMessage();
                mail.To.Add(check.Email);
                mail.From = new MailAddress("martquick30@gmail.com", "Password Forgotton", System.Text.Encoding.UTF8);
                mail.Subject = "Password Forgot Request";
                mail.SubjectEncoding = System.Text.Encoding.UTF8;

                mail.Body = "Dear Customer Your Current Login Details are as Follows : <br><br><br>Username = " + check.Email + " <br>Password = " + check.Password + " <br><br>Quick Mart Team";
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.Credentials = new System.Net.NetworkCredential("martquick30@gmail.com", "mart3048");
                client.EnableSsl = true;

                client.Send(mail);

                await DisplayAlert("Message", "Your Login Details are sent to your email address please find that in your inbox", "OK");
                await Navigation.PopAsync();


                Progress.IsRunning = false;
            }
            catch (Exception ex)
            {
                Progress.IsRunning = false;
                await DisplayAlert("Message", "Somthing went wrong this may be a problem with internet or application please ensure that you have a working internet connectiony . \nError Details : " + ex.Message, "OK");
            }
        }

       
    }
}