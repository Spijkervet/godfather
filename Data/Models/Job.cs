using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.ComponentModel;
using TheGodfatherGM.Data.Enums;

namespace TheGodfatherGM.Data
{
    public class Job
    {
        [Key]
        public int Id { get; set; }
        public JobType Type { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }

        [DefaultValue(1)]
        public int Level { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}