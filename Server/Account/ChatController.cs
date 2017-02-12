using GTANetworkServer;
using System.Collections.Generic;
using TheGodfatherGM.Server.Admin;
using TheGodfatherGM.Server.Characters;
using TheGodfatherGM.Server.Extensions;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server
{
    public class ChatController : Script
    {
        public ChatController()
        {
            API.onChatMessage += OnChatMessageHandler;
            API.onChatCommand += OnChatCommandHandler;
        }

        public void OnChatMessageHandler(Client player, string message, CancelEventArgs e)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 15.0f, "~#FFFFFF~", account.CharacterController.FormatName + " says: " + message);
            e.Cancel = true;
        }

        public void OnChatCommandHandler(Client player, string arg, CancelEventArgs ce)
        {
            if (API.getEntityData(player, "DOWNLOAD_FINISHED") != true) ce.Cancel = true;

            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            if (!arg.Contains("/login") && !arg.Contains("/register"))
            {
                if (account == null) ce.Cancel = true;
            }
            
        }

        public static void LoginMessages(AccountController account)
        {
            // CharacterController.ListCharacters(account);
        }

		public static void LoginMessages(CharacterController character)
		{
			if (character.Character.Admin > 0)
			{
				API.shared.sendChatMessageToPlayer(character.AccountController.Client, "~#FFCCBB~You are a " + AdminController.GetAdminRank(character.Character.Admin) + " on this server. Please respect fellow staff and players.");
			}
		}

        public static void sendProxMessage(Client player, float radius, string color, string msg)
        {
            List<Client> proxPlayers = API.shared.getPlayersInRadiusOfPlayer(radius, player);
            foreach (Client target in proxPlayers)
            {
                API.shared.sendChatMessageToPlayer(target, color, msg);
            }
        }

        // Chat-related commands:
        [Command("me", GreedyArg = true)]
        public void ME_Command(Client player, string msg)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 15.0f, "~#C2A2DA~", account.CharacterController.FormatName + " " + msg);
        }

        [Command("melow", GreedyArg = true)]
        public void MELow_Command(Client player, string msg)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 7.5f, "~#C2A2DA~", account.CharacterController.FormatName + " " + msg);
        }

        [Command("do", GreedyArg = true)] // do command 
        public void DO_Command(Client player, string message)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 15.0f, "~#C2A2DA~", "* " + message + " (( " + account.CharacterController.FormatName + " ))");
        }

        [Command("dolow", GreedyArg = true)] // do command 
        public void DOLow_Command(Client player, string message)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 7.5f, "~#C2A2DA~", "* " + message + " (( " + account.CharacterController.FormatName + " ))");
        }

        [Command("s", Alias = "shout", GreedyArg = true)]
        public void S_Command(Client player, string message)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 25.0f, "~#FFFFFF~", account.CharacterController.FormatName + " shouts: " + message + "!");
        }
        [Command("w", Alias = "whisper", GreedyArg = true)]
        public void W_Command(Client player, string message)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 7.5f, "~#FFFFFF~", account.CharacterController.FormatName + " whispers: " + message);
        }

        [Command("b", GreedyArg = true)]
        public void B_Command(Client player, string msg)
        {
            AccountController account =  player.getData("ACCOUNT");
            if (account == null) return;
            sendProxMessage(player, 15.0f, "~#FFFFFF~", "(( " + account.CharacterController.FormatName + ": " + msg + " ))");
        }

        [Command("o", GreedyArg = true)]
        public void OOC_Command(Client player, string msg)
        {
            foreach (Client c in API.getAllPlayers())
            {
                AccountController account =  player.getData("ACCOUNT");
                if (account == null) return;
                API.sendChatMessageToPlayer(c, "~#FFFFFF~", "(( " + account.CharacterController.FormatName + ": " + msg + " ))");
            }
        }
        
        [Command("pm", GreedyArg = true)]
        public void PmCommand(Client player, Client targetPlayer, string message)
        {
            var senderAccountController =  player.GetAccountController();
            if (senderAccountController == null) return;

            var targetAccountController = targetPlayer.GetAccountController();
            if (targetAccountController == null) return;

            targetAccountController.SendPmMessage(senderAccountController, message);
        }
    }
}
