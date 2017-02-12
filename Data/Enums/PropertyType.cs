using TheGodfatherGM.Data.Attributes;
using System.ComponentModel.DataAnnotations;

namespace TheGodfatherGM.Data.Enums
{
    public enum PropertyType
    {
        [Display(Name = "Invalid")]
        Invalid = 0,

        [BlipType(60)]
        [Display(Name = "House")]
        House = 1,

        [Display(Name = "Door")]
        Door = 2,

        [Display(Name = "Building")]
        Building = 3
    }
}
