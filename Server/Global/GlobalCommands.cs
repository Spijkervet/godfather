using GTANetworkServer;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.Global
{
    public class GlobalCommands : Script
    {

        [Command("id", GreedyArg = true)]
        public void id(Client player, string IDName)
        {
            AccountController account = EntityManager.GetUserAccount(player, IDName);
            if (account == null)
            {
                EntityManager.ListUserAccounts(player, IDName);
            }
            else
            {
                API.sendChatMessageToPlayer(player, "" + account.CharacterController.FormatName + " (ID: " + account.PlayerId + ") - (Level: " + account.CharacterController.Character.Level + ") - (Ping: " + API.getPlayerPing(account.Client) + ")");
            }
        }
    }
}