using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public class Pallet
    {
        internal static void Cut(StockItemViewModel item, OrderFilter filter)
        {
            Sheet master;
            if (filter.Hardness.HasValue)
            {
                master = CuttingWithOneDirection(item, filter);
            }
            else
            {
                master = CuttingWithRotation(item, filter);
            }
        }

        private static Sheet CuttingWithRotation(StockItemViewModel item, OrderFilter filter)
        {
            Sheet master = new Sheet(item.Length, item.Width);
            master.Cut(filter, true);
            return master;
        }

        private static Sheet CuttingWithOneDirection(StockItemViewModel item, OrderFilter filter)
        {
            var master = new Sheet(item.Length, item.Width, filter.Hardness.Value == item.Hardness);
            master.Cut(filter, false);
            return master;
        }
    }
}
