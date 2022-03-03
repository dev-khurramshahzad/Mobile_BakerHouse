using BakerHouse.AdminPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.Admin_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminSidebarMaster : ContentPage
    {
        public ListView ListView;

        public AdminSidebarMaster()
        {
            InitializeComponent();

            BindingContext = new AdminSidebarMasterViewModel();
            ListView = MenuItemsListView;
        }

        class AdminSidebarMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<AdminSidebarMasterMenuItem> MenuItems { get; set; }

            public AdminSidebarMasterViewModel()
            {
                MenuItems = new ObservableCollection<AdminSidebarMasterMenuItem>(new[]
                {
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "Admin Home", TargetType = typeof(AdminHome) },
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "Create Categories", TargetType = typeof(CreateCategories) },
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "Manage Categories", TargetType = typeof(ManageCategories) },
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "Create Items", TargetType = typeof(CreateItems) },
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "Manage Items", TargetType = typeof(ManageItems) },
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "OrdersReport", TargetType = typeof(OrdersReport) },
                    new AdminSidebarMasterMenuItem { Id = 0, Title = "P/L Report", TargetType = typeof(Profit_LossReport) },

                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}