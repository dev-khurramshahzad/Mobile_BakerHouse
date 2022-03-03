using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BakerHouse.User_Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserSidebarMaster : ContentPage
    {
        public ListView ListView;

        public UserSidebarMaster()
        {
            InitializeComponent();

            BindingContext = new UserSidebarMasterViewModel();
            ListView = MenuItemsListView;
        }

        class UserSidebarMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<UserSidebarMasterMenuItem> MenuItems { get; set; }

            public UserSidebarMasterViewModel()
            {
                MenuItems = new ObservableCollection<UserSidebarMasterMenuItem>(new[]
                {
                    new UserSidebarMasterMenuItem { Id = 0, Title = "Home",TargetType =typeof(Home_Page) },
                    new UserSidebarMasterMenuItem { Id = 0, Title = "About us",TargetType =typeof(About) },
                    new UserSidebarMasterMenuItem { Id = 0, Title = "Contact Us",TargetType =typeof(Contact) },
                    new UserSidebarMasterMenuItem { Id = 0, Title = "User Profile",TargetType =typeof(Userprofile) },

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