using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TheGodfatherGM.Data;

namespace TheGodfatherGM.Server.Account
{
    [Table("AspNetUsers")]
    public class Account : IAccountData
    {
        public Account() { }

        [Key]
        public string Id { get; set; }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }

        [StringLength(25)]
        public string SessionID { get; set; }

        [StringLength(24)]
        public string SocialClub { get; set; }
        [StringLength(16)]
        public string Ip { get; set; }
        public DateTime RegisterDate { get; set; }
        public DateTime LastLoginDate { get; set; }
        public DateTime UpdateDate { get; set; }

        public bool Online { get; set; }

        public virtual ICollection<Character> Character { get; set; }

        public virtual ICollection<Ban> Ban { get; set; }
    }
}
