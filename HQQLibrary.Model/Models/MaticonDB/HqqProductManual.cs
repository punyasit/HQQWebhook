using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqProductManual
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public DateTime CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? ModifiedBy { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqProduct Product { get; set; }
    }
}
