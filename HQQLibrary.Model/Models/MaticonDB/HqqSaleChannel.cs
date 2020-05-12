using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqSaleChannel
    {
        public HqqSaleChannel()
        {
            HqqCompetitorShop = new HashSet<HqqCompetitorShop>();
            HqqPrice = new HashSet<HqqPrice>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public sbyte Status { get; set; }

        public virtual ICollection<HqqCompetitorShop> HqqCompetitorShop { get; set; }
        public virtual ICollection<HqqPrice> HqqPrice { get; set; }
    }
}
