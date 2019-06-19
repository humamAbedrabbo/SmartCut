using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Models
{
    public class Order
    {
        public Order()
        {
            Filter = new OrderFilter();
            Items = new List<StockItemViewModel>();
        }

        public OrderFilter Filter { get; set; }
        public List<StockItemViewModel> Items { get; set; }
    }
}
