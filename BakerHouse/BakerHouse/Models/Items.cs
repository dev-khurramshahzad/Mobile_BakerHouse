using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace BakerHouse.Models
{
   public class Items
    { 
        [PrimaryKey,AutoIncrement]
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int  CatFID { get; set; }
        public int Rating { get; set; }
        public float SPrice { get; set; }
        public float PPrice { get; set; }
        public string ItemImage { get; set; }
        public string ItemMFGDate { get; set; }
        public string ItemEXPDate { get; set; }
        public string ItemDetail { get; set; }
        public string ItemStatus { get; set; }
    }
}
