using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqProductFaq
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public sbyte Order { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public sbyte Status { get; set; }
    }
}
