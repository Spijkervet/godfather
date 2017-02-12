using System.Collections.Generic;
using System.Data;
using GTANetworkServer;
using TheGodfatherGM.Server.Characters;
using System.Linq;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server
{
    public class DatabaseManager : Script
    {
        public DatabaseManager()
        {
            API.onResourceStart += OnResourceStart;
            // API.onPlayerConnected += OnPlayerConnected;
        }

        private void OnResourceStart()
        {
            ContextFactory.SetConnectionParameters(API.getSetting<string>("database_server"), API.getSetting<string>("database_user"), API.getSetting<string>("database_password"), API.getSetting<string>("database_database"));
            EntityManager.Init();
        }

        public static void ResetSessions()
        {
            var entities = ContextFactory.Instance.Account.Where(x => x.SessionID != null).ToList();
            if(entities != null)
            {
                API.shared.consoleOutput("[SERVER]: Resetting all SessionIDs...");
                entities.ForEach(x =>
                {
                    x.SessionID = null;
                    x.Online = false;
                });
            }
        }

        public static bool DoesAccountExist(string username)
        {
            if (ContextFactory.Instance.Account.Where(x => x.UserName == username).FirstOrDefault() == null) return false;
            return true;
        }

        public static bool RegisterAccount(Client sender, string username, string hash)
        {
            if (!DoesAccountExist(username))
            {
                Account.Account account = new Account.Account();
                account.UserName = username;
                account.PasswordHash = hash;
                account.SocialClub = sender.name;
                account.Ip = sender.address;

                ContextFactory.Instance.Account.Add(account);
                ContextFactory.Instance.SaveChanges();

                new AccountController(account, sender);
                return true;
            }
            return false;
        }

        public static bool DoesCharacterExist(string name)
		{
            if (ContextFactory.Instance.Character.Where(x => x.Name == name).FirstOrDefault() == null) return false;
            return true;
		}


        public static List<Data.Character> GetCharacters(AccountController account)
        {
            return account.Account.Character.ToList();
        }

        public static bool HasCharacterSlot(AccountController account)
		{
            if (account.Account.Character == null) return true;
            if (account.Account.Character.Count() > 10)
            {
                API.shared.sendChatMessageToPlayer(account.Client, "~r~[ERROR]: ~w~You have too many characters!");
                return false;
            }
            return true;
		}

		public static bool RegisterCharacter(AccountController account, string name)
		{
			if (!DoesCharacterExist(name) && HasCharacterSlot(account))
			{
                new CharacterController(account, name);
                return true;
            }
			return false;
		}
    }
}