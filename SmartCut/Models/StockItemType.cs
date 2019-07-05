using System.ComponentModel.DataAnnotations;

namespace SmartCut.Models
{
    public enum StockItemType
    {
        [Display(Name = "رول")]
        Roll,
        [Display(Name = "بالة")]
        Sheets
    }
}
