using System;
using System.Collections.Generic;
using System.Text;

namespace HQQLibrary.Model.Models.Marketing
{
    public partial class ProductDisplayItem
    {
        public Uri Context { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Uri Url { get; set; }
        public long ProductId { get; set; }
        public Uri Image { get; set; }
        public string Brand { get; set; }
        public Offers Offers { get; set; }
        public AggregateRating AggregateRating { get; set; }
    }

    public partial class AggregateRating
    {
        public string Type { get; set; }
        public long BestRating { get; set; }
        public long WorstRating { get; set; }
        public long RatingCount { get; set; }
        public string RatingValue { get; set; }
    }

    public partial class Offers
    {
        public string Type { get; set; }
        public string Price { get; set; }
        public string PriceCurrency { get; set; }
        public Uri Availability { get; set; }
    }

}
