using System.ComponentModel.DataAnnotations;

namespace TheGodfatherGM.Web.Models.GameViewModel
{
    public class CharacterViewModel
    {
        [Required]
        [MinLength(4)]
        public string Name { get; set; }

        [Required]
        public string socialclub { get; set; }
    }
}
