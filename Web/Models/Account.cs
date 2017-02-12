using Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheGodfatherGM.Data;

namespace TheGodfatherGM.Web.Models
{
    public class Account : IdentityUser<string>, IAccountData
    {
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

        public ICollection<Character> Character { get; set; }

        public ICollection<Ban> Ban { get; set; }
    }
}
