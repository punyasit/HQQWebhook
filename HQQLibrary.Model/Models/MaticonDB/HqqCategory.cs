using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqCategory
    {
        public HqqCategory()
        {
            HqqProduct = new HashSet<HqqProduct>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string InformationUrl { get; set; }
        public sbyte Status { get; set; }

        public virtual ICollection<HqqProduct> HqqProduct { get; set; }
    }
}
