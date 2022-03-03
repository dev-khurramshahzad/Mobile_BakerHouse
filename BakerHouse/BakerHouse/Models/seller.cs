using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BakerHouse.Models
{
  public class seller
    {
        [PrimaryKey, AutoIncrement]
        public int UserID { get; set; }
        public string Name { get; set; }
        public string phone { get; set; }
        public string Address { get; set; }
    }
}
