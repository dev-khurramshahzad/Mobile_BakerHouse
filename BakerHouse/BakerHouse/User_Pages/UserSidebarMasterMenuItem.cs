using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakerHouse.User_Pages
{

    public class UserSidebarMasterMenuItem
    {
        public UserSidebarMasterMenuItem()
        {
            TargetType = typeof(UserSidebarMasterMenuItem);
        }
        public int Id { get; set; }
        public string Title { get; set; }

        public Type TargetType { get; set; }
    }
}