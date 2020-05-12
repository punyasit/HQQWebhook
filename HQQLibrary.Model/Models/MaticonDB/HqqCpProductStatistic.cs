using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqCpProductStatistic
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int? Available { get; set; }
        public long? SaleHistory { get; set; }
        public int? SaleMovement { get; set; }
        public decimal? SaleMovementPercentage { get; set; }
        public decimal? Price { get; set; }
        public decimal? PriceMovement { get; set; }
        public decimal? PriceMovementPercentage { get; set; }
        public long? RatingCount { get; set; }
        public decimal? RatingValue { get; set; }
        public long? LikeCount { get; set; }
        public long? Stock { get; set; }
        public int? StockMovement { get; set; }
        public DateTime CreatedOn { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqCompetitorProduct Product { get; set; }
    }
}
