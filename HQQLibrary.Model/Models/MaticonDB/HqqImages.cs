using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqImages
    {
        public int Id { get; set; }
        public int EntityId { get; set; }
        public string Filename { get; set; }
        public string Path { get; set; }
        public string FileType { get; set; }
        public string Location { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int CreatedBy { get; set; }
        public sbyte Status { get; set; }
    }
}
