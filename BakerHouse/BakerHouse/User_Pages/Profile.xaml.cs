using BakerHouse.Models;
using SQLite;
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
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (txtName.Text == null || txtAddress.Text == null || txtPhone.Text == null)
            {
                await DisplayAlert("Message", "Please enter all required data", "OK");
                return;
            }
           
            var db = new SQLiteConnection(App.dbPath);
            db.CreateTable<seller>();

            seller s = new seller()
            {
              
                Name = txtName.Text,
                phone = txtPhone.Text,
                Address = txtAddress.Text,
                

            };
            db.Insert(s);
            await DisplayAlert("Success", "Submitted Successfully...", "OK");
        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
        }
        private void txtPhone_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 11 || e.NewTextValue.Length > 14)
            {
                lblPhoneVali.IsVisible = true;
                lblPhoneVali.Text = "InValid Phone! Missing digit(s)";
                lblPhoneVali.TextColor = Color.Red;
            }
            else
            {
                lblPhoneVali.Text = "Valid Phone";
                lblPhoneVali.TextColor = Color.Green;
            }
        }
    }
}
