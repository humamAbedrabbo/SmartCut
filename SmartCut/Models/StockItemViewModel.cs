namespace SmartCut.Models
{
    public class StockItemViewModel
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

        public double LossPercent { get; set; }

        public int Total { get; set; }

        public static explicit operator StockItem(StockItemViewModel si)
        {
            return new StockItem
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
