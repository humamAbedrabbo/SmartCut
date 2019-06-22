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
            Rolle.maximumCuttingLengthInMm = settings.MaximumCuttingLengthInCm * 10;
            order.Items.AsParallel().ForAll(item =>
            {
                if(item.ItemType == StockItemType.Roll)
                {
                    Rolle.Cut(item, order.Filter);
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
