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
            Sheet.OrderItem = Rolle.OrderItem = Pallet.OrderItem = new OrderItem(order.Filter);

            order.Items.AsParallel().ForAll(item =>
            {
                if(item.ItemType == StockItemType.Roll)
                {
                    Rolle.Cut(item);
                }
                else
                {
                    Pallet.Cut(item);
                }
            });
            //sort pallet
            return order;
        }
    }
}
