using HQQLibrary.Model.Models.MaticonDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace HQQLibrary.Model.Models.Marketing
{
    public class CPChartData
    {
        public List<HqqCpProductStatistic> CPProductStatistic { get; set; }
        public HqqCompetitorProduct CPProduct { get; set; }
    }
}
