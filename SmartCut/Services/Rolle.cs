using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public static class Rolle
    {
        static public int maximumCuttingLengthInMm;

        internal static void Cut(StockItemViewModel item, OrderFilter filter)
        {
            Sheet master;
            if(filter.Hardness.HasValue)
            {
                master = CuttingWithOneDirection(item, filter);
            }
            else
            {
                master = CuttingWithRotation(item, filter);
            }
        }

        private static Sheet CuttingWithOneDirection(StockItemViewModel item, OrderFilter filter)
        {
            Sheet master;
            if (filter.Hardness.Value == item.Hardness)
                master = new Sheet(filter.Length, item.Width);
            else
                master = new Sheet(filter.Width, item.Width, false);
            master.Cut(filter, false);
            return master;
        }

        private static Sheet CuttingWithRotation(StockItemViewModel item, OrderFilter filter)
        {
            Sheet master1, master2;
            master1 = BestCuttingLocation(filter.Length, item.Width, filter);
            if (filter.Length == filter.Width || (master1 != null && master1.LossPercent == 0))
            {
                return master1;
            }
            master2 = BestCuttingLocation(filter.Width, item.Width, filter);
            if (master2 == null)
                return master1;
            if (master1 == null || master2.LossPercent == 0 || master2.LossPercent < master1.LossPercent)
                return master2;
            return master1;
        }

        private static Sheet BestCuttingLocation(int cuttingUnit, int itemWidth, OrderFilter filter)
        {
            Sheet master, bestMaster = null;
            for (int length = cuttingUnit; length <= maximumCuttingLengthInMm; length += cuttingUnit)
            {
                master = new Sheet(length, itemWidth);
                master.Cut(filter, true);
                if (master.LossPercent == 0)
                {
                    return master;
                }
                else
                {
                    if (bestMaster == null || master.LossPercent < bestMaster.LossPercent)
                        bestMaster = master;
                }
            }
            return bestMaster;
        }
    }
}
