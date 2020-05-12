using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqCompetitorShop
    {
        public HqqCompetitorShop()
        {
            HqqCompetitorProduct = new HashSet<HqqCompetitorProduct>();
        }

        public int Id { get; set; }
        public int? ChannelId { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public int Follower { get; set; }
        public long? RatingCount { get; set; }
        public decimal? RatingValue { get; set; }
        public string DataUrl { get; set; }
        public string PageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqSaleChannel Channel { get; set; }
        public virtual ICollection<HqqCompetitorProduct> HqqCompetitorProduct { get; set; }
    }
}
