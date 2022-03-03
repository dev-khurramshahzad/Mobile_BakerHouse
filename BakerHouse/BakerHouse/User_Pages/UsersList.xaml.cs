using BakerHouse.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.UserPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UsersList : ContentPage
    {
        Users item = new  Users();
        public UsersList()
        {
            InitializeComponent();
            var db = new SQLiteConnection(App.dbPath);
            DataList.ItemsSource = db.Table<Users>().ToList();
              
        }

        private async void MenuItem_ClickedView(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            var item = menu.CommandParameter as Users;
          await   DisplayAlert("Detail",

              " Name :"  + item.Name + "\n" +
               " Email :" + item.Email+ "\n" +
                " Password :" + item.Password + "\n" +
                 " Phone :" + item.phone + "\n"
              , "ok");

        }

        private  void MenuItem_ClickedEdit(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
             item = menu.CommandParameter as Users;
            txtEmail.IsVisible = true;
            txtName.IsVisible = true;
            txtPass.IsVisible = true;
            txtphone.IsVisible = true;
            btnUpdate.IsVisible = true;

            txtEmail.Text = item.Email;
            txtName.Text = item.Name;
            txtPass.Text = item.Password;
            txtphone.Text = item.phone;









        }
        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            item.Email = txtEmail.Text;
            item.Password = txtPass.Text;
            item.Name = txtName.Text;
            item.phone = txtphone.Text;

            txtEmail.IsVisible = false;
            txtName.IsVisible = false;
            txtPass.IsVisible = false;
            txtphone.IsVisible = false;
            btnUpdate.IsVisible = false;

            var db = new SQLiteConnection(App.dbPath);
            db.Update(item);
            DataList.ItemsSource = db.Table<Users>().ToList();
            await DisplayAlert("Updated", "Data updated successfully.....", "ok");
        }





        private async void MenuItem_ClickedDelete(object sender, EventArgs e)
        {
            MenuItem menu = sender as MenuItem;
            var item = menu.CommandParameter as Users;
         var q =  await DisplayAlert("Question", "Are you sure you want to delete" + item.Name + "?", "Yes", "No");

            if (q)
            {
                var db = new SQLiteConnection(App.dbPath);
                db.Delete(item);
               

                DataList.ItemsSource = db.Table<Users>().ToList();
                await DisplayAlert("Message", item.Email + "Deleted successfully", "ok");

            }
           






        }

       
    }
       


} 