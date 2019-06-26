using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartCut.Models;

namespace SmartCut.Services
{
    public class OrderService 
    {
        static object locker = new object();

        public static Order CheckOrder(Order order, SettingModel settings)
        {
            lock (locker)
            {
                Rolle.maximumCuttingLengthInMm = settings.MaximumCuttingLengthInCm * 10;
                Sheet.OrderItem = Rolle.OrderItem = Pallet.OrderItem = new OrderItem(order.Filter);
                Sheet.CalculatedSheetdictinary = new System.Collections.Concurrent.ConcurrentDictionary<(int, int), int>();
                order.Items.AsParallel().ForAll(item =>
                {
                    if (item.ItemType == StockItemType.Roll)
                    {
                        Rolle rolle = new Rolle(item);
                        rolle.Cut();
                    }
                    else
                    {
                        Pallet pallet = new Pallet(item);
                        pallet.Cut();
                    }
                });
                //sort pallet
                order.Items.Sort();
            }
            return order;
        }
    }
}
