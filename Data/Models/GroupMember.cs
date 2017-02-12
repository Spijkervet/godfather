using System.ComponentModel.DataAnnotations;
using TheGodfatherGM.Data;

namespace TheGodfatherGM.Data
{
    public class GroupMember
    {
        [Key]
        public int Id { get; set; }
        public Character Character { get; set; }
        public Group Group { get; set; }
        public bool Leader { get; set; }
    }
}