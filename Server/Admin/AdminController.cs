using GTANetworkServer;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.Admin
{
    public class AdminController
    {
        
        public static bool AdminRankCheck(string cmd, AccountController account)
        {
            if (cmd == "makeadmin" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "creategroup" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "editgroup" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "createproperty" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "setproperty" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "editjob" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "setmoney" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "createnpc" && account.CharacterController.Character.Admin > 4) return true;
            else if (cmd == "givegun" && account.CharacterController.Character.Admin > 1) return true;
            else if (cmd == "createvehicle" && account.CharacterController.Character.Admin > 1) return true;
            else if (cmd == "goto" && account.CharacterController.Character.Admin > 1) return true;
            else if (cmd == "switchgroup" && account.CharacterController.Character.Admin > 1) return true;
            else if (cmd == "setskin" && account.CharacterController.Character.Admin > 1) return true;
            else if (cmd == "sethealth" && account.CharacterController.Character.Admin > 1) return true;
            else if (cmd == "setarmor" && account.CharacterController.Character.Admin > 1) return true;


            API.shared.sendChatMessageToPlayer(account.Client, "~r~[ERROR]: ~w~You do not have access to this command.");
            return false;
        }

        public static string GetAdminRank(int Level)
        {
            switch (Level)
            {
                case 1: return "~g~Level 1 Admin~w~";
                case 2: return "~g~Level 2 Admin~w~";
                case 3: return "~g~Level 3 Admin~w~";
                case 4: return "~o~Senior Admin~w~";
                case 5: return "~r~Lead Admin~w~";
                case 6: return "~r~Management~w~";
                case 9: return "~p~Root Access~w~";
                default: return "";
            }
        }
    }
}
