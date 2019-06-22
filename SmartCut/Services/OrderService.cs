using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public class OrderService 
    {
        public static Order CheckOrder(Order order, SettingModel settings)
        {
            order.Items.AsParallel().ForAll(item =>
            {
                if(item.ItemType == StockItemType.Roll)
                {
                    Rolle.Cut(item, order.Filter, settings.MaximumCuttingLengthInCm * 10);
                }
                else
                {
                    Pallet.Cut(item, order.Filter);
                }
            });
            //sort pallet
            return order;
        }
    }
}
