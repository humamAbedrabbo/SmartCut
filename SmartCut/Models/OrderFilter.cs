namespace SmartCut.Models
{
    public class OrderFilter
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }

        public int Gramage { get; set; }

        public Hardness? Hardness { get; set; }

        public StockItemType? ItemType { get; set; }

        public int Available { get; set; }
    }
}
