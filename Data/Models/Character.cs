using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGodfatherGM.Data
{
    public class Character
    {
        public Character()
        {
            GroupMember = new List<GroupMember>();
            Property = new List<Property>();
            Vehicle = new List<Vehicle>();
        }

        [Key]
        public int Id { get; set; }

        public string AccountId { get; set; }
        public IAccountData Account { get; set; }

        [StringLength(32)]
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public int RegistrationStep { get; set; }
        public bool Online { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
        public float PosZ { get; set; }
        public float Rot { get; set; }
        public int Model { get; set; }
        public string ModelName { get; set; }
        public int Admin { get; set; }
        [DefaultValue(0)]
        public int Level { get; set; }
        public int Cash { get; set; }
        public int Bank { get; set; }
        public int ActiveGroupID { get; set; }

        public int JobId { get; set; }

        public virtual ICollection<GroupMember> GroupMember { get; set; }
        public virtual ICollection<Property> Property { get; set; }
        public virtual ICollection<Vehicle> Vehicle { get; set; }
    }
}


