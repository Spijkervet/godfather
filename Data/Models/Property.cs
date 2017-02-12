using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using TheGodfatherGM.Data.Enums;

namespace TheGodfatherGM.Data
{
    public class Property
    {
        [Key]
        public int PropertyID { get; set; }

        public string Name { get; set; }
        public PropertyType Type { get; set; }


        public int? CharacterId { get; set; }
        public Character Character { get; set; }
        public int? GroupId { get; set; }
        public Group Group { get; set; }

        [StringLength(48)]
        public string IPL { get; set; }

        public float ExtPosX { get; set; }
        public float ExtPosY { get; set; }
        public float ExtPosZ { get; set; }
        public float IntPosX { get; set; }
        public float IntPosY { get; set; }
        public float IntPosZ { get; set; }

        [DefaultValue(false)]
        public bool Enterable { get; set; }
        public int Stock { get; set; }

    }
}
