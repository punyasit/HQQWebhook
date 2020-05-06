using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqProductImages
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public string Url { get; set; }
        public string FileType { get; set; }
        public long? Length { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public sbyte Status { get; set; }

        public virtual HqqProduct Product { get; set; }
    }
}
