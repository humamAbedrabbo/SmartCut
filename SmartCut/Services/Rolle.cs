using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public static class Rolle
    {
        internal static void Cut(StockItemViewModel item, OrderFilter filter, int maximumCuttingLengthInMm)
        {
            Sheet master;
            if(filter.Hardness.HasValue)
            {
                master = CuttingWithOneDirection(item, filter, maximumCuttingLengthInMm);
            }
            else
            {
                master = CuttingWithRotation(item, filter, maximumCuttingLengthInMm);
            }
        }

        private static Sheet CuttingWithOneDirection(StockItemViewModel item, OrderFilter filter, int maximumCuttingLengthInMm)
        {
            if (filter.Length > maximumCuttingLengthInMm)
                return null;
            var master = new Sheet(filter.Length, filter.Width, filter.Hardness.Value == item.Hardness);
            master.Cut(filter, false);
            return master;
        }

        private static Sheet CuttingWithRotation(StockItemViewModel item, OrderFilter filter, int maximumCuttingLengthInMm)
        {
            Sheet master1, master2;
            master1 = BestCuttingLocation(filter.Length, item.Width, filter, maximumCuttingLengthInMm);
            if (filter.Length == filter.Width || (master1 != null && master1.LossPercent == 0))
            {
                return master1;
            }
            master2 = BestCuttingLocation(filter.Width, item.Width, filter, maximumCuttingLengthInMm);
            if (master2 == null)
                return master1;
            if (master1 == null || master2.LossPercent == 0 || master2.LossPercent < master1.LossPercent)
                return master2;
            return master1;
        }

        private static Sheet BestCuttingLocation(int cuttingUnit, int itemWidth, OrderFilter filter, int maximumCuttingLengthInMm)
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
