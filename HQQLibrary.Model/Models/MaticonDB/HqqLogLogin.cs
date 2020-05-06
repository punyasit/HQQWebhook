using System;
using System.Collections.Generic;

namespace HQQLibrary.Model.Models.MaticonDB
{
    public partial class HqqLogLogin
    {
        public long Id { get; set; }
        public int MemberId { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
