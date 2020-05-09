using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqDialogflowAddon
    {
        public int Id { get; set; }
        public int DialogflowId { get; set; }
        public int? ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqDialogflow Dialogflow { get; set; }
        public virtual HqqProduct Product { get; set; }
    }
}
