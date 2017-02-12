using Data.Interfaces;
using GTANetworkServer;
using System;
using System.Linq;
using System.Text;
using TheGodfatherGM.Server.Characters;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.Extensions;

namespace TheGodfatherGM.Server.User
{
    public class AccountController
    {
        public string AccountId;
        public IAccountData Account;

        public int PlayerId;

        public Client Client { get; private set; }

        public CharacterController CharacterController;

        public AccountController(Account.Account account, Client client)
        {
            Account = account;
            Client = client;

            client.setData("ACCOUNT", this);

            API.shared.sendNotificationToPlayer(client, string.Format("~w~Welcome {0} to {1}. Your last login was at {2}",
                Account.UserName, Global.GlobalVars.ServerName, Account.LastLoginDate));

            Account.LastLoginDate = DateTime.Now;
            Account.Online = true;

            ChatController.LoginMessages(this);
            CharacterController.CharacterMenu(this);
            EntityManager.Add(this);


            API.shared.consoleOutput("[SERVER]: " + Account.SocialClub + "'s SessionID set!");
            API.shared.setEntityData(client, "session_id", GetSessionID(Account.SocialClub));
            // API.shared.triggerClientEvent(client, "SESSION_CRYPT", "var cef=API.createCefBrowser(0,0,true);API.waitUntilCefBrowserInit(cef);API.setCefBrowserPosition(cef,0,00);API.loadPageCefBrowser(cef,'');cef.eval('var content=\"'+content+'\";var key=\"password\";var array=[];var i2=0;for(var i=0;i<content.length;i++){if(i2>=key.length){i2=0}array+=String.fromCharCode((content[i].charCodeAt(0))-15000-(key[i2].charCodeAt(0)));i2++};resourceEval(array)')");
            API.shared.triggerClientEvent(client, "CEF_DESTROY");
            ContextFactory.Instance.SaveChanges();
        }

        public static bool IsAccountBanned(Client player)
        {
            var IPBanEntity = ContextFactory.Instance.Ban.FirstOrDefault(x => x.Active == true && x.Ip == player.address);
            if (IPBanEntity != null) return true;
            var SocialClubBanEntity = ContextFactory.Instance.Ban.FirstOrDefault(x => x.Active == true && x.IsSocialClubBanned == true && x.SocialClub == player.socialClubName);
            if (SocialClubBanEntity != null) return true;
            return false;
        }

        public static bool IsAccountBanned(Account.Account accountData)
        {
            var AccountBanEntity = ContextFactory.Instance.Ban.FirstOrDefault(ban => ban.Active == true && ban.Target == accountData);
            if (AccountBanEntity != null) return true;
            return false;
        }

        public static bool RegisterAccount(Client sender, string username, string hash)
        {
            if (!DatabaseManager.DoesAccountExist(username))
            {
                Account.Account account = new Account.Account();
                account.UserName = username;
                account.PasswordHash = hash;
                ContextFactory.Instance.Account.Add(account);
                ContextFactory.Instance.SaveChanges();
                new AccountController(account, sender);
                return true;
            }
            return false;
        }

        public void SendPmMessage(AccountController sender, string message)
        {
            API.shared.sendChatMessageToPlayer(sender.Client, "~#FFFF00~", "(( PM sent to " + sender.CharacterController.FormatName + " (ID: " + sender.PlayerId + "): " + message + " ))");
            API.shared.sendChatMessageToPlayer(Client, "~#FFFF00~", "(( PM from " + sender.CharacterController.FormatName + " (ID: " + sender.PlayerId + "): " + message + " ))");
        }

        public static AccountController GetAccountControllerFromName(string Name)
        {
            Client client = API.shared.getAllPlayers().FirstOrDefault(x => x.GetAccountController().CharacterController.FormatName.ToLower().Contains(Name.ToLower()));
            if (client != null) return client.GetAccountController();
            return null;
        }

        public void Save()
        {
            if (CharacterController != null)
            {
                CharacterController.Save();
            }
        }

        public static bool IsSessionIDValid(string socialclub_id, string session_id)
        {
            foreach (Client player in API.shared.getAllPlayers())
            {
                if (player.socialClubName == socialclub_id)
                {
                    return (API.shared.getEntityData(player, "session_id") == session_id);
                }
            }
            return false;
        }

        public static Client GetClientFromSocialClub(string SocialClub)
        {
            foreach(var client in API.shared.getAllPlayers())
            {
                if (client.socialClubName == SocialClub) return client;
            }
            return null;
        }

        public string GetSessionID(string socialclub_id)
        {
            string SessionID = Account.SessionID;
            if (SessionID != null) return SessionID;
            
            StringBuilder session_id = new StringBuilder("");
            Random Random = new Random();
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789_-".ToCharArray();
            for (int i = 0; i < 25; i++)
            {
                int charindex = (Random).Next(chars.Length);
                session_id.Append(chars[charindex]);
            }

            Client.setData("session_id", session_id);
            Account.SessionID = session_id.ToString();               
            return session_id.ToString();
        }
    }
}
