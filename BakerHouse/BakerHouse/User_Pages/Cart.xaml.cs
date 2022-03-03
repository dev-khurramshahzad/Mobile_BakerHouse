using BakerHouse.User_Pages.Model;
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
    public partial class Cart : ContentPage
    {
        public Cart()
        {
            InitializeComponent();
            try
            {
                List<Cart_VM> CartDatalist = new List<Cart_VM>();

                foreach (var item in App.Cart)
                {
                    var prod = App.db.Table<Models.Items>().FirstOrDefault(x => x.ItemID == item.ProductFID);
                    var cat = App.db.Table<Models.Categories>().FirstOrDefault(x => x.CatID == prod.CatFID);




                    CartDatalist.Add(new Cart_VM
                    {
                        Name = prod.ItemName,
                        Category = cat.CatName,

                        Quantity = item.UnitQuantity,
                        Picture = prod.ItemImage,
                        Price = prod.SPrice,

                        Total = (prod.SPrice * item.UnitQuantity),



                    });



                }
                CartList.ItemsSource = CartDatalist;

            }
            catch (Exception ex)
            {

                DisplayAlert("Message", " Some thing wrong...\n\n errors Details.." + ex.Message, "Ok");

            }
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

        }
    }
}