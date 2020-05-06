using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ChannelId { get; set; }
        public string AffiliateUrl { get; set; }
        public DateTime PriceDate { get; set; }
        public decimal? Price { get; set; }
        public sbyte? Status { get; set; }

        public virtual HqqSaleChannel Channel { get; set; }
        public virtual HqqProduct Product { get; set; }
    }
}
