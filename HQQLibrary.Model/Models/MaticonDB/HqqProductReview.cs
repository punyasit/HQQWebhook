using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqProductReview
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string Subject { get; set; }
        public string ShortDescription { get; set; }
        public string Review { get; set; }
        public string CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqProduct Product { get; set; }
    }
}
