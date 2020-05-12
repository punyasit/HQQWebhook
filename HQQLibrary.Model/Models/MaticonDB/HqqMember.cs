using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqMember
    {
        public HqqMember()
        {
            HqqMemberProduct = new HashSet<HqqMemberProduct>();
        }

        public int Id { get; set; }
        public string FacebookId { get; set; }
        public string Fullname { get; set; }
        public string FacebookName { get; set; }
        public string PictureUrl { get; set; }
        public string Address { get; set; }
        public string LocationCode { get; set; }
        public string HometownCode { get; set; }
        public string Postcode { get; set; }
        public sbyte Role { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public sbyte Status { get; set; }

        public virtual ICollection<HqqMemberProduct> HqqMemberProduct { get; set; }
    }
}
