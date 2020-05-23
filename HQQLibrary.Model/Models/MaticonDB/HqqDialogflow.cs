using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqDialogflow
    {
        public HqqDialogflow()
        {
            HqqDialogflowAddon = new HashSet<HqqDialogflowAddon>();
        }

        public int Id { get; set; }
        public string FlowType { get; set; }
        public string MatchKeywords { get; set; }
        public int? ProductId { get; set; }
        public int? Ordering { get; set; }
        public string Payload { get; set; }
        public string PayloadImgUrl { get; set; }
        public string ResponseHeader { get; set; }
        public string ResponseAnswer { get; set; }
        public string ResponseItems { get; set; }
        public sbyte? Status { get; set; }

        public virtual HqqProduct Product { get; set; }
        public virtual ICollection<HqqDialogflowAddon> HqqDialogflowAddon { get; set; }
    }
}
