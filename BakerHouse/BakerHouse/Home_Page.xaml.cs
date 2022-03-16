using BakerHouse.Admin_Pages;
using BakerHouse.Models;
using BakerHouse.User_Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home_Page : ContentPage
    {
        public Home_Page()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.db.CreateTable<Remember>();
            var check = App.db.Table<Remember>().ToList();
            if (check.Count == 1)
            {
                int id = check[0].UserID;
                App.LoggedInUser = App.db.Table<Users>().FirstOrDefault(x => x.UserID == id);
                App.Current.MainPage = new UserSidebar();
            }
            else
            {
                App.Current.MainPage = new NavigationPage(new LoginUser());
            }

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new LoginAdmin());
        }

        private async void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            App.db.DropTable<Models.Users>();
            App.db.CreateTable<Models.Users>();
            App.db.Insert(new Models.Users { UserID = 1, Name = "Admin", Email = "admin@admin.com", Password = "admin", Type = "Admin" });
            App.db.Insert(new Models.Users { UserID = 1, Name = "Customer", Email = "customer@customer.com", Password = "customer", Type = "Customer", Address = "Gujranwala Main City", phone="0300-00000000" });

            App.db.DropTable<Models.Categories>();
            App.db.CreateTable<Models.Categories>();
            App.db.Insert(new Models.Categories {CatID=2, CatName = "Cakes", CatImage = "blackcake.jpg", CatDetails = "All kinds of Cakes", Status="Available"  });
            App.db.Insert(new Models.Categories {CatID=3, CatName = "Cookies", CatImage = "cookies_210706.jpg", CatDetails = "All kinds of Cppkies", Status="Available"  });

            App.db.DropTable<Models.Items>();
            App.db.CreateTable<Models.Items>();
            App.db.Insert(new Models.Items { ItemID=1, CatFID=2, ItemName= "Brook 1", PPrice=1000, SPrice=1350, ItemImage= "brooke.jpg", ItemEXPDate="12/12/2022",ItemMFGDate="1/1/2022", Rating=5,  ItemDetail="Yummy, Creemy and Delicious to eat.", ItemStatus="Available" });
            App.db.Insert(new Models.Items { ItemID=2, CatFID=2, ItemName= "Cake 1", PPrice=1000, SPrice=1350, ItemImage= "blackcake.jpg", ItemEXPDate="12/12/2022",ItemMFGDate="1/1/2022", Rating=5,  ItemDetail="Yummy, Creemy and Delicious to eat.", ItemStatus="Available" });
            App.db.Insert(new Models.Items { ItemID=3, CatFID=3, ItemName= "Cookie 1", PPrice=1000, SPrice=1350, ItemImage= "cookies_210706.jpg", ItemEXPDate="12/12/2022",ItemMFGDate="1/1/2022", Rating=5,  ItemDetail="Yummy, Creemy and Delicious to eat.", ItemStatus="Available" });

            await DisplayAlert("Message", "Application is restored to defaults", "OK");
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
