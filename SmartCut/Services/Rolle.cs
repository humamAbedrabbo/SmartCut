using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public static class Rolle
    {
        public static int maximumCuttingLengthInMm;
        public static OrderItem OrderItem;

        internal static void Cut(StockItemViewModel item)
        {
            Sheet master;
            if(OrderItem.CanRotate)
            {
                master = CuttingWithRotation(item);
            }
            else
            {
                master = CuttingWithOutRotation(item);
            }
        }

        private static Sheet CuttingWithOutRotation(StockItemViewModel item)
        {
            Sheet master;
            if (OrderItem.Hardness.Value == item.Hardness)
                master = new Sheet(OrderItem.Length, item.Width);
            else
                master = new Sheet(OrderItem.Width, item.Width, false);
            master.Cut(false);
            return master;
        }

        private static Sheet CuttingWithRotation(StockItemViewModel item)
        {
            Sheet master1, master2;
            master1 = BestCuttingLocation(OrderItem.Length, item.Width);
            if (OrderItem.Length == OrderItem.Width || (master1 != null && master1.LossPercent == 0))
            {
                return master1;
            }
            master2 = BestCuttingLocation(OrderItem.Width, item.Width);
            if (master2 == null)
                return master1;
            if (master1 == null || master2.LossPercent == 0 || master2.LossPercent < master1.LossPercent)
                return master2;
            return master1;
        }

        private static Sheet BestCuttingLocation(int cuttingUnit, int itemWidth)
        {
            Sheet master, bestMaster = null;
            for (int length = cuttingUnit; length <= maximumCuttingLengthInMm; length += cuttingUnit)
            {
                master = new Sheet(length, itemWidth);
                master.Cut(true);
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
