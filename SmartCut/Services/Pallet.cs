using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public class Pallet
    {
        public static OrderItem OrderItem;

        internal static void Cut(StockItemViewModel item)
        {
            Sheet master;
            if (OrderItem.CanRotate)
            {
                master = CuttingWithRotation(item);
            }
            else
            {
                master = CuttingWithOutRotation(item);
            }
        }

        private static Sheet CuttingWithRotation(StockItemViewModel item)
        {
            Sheet master = new Sheet(item.Length, item.Width);
            master.Cut(true);
            return master;
        }

        private static Sheet CuttingWithOutRotation(StockItemViewModel item)
        {
            var master = new Sheet(item.Length, item.Width, OrderItem.Hardness.Value == item.Hardness);
            master.Cut(false);
            return master;
        }
    }
}
