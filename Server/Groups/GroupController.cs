using TheGodfatherGM.Data.Extensions;
using GTANetworkServer;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Data;

namespace TheGodfatherGM.Server.Groups
{
    public class GroupController
    {

        /*public string GetRankName(CharacterController Character)
        {
            return GroupData.Members.FirstOrDefault(x => x.CharacterID == Character.CharacterData.CharacterID).DivisionID.ToString();
        }*/

        public Group Group;

        public GroupController() { }

        public GroupController(Data.Group GroupData)
        {
            this.Group = GroupData;
            EntityManager.Add(this);
        }

        public static void LoadGroups()
        {
            foreach (var group in ContextFactory.Instance.Group)
            {
                new GroupController(group);
            }
            API.shared.consoleOutput("[GM] Loaded groups: " + ContextFactory.Instance.Group.Count());
        }

        public static string GetName(int groupId)
        {
            if (groupId > -1)
            {
                return EntityManager.GetGroups()[groupId].Group.Name;
            }
            return "Invalid GroupID";
        }

        public string Type()
        {
            return Group.Type.GetDisplayName();
        }

        public string ExtraType()
        {
            return Group.ExtraType.GetDisplayName();
        }
    }
}
