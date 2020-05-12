using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqMemberProduct
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime? GaranteeExpired { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqMember Member { get; set; }
        public virtual HqqProduct Product { get; set; }
    }
}
