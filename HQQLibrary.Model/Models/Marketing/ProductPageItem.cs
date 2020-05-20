using System;
using System.Collections.Generic;
using System.Text;

namespace HQQLibrary.Model.Models.Marketing
{
    public class ProductPageItem
    {
        public long ProductId { get; set; }
        public string URL { get; set; }
        public int Sold { get; set; }
        public int Liked { get; set; }
        public int Stock { get; set; }
    }
}
