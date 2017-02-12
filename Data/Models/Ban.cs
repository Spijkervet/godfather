using Data.Interfaces;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheGodfatherGM.Data
{
    public class Ban
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        public int IssuerId { get; set; } //virtual IAccountData Issuer { get; set; }
        public string AccountId { get; set; }
        public IAccountData Target { get; set; }

        [StringLength(16)]
        public string Ip { get; set; }

        [StringLength(24)]
        public string SocialClub { get; set; }
        public bool IsSocialClubBanned { get; set; }

        public DateTime BanDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        [StringLength(128)]
        public string BanReason { get; set; }
    }
}
