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
        public Sheet Master;
        private StockItemViewModel Item;

        public Pallet(StockItemViewModel item)
        {
            this.Item = item;
        }

        public void Cut()
        {
            if (OrderItem.CanRotate)
            {
                Master = new Sheet(Item.Length, Item.Width);
            }
            else
            {
                Master = new Sheet(Item.Length, Item.Width, OrderItem.Hardness.Value == Item.Hardness);
            }
            Master.Cut();
            Item.Evaluate(Master);
        }
    }
}
