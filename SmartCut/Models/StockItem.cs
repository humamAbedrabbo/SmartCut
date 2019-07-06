using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartCut.Models
{
    public class StockItem
    {
        public int Id { get; set; }

        public StockItemType ItemType { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public double LengthCM { get => Length / 10.0; set => Length = (int) (value * 10); }
        public double WidthCM { get => Width / 10.0; set => Width = (int) (value * 10); }

        public Hardness Hardness { get; set; }

        public double Weight { get; set; }

        public int Gramage { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string ShipmentNo { get; set; }

        public string Notes { get; set; }

        public static explicit operator StockItemViewModel(StockItem si)
        {
            return new StockItemViewModel
            {
                Id = si.Id,
                ItemType = si.ItemType,
                CategoryId = si.CategoryId,
                Category = si.Category,
                Hardness = si.Hardness,
                Length = si.Length,
                Width = si.Width,
                Weight = si.Weight,
                Gramage = si.Gramage,
                IsAvailable = si.IsAvailable,
                ShipmentNo = si.ShipmentNo,
                Notes = si.Notes
            };
        }
    }
}
