using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqMetaLocation
    {
        public int Id { get; set; }
        public string IsoCode { get; set; }
        public string Name { get; set; }
        public string Keyword { get; set; }
        public string Language { get; set; }
        public sbyte Status { get; set; }
    }
}
