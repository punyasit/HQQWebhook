using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqvProducts
    {
        public int? Id { get; set; }
        public string ShopId { get; set; }
        public string ShopName { get; set; }
        public int? ProductId { get; set; }
        public long? ProductRefId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public sbyte? IsNew { get; set; }
        public int? Available { get; set; }
        public long? SaleHistory { get; set; }
        public int? SaleMovement { get; set; }
        public decimal? SaleMovementPercentage { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceMovement { get; set; }
        public decimal? PriceMovementPercentage { get; set; }
        public long? RatingCount { get; set; }
        public decimal? RatingValue { get; set; }
        public long? LikedCount { get; set; }
        public int? LikedMovement { get; set; }
        public decimal? LikedPercentage { get; set; }
        public long? Stock { get; set; }
        public int? StockMovement { get; set; }
        public decimal? StockMovementPercentage { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
