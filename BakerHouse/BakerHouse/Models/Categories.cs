using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BakerHouse.Models
{
  public  class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int CatID { get; set; }
        public int CatFID { get; set; }
        public string CatName { get; set; }

        public string CatImage { get; set; }
        public string CatType { get; set; }
        public string CatDetails { get; set; }
        public string Status { get; set; }
        
    }
}
