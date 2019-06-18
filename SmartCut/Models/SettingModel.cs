using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Models
{
    public class SettingModel
    {
        public int Id { get; set; }

        public int MaximumCuttingLengthInCm { get; set; }

        public int GramageRangePercent { get; set; }
    }
}
