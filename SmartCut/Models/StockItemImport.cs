
namespace SmartCut.Models
{
    public class StockItemImport
    {
        
        public int ItemType { get; set; }

        
        public int CategoryId { get; set; }

        
        public float Length { get; set; }

        
        public float Width { get; set; }

        
        public int Hardness { get; set; }

        
        public double Weight { get; set; }

        
        public int Gramage { get; set; }

        
        public int IsAvailable { get; set; }

        
        public string ShipmentNo { get; set; }

        
        public string Notes { get; set; }

        public static explicit operator StockItem(StockItemImport si)
        {
            return new StockItem
            {
                ItemType = si.ItemType == 0 ? StockItemType.Roll : StockItemType.Sheets,
                CategoryId = si.CategoryId,
                Hardness = si.Hardness == 0 ? Models.Hardness.Length : Models.Hardness.Width,
                Length = System.Convert.ToInt32( si.Length * 10 ),
                Width = System.Convert.ToInt32( si.Width * 10 ),
                Weight = si.Weight,
                Gramage = si.Gramage,
                IsAvailable = si.IsAvailable == 0 ? false : true,
                ShipmentNo = si.ShipmentNo,
                Notes = si.Notes
            };
        }
    }
}
