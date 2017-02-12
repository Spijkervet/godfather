using System;
using System.Collections.Generic;
using TheGodfatherGM.Data;

namespace Data.Interfaces
{
    public interface IAccountData
    {
        string Id { get; set; }

        string UserName { get; set; }

        string PasswordHash { get; set; }

        string SessionID { get; set; }

        string SocialClub { get; set; }

        string Ip { get; set; }

        DateTime RegisterDate { get; set; }

        DateTime LastLoginDate { get; set; }

        DateTime UpdateDate { get; set; }

        bool Online { get; set; }

        ICollection<Character> Character { get; set; }

        ICollection<Ban> Ban { get; set; }
    }
}
