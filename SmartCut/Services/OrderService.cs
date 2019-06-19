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
            foreach(var item in order.Items)
            {
                item.LossPercent = 5;
                item.Total = 100;
            }

            return order;
        }
    }
}
