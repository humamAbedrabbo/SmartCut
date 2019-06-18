using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Models
{
    public class SettingModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int MaximumCuttingLengthInCm { get; set; }

        [Required]
        [Range(0, 25)]
        public int GramageRangePercent { get; set; }
    }
}
