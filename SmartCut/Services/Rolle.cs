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
        double UsefullPercent;
        public Sheet BestMaster;

        public Rolle(StockItemViewModel item)
        {
            this.item = item;
        }

        internal void Cut()
        {
            UsefullPercent = 0;
            BestMaster = null;
            if (OrderItem.CanRotate)
            {
                CuttingWithRotation();
            }
            else
            {
                CuttingWithOutRotation();
            }
        }

        private void CuttingWithOutRotation()
        {
            if (OrderItem.Hardness.Value == item.Hardness)
                BestMaster = new Sheet(OrderItem.Length, item.Width);
            else
                BestMaster = new Sheet(OrderItem.Width, item.Width, false);
            BestMaster.Cut();
            item.Evaluate(BestMaster);
        }

        private void CuttingWithRotation()
        {
            BestCuttingLocation(OrderItem.Length);
            if (UsefullPercent != 1 && OrderItem.Length != OrderItem.Width)
                BestCuttingLocation(OrderItem.Width);
            item.Evaluate(BestMaster, UsefullPercent);
        }

        private void BestCuttingLocation(int cuttingUnit)
        {
            Sheet master = null;
            double usefullPercentBuffer = 0;
            for (int length = cuttingUnit; length <= maximumCuttingLengthInMm; length += cuttingUnit)
            {
                master = new Sheet(length, item.Width);
                master.Cut();
                usefullPercentBuffer = master.UsefullPercent();
                if (BestMaster == null || usefullPercentBuffer > UsefullPercent)
                {
                    BestMaster = master;
                    UsefullPercent = usefullPercentBuffer;
                    if (UsefullPercent == 1)
                        return;
                }
            }
        }
    }
}
