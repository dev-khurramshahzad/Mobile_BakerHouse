using BakerHouse.Models;
using BakerHouse.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.AdminPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profit_LossReport : ContentPage
    {
        public Profit_LossReport()
        {
            InitializeComponent();

            var OrdersList = App.db.Table<OrderDetail>().ToList();

            List<imageCell_VM> CartItems = new List<imageCell_VM>();
            double? Profit = 0;
            double? Purchase = 0;
            double? Sale = 0;
            foreach (var item in OrdersList)
            {
                var prod = App.db.Table<Models.Items>().FirstOrDefault(x => x.ItemID == item.ProductFID);

                double? totalP = prod.PPrice * (item.UnitQuantity);
                Purchase += totalP;

                double? totalS = prod.SPrice * (item.UnitQuantity);
                Sale += totalS;

                Profit = Sale - Purchase;
                CartItems.Add(new imageCell_VM
                {
                    ID = prod.ItemID,
                    image = prod.ItemImage,
                    Name = prod.ItemName + "  - Rating: " + prod.Rating,
                    Detail = "Purchase Rs. = " + prod.PPrice * item.UnitQuantity + " Sale Rs. = " + prod.SPrice * item.UnitQuantity + " Profit Rs. " + ((prod.SPrice * item.UnitQuantity) - (prod.PPrice * item.UnitQuantity))
                });
            }



            lblPurch.Text = Purchase.ToString();
            lblSale.Text = Sale.ToString();
            lblP_L.Text = Profit.ToString();
            DataList.ItemsSource = CartItems;

        }
    }
}