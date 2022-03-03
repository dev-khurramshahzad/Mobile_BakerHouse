using BakerHouse.Models;
using SQLite;
using System;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateAccount : ContentPage
    {
        public CreateAccount()
        {
            InitializeComponent();
        }

        //private async void btnPick_Clicked(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        await CrossMedia.Current.Initialize();
        //        if (!CrossMedia.Current.IsPickPhotoSupported)
        //        {
        //            await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
        //            return;
        //        }

        //        var mediaOptions = new PickMediaOptions()
        //        {
        //            PhotoSize = PhotoSize.Medium
        //        };

        //        var SelectedImg = await CrossMedia.Current.PickPhotoAsync(mediaOptions);

        //        if (SelectedImg == null)
        //        {
        //            await DisplayAlert("Error", "Error Picking Image...", "OK");
        //            return;
        //        }

        //        PreviewImage.Source = SelectedImg.Path;

        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", ex.Message, "OK");
        //    }
        //}

        //private async void btnTake_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        await CrossMedia.Current.Initialize();
        //        if (!CrossMedia.Current.IsPickPhotoSupported)
        //        {
        //            await DisplayAlert("Error", "Phone is not Pick Photo Supported", "OK");
        //            return;
        //        }

        //        var mediaOptions = new StoreCameraMediaOptions()
        //        {
        //            PhotoSize = PhotoSize.Medium
        //        };

        //        var SelectedImg = await CrossMedia.Current.TakePhotoAsync(mediaOptions);

        //        if (SelectedImg == null)
        //        {
        //            await DisplayAlert("Error", "Error Picking Image...", "OK");
        //            return;
        //        }

        //        PreviewImage.Source = ImageSource.FromStream(() => SelectedImg.GetStream());

        //    }
        //    catch (Exception ex)
        //    {
        //        await DisplayAlert("Error", ex.Message, "OK");
        //    }
        //}


        private void btncreate_Clicked(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SQLiteConnection(App.dbPath))
                {

                    //============ For Checking Empty Fields ==========================================
                    if (txtEmail.Text == null || txtName.Text == null || txtPass.Text == null || txtPhone.Text == null || txtAddress.Text == null)
                    {
                        DisplayAlert("Message", "Please fill-up all the required fields.", "OK");
                        return;
                    }



                    //====================================================
                    db.CreateTable<Models.Users>();
                    var check = db.Table<Models.Users>().FirstOrDefault(x => x.Email == txtEmail.Text);
                    if (check != null)
                    {
                        DisplayAlert("Message", "This Email is Already Registered", "OK");
                        return;
                    }



                    //====================================================
                    if (txtPass.Text != txtCPass.Text)
                    {
                        DisplayAlert("Message", "Password do not match", "OK");
                        return;
                    }



                    Models.Users u = new Models.Users()
                    {
                        Name = txtName.Text,
                        Email = txtEmail.Text,
                        Password = txtPass.Text,
                        phone = txtPhone.Text,
                        Status = "Active",
                        Address = txtAddress.Text,
                        Type = "Admin",
                        //Details = txtDetails.Text

                    };
                    db.Insert(u);

                    DisplayAlert("Message", "Account for " + txtName.Text + " is created succuessfully.", "OK");

                    txtPhone.Text = "";
                    txtPass.Text = "";
                    txtName.Text = "";
                    txtEmail.Text = "";
                    txtAddress.Text = "";




                }
            }

            catch (Exception ex)
            {
                DisplayAlert("Message", ex.Message, "OK");

            }
        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {
            var EmailPattern = @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$";
            if (Regex.IsMatch(e.NewTextValue, EmailPattern))
            {
                lblPhoneVali.IsVisible = true;
                lblEmailVali.Text = "Valid Email";
                lblEmailVali.TextColor = Color.Green;
            }
            else
            {
                lblEmailVali.Text = "InValid Email! Email must contain @ and .com";
                lblEmailVali.TextColor = Color.Red;
            }

        }

        private void txtPhone_TextChanged(object sender, TextChangedEventArgs e)
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

        private void txtCNIC_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 13 || e.NewTextValue.Length > 13)
            {
                lblCNICVali.IsVisible = true;
                lblCNICVali.Text = "InValid CNIC #! Missing digit(s)";
                lblCNICVali.TextColor = Color.Red;
            }
            else
            {
                lblCNICVali.Text = "Valid CNIC #";
                lblCNICVali.TextColor = Color.Green;
            }
        }

        private void txtPass_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue.Length < 8)
            {
                lblPassVali.IsVisible = true;
                lblPassVali.Text = "Password should be of at least 8 charaters";
                lblPassVali.TextColor = Color.Red;
            }

            var PasswordPattern = @"^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])";
            if (Regex.IsMatch(e.NewTextValue, PasswordPattern))
            {
                lblPassVali.Text = "Strong Password!";
                lblPassVali.TextColor = Color.Green;
            }
            else
            {
                lblPassVali.Text = "Weak Password! Password should contain Uppercase Letter , Lowercase Letter, Number(s) and special characters [$@$!%*#?&]";
                lblPassVali.TextColor = Color.Red;
            }
        }

        private void txtAddress_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}