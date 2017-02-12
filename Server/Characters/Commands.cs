using GTANetworkServer;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.Characters
{
    public class Commands : Script
    {
        [Command("voice")]
        public void voice(Client player)
        {
            Global.CEFController.OpenVoice(player);
        }

        [Command("createcharacter")]
        public static void CreateCharacter(Client player, string name)
        {
            if (!CharacterController.NameValidityCheck(player, name))
            {
                // TODO: CharacterController.CreateCharacter(player);
                return;
            }

            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;

            bool SuccessID = DatabaseManager.RegisterCharacter(account, name);
            if (SuccessID)
            {
                API.shared.sendChatMessageToPlayer(player, "~g~Server: ~w~Character created!");
                CharacterController.CharacterMenu(account);
            }
            else
            {
                API.shared.sendChatMessageToPlayer(player, "We couldn't create this character.");
                // TODO: CharacterController.CreateCharacter(player);
            }
        }

        [Command("logout")]
        public void LogOut(Client player)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            API.shared.sendNotificationToPlayer(player, "~y~Server: ~w~You will be logged out in 5 seconds.", true);
            Global.Util.delay(5000, () =>
            {
                ConnectionController.LogOut(account, 1);
            });
        }

        [Command("stats", Group = "Global Commands")]
        public void GetStatistics(Client player)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;

            API.sendChatMessageToPlayer(player, "___________ STATS __________");
            API.sendChatMessageToPlayer(player, string.Format("~h~Name:~h~ {0} ~h~Level:~h~ {1} ~h~Job:~h~ {2}\n",
                account.CharacterController.FormatName,
                account.CharacterController.Character.Level,
                (account.CharacterController.job == null ? "None" : account.CharacterController.job.Type())) +
                account.CharacterController.ListGroups());
        }
    }
}
