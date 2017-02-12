using GTANetworkServer;
using GTANetworkShared;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server
{
    public class ConnectionController : Script
    {
        public static readonly Vector3 _startPos = new Vector3(3433.339f, 5177.579f, 39.79541f);
        public static readonly Vector3 _startCamPos = new Vector3(3476.85f, 5228.022f, 9.453369f);

        public ConnectionController()
        {
            API.onPlayerConnected += OnPlayerConnectedHandler;
            API.onPlayerFinishedDownload += onPlayerFinishedDownloadHandler;
            API.onPlayerDisconnected += onPlayerDisconnectedHandler;
        }

        public void OnPlayerConnectedHandler(Client player)
        {
            if(AccountController.IsAccountBanned(player))
            {
                player.kick("~r~You are banned from this server.");
            }
        }

        public void onPlayerFinishedDownloadHandler(Client player)
        {
            API.setEntityData(player, "DOWNLOAD_FINISHED", true);
            LoginMenu(player);
        }

        public void onPlayerDisconnectedHandler(Client player, string reason)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            LogOut(account);
        }

        public static void LoginMenu(Client player)
        {
            API.shared.triggerClientEvent(player, "interpolateCamera", 20000, _startCamPos, _startCamPos + new Vector3(0.0, -50.0, 50.0), new Vector3(0.0, 0.0, 180.0), new Vector3(0.0, 0.0, 95.0));
            player.position = _startPos;
            player.freeze(true);
            player.transparency = 0;
            PromptLoginScreen(player);
        }

        public static void LogOut(AccountController account, int type = 0)
        {
            account.Save();
            account.Account.Online = false;
            if (type != 0)
            {
                LoginMenu(account.Client);
            }

            account.Account.SessionID = null;
            ContextFactory.Instance.SaveChanges();
            Vehicles.VehicleController.UnloadVehicles(account);
            account.Client.resetData("ACCOUNT");
        }

        public static void PromptLoginScreen(Client player)
        {
            string url = Global.GlobalVars.WebServerConnectionString + "Game/Login?socialclub=" + player.socialClubName + "&token=" + Global.Util.GenerateToken();
            API.shared.triggerClientEvent(player, "CEFController", url);
            API.shared.sendChatMessageToPlayer(player, "URL: " + url);
        }

        /*
        [Command]
        public static void Login(Client player, string name, string password)
        {
            if (API.shared.getEntityData(player, "ACCOUNT") != null)
            {
                API.shared.sendChatMessageToPlayer(player, "~r~SERVER: ~w~You're already logged in!");
                return;
            }

            var accountData = ContextFactory.Instance.Accounts.Where(x => x.UserName == name).FirstOrDefault();
            if (accountData != null && BCr.BCrypt.Verify(password, accountData.PasswordHash))
            {
                if (AccountController.IsAccountBanned(accountData))
                {
                    player.kick("~r~This account is banned.");
                }
                else new AccountController(accountData, player);
            }
            else
            {
                API.shared.sendChatMessageToPlayer(player, "~r~ERROR:~w~ Wrong password, or account doesnt exist!");
                PromptLoginScreen(player);
            }
        }
        */
    }
}