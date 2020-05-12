using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqCompetitorProduct
    {
        public HqqCompetitorProduct()
        {
            HqqCpProductStatistic = new HashSet<HqqCpProductStatistic>();
        }

        public int Id { get; set; }
        public int ShopId { get; set; }
        public long? ProductRefId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public sbyte? IsNew { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqCompetitorShop Shop { get; set; }
        public virtual ICollection<HqqCpProductStatistic> HqqCpProductStatistic { get; set; }
    }
}
