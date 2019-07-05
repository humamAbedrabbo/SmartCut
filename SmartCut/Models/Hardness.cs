using System.ComponentModel.DataAnnotations;

namespace SmartCut.Models
{
    public enum Hardness
    {
        [Display(Name = "طولية")]
        Length,
        [Display(Name = "عرضية")]
        Width
    }
}
