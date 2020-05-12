using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqProduct
    {
        public HqqProduct()
        {
            HqqDialogflow = new HashSet<HqqDialogflow>();
            HqqDialogflowAddon = new HashSet<HqqDialogflowAddon>();
            HqqMemberProduct = new HashSet<HqqMemberProduct>();
            HqqPrice = new HashSet<HqqPrice>();
            HqqProductFaq = new HashSet<HqqProductFaq>();
            HqqProductImages = new HashSet<HqqProductImages>();
            HqqProductManual = new HashSet<HqqProductManual>();
            HqqProductReview = new HashSet<HqqProductReview>();
        }

        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InformationUrl { get; set; }
        public string PreviewImageUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqCategory Category { get; set; }
        public virtual ICollection<HqqDialogflow> HqqDialogflow { get; set; }
        public virtual ICollection<HqqDialogflowAddon> HqqDialogflowAddon { get; set; }
        public virtual ICollection<HqqMemberProduct> HqqMemberProduct { get; set; }
        public virtual ICollection<HqqPrice> HqqPrice { get; set; }
        public virtual ICollection<HqqProductFaq> HqqProductFaq { get; set; }
        public virtual ICollection<HqqProductImages> HqqProductImages { get; set; }
        public virtual ICollection<HqqProductManual> HqqProductManual { get; set; }
        public virtual ICollection<HqqProductReview> HqqProductReview { get; set; }
    }
}
