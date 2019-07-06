namespace SmartCut.Models
{
    public class OrderFilter
    {
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public int Length { get; set; }

        public int Width { get; set; }
        public double LengthCM { get => Length / 10.0; set => Length = (int)(value * 10); }
        public double WidthCM { get => Width / 10.0; set => Width = (int)(value * 10); }

        public int Gramage { get; set; }

        public Hardness? Hardness { get; set; }

        public StockItemType? ItemType { get; set; }

        public int Available { get; set; }
    }
}
