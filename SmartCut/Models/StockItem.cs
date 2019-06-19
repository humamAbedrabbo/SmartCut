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

        public Hardness Hardness { get; set; }

        public double Weight { get; set; }

        public int Gramage { get; set; }

        public bool IsAvailable { get; set; } = true;

        public string ShipmentNo { get; set; }

        public string Notes { get; set; }
    }

    public enum StockItemType
    {
        Roll,
        Sheets
    }

    public enum Hardness
    {
        Length,
        Width
    }
}
