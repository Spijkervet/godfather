using TheGodfatherGM.Data.Enums;
using GTANetworkServer;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Server.Admin;

namespace TheGodfatherGM.Server.Groups
{
    public class Commands : Script
    {
        [Command]
        public void creategroup(Client player)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("creategroup", account)) return;
            Global.CEFController.SendRequest(player, "Group/CreateGroup", null);
        }

        [Command("editgroup")]
        public void editgroup(Client player, int id, string name, GroupType type, GroupExtraType extraType)
        {
            if (id > 0)
            {
                AccountController account = player.getData("ACCOUNT");
                if (account == null) return;
                if (!AdminController.AdminRankCheck("editgroup", account)) return;

                GroupController GroupController = EntityManager.GetGroup(id);
                if (GroupController == null)
                {
                    player.sendChatMessage("~r~ERROR: ~w~You specified an invalid group.");
                }
                else
                {
                    GroupController.Group.Name = name;
                    GroupController.Group.Type = type;

                    GroupController.Group.ExtraType = extraType;
                    API.sendChatMessageToPlayer(player, "You successfully edited GroupID " + id);
                    ContextFactory.Instance.SaveChanges();
                }
            }
        }

        [Command]
        public void listgroups(Client player)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            player.sendChatMessage("____ Groups ____");
            foreach (var group in EntityManager.GetGroups())
            {
                if (group != null) player.sendChatMessage("" + group.Group.Id + ": " + group.Group.Name + " | " + group.Type());
            }
            player.sendChatMessage("_______________");
        }

        [Command]
        public void switchgroup(Client player, int id)
        {
            if (id > 0)
            {
                AccountController account = player.getData("ACCOUNT");
                if (account == null) return;
                if (!AdminController.AdminRankCheck("switchgroup", account)) return;

                GroupController GroupController = EntityManager.GetGroup(id);
                if (GroupController == null)
                {
                    player.sendChatMessage("~r~ERROR: ~w~You specified an invalid group.");
                }
                else
                {
                    account.CharacterController.AddGroup(GroupController.Group, true);
                    account.CharacterController.SetActiveGroup(GroupController.Group);
                    API.shared.sendChatMessageToPlayer(player, "You joined: " + GroupController.Group.Name);
                }
            }
        }
    }
}
