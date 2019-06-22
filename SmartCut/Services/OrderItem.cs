using SmartCut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Services
{
    public class OrderItem
    {
        public int Length { get; }
        public int Width { get; }
        public int Area { get; }
        public int SmallerRib { get; }
        public int Gramage { get; }
        public Hardness? Hardness { get; }
        public bool CanRotate { get; }

        public OrderItem(OrderFilter orderFilter)
        {
            Length = orderFilter.Length;
            Width = orderFilter.Width;
            Gramage = orderFilter.Gramage;
            Area = Length * Width;
            SmallerRib = (Length < Width) ? Length : Width;
            CanRotate = !Hardness.HasValue;
        }
    }
}
