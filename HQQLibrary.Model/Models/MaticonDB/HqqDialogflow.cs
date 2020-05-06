using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqDialogflow
    {
        public int Id { get; set; }
        public string FlowType { get; set; }
        public string MatchKeywords { get; set; }
        public int? ProductId { get; set; }
        public string Payload { get; set; }
        public string ResponseWording { get; set; }
        public string ResponseItems { get; set; }
        public sbyte? Status { get; set; }

        public virtual HqqProduct Product { get; set; }
    }
}
