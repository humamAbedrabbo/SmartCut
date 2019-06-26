using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public class Rolle
    {
        public static int maximumCuttingLengthInMm;
        public static OrderItem OrderItem;
        private StockItemViewModel item;
        double usefullPercent;

        public Rolle(StockItemViewModel item)
        {
            this.item = item;
        }

        internal void Cut()
        {
            Sheet master;
            if(OrderItem.CanRotate)
            {
                master = CuttingWithRotation();
            }
            else
            {
                master = CuttingWithOutRotation();
            }
            item.Evaluate(master);
        }

        private Sheet CuttingWithOutRotation()
        {
            Sheet master;
            if (OrderItem.Hardness.Value == item.Hardness)
                master = new Sheet(OrderItem.Length, item.Width);
            else
                master = new Sheet(OrderItem.Width, item.Width, false);
            master.Cut(false);
            return master;
        }

        private Sheet CuttingWithRotation()
        {
            Sheet master1, master2;
            master1 = BestCuttingLocation(OrderItem.Length, item.Width);
            if (OrderItem.Length == OrderItem.Width || (master1 != null && master1.UsefullPercent() == 0))
            {
                return master1;
            }
            master2 = BestCuttingLocation(OrderItem.Width, item.Width);
            if (master2 == null)
                return master1;
            if (master1 == null || master2.UsefullPercent() == 0 || master2.UsefullPercent() < master1.UsefullPercent())
                return master2;
            return master1;
        }

        private Sheet BestCuttingLocation(int cuttingUnit, int itemWidth)
        {
            Sheet master, bestMaster = null;
            usefullPercent = 0;
            for (int length = cuttingUnit; length <= maximumCuttingLengthInMm; length += cuttingUnit)
            {
                master = new Sheet(length, itemWidth);
                master.Cut(true);
                usefullPercent = master.UsefullPercent();
                if (usefullPercent == 1)
                {
                    return master;
                }
                
                {
                    if (bestMaster == null || master.UsefullPercent() < bestMaster.UsefullPercent())
                        bestMaster = master;
                }
            }
            return bestMaster;
        }
    }
}
