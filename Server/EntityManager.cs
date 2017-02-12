using GTANetworkServer;
using GTANetworkShared;
using System;
using System.Collections.Generic;
using TheGodfatherGM.Server.Property;
using TheGodfatherGM.Server.Jobs;
using System.Linq;
using TheGodfatherGM.Server.Vehicles;
using TheGodfatherGM.Server.Groups;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server
{
    public class EntityManager
    {
        private static List<VehicleController> _VehicleControllers = new List<VehicleController>();
        private static List<GroupController> Groups = new List<GroupController>();
        private static List<PropertyController> Properties = new List<PropertyController>();
        private static List<JobController> Jobs = new List<JobController>();

        public static void Init()
        {
            GroupController.LoadGroups();
            VehicleController.LoadVehicles();
            PropertyController.LoadProperties();
            JobController.LoadJobs();
        }

        private static Dictionary<int, AccountController> accountDictionary = new Dictionary<int, AccountController>();
        public static AccountController GetUserAccount(int id)
        {
            if(id > -1) return accountDictionary.Get(id);
            return null;
        }


        public static AccountController GetUserAccount(Client player, string IDOrName)
        {
            int id;
            int count = 0;
            if (int.TryParse(IDOrName, out id))
            {
                return GetUserAccount(id);
            }

            AccountController rAccount = null;
            foreach (KeyValuePair<int, AccountController> account in accountDictionary)
            {
                if (account.Value.CharacterController.Character.Name.ToLower().StartsWith(IDOrName.ToLower()))
                {
                    if ((account.Value.CharacterController.Character.Name.Equals(IDOrName, StringComparison.OrdinalIgnoreCase)))
                    {
                        return account.Value;
                    }
                    rAccount = account.Value;
                    count++;
                }
            }
            if (count == 1) return rAccount;
            else if(count > 1)
            {
                API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~Multiple players found.");
            }
            return null;
        }

        public static void ListUserAccounts(Client player, string IDOrName)
        {
            int count = 0;
            foreach (KeyValuePair<int, AccountController> userAccount in accountDictionary)
            {
                if (userAccount.Value.CharacterController.Character.Name.ToLower().Contains(IDOrName.ToLower()))
                {
                    API.shared.sendChatMessageToPlayer(player, "" + userAccount.Value.CharacterController.FormatName + " (ID: " + userAccount.Value.PlayerId + ") - (Level: " + userAccount.Value.CharacterController.Character.Level + ") - (Ping: " + API.shared.getPlayerPing(userAccount.Value.Client) + ")");
                    count++;
                }
            }
            if(count == 0) API.shared.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
        }

        public static void Add(AccountController account)
        {
            int id = GetNextID();
            foreach (Client client in API.shared.getAllPlayers())
            {
                AccountController accountController;
                if ((accountController = client.getData("ACCOUNT")) != null && accountController.PlayerId == id)
                {
                    API.shared.consoleOutput("DUPLICATE ID FOUND AT ID" + id);
                    id = GetNextID();
                    API.shared.consoleOutput("GENERATED NEW ID: " + id);
                }
            }
            accountDictionary.Add(id, account);
            account.PlayerId = id;
        }

        public static void Remove(AccountController account)
        {
            accountDictionary.Remove(account.PlayerId);
        }

        public static ICollection<Data.Vehicle> GetVehicles(AccountController account)
        {
            return account.CharacterController.Character.Vehicle;
        }

        public static List<VehicleController> GetVehicleControllers(AccountController account)
        {
            return _VehicleControllers.Where(x => x.VehicleData.Character == account.CharacterController.Character).ToList();
        }

        public static List<VehicleController> GetVehicleControllers()
        {
            return _VehicleControllers;
        }

        public static VehicleController GetVehicle(Data.Vehicle VehicleData)
        {
            return _VehicleControllers.Find(x => x.VehicleData == VehicleData); ;
        }

        public static VehicleController GetVehicle(Vehicle vehicle)
        {
            return _VehicleControllers.Find(x => x.Vehicle == vehicle); ;
        }

        public static VehicleController GetVehicle(NetHandle vehicle)
        {
            return _VehicleControllers.Find(x => x.Vehicle.handle == vehicle); ;
        }

        public static VehicleController GetVehicle(int id)
        {
            return _VehicleControllers.Find(x => x.VehicleData.Id == id); ;
        }

        public static void Add(VehicleController vehicle)
        {
            _VehicleControllers.Add(vehicle);
        }

        public static void Remove(VehicleController vehicle)
        {
            _VehicleControllers.Remove(vehicle);
        }

        public static List<GroupController> GetGroups()
        {
            return Groups;
        }

        public static GroupController GetGroup(int id)
        {
            if (id > -1) return Groups.Find(x => x.Group.Id == id); ;
            return null;
        }

        public static GroupController GetGroup(Client player, string IDOrName)
        {
            int id;
            int count = 0;
            if (int.TryParse(IDOrName, out id))
            {
                return GetGroup(id);
            }

            GroupController rGroup = null;
            foreach (var group in Groups)
            {
                if (group.Group.Name.ToLower().StartsWith(IDOrName.ToLower()))
                {
                    if ((group.Group.Name.Equals(IDOrName, StringComparison.OrdinalIgnoreCase)))
                    {
                        return group;
                    }
                    rGroup = group;
                    count++;
                }
            }
            if (count == 1) return rGroup;
            else if (count > 1)
            {
                API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~Multiple groups found.");
            }
            return null;
        }

        public static void Add(GroupController group)
        {
            Groups.Add(group);
        }

        public static void Remove(GroupController group)
        {
            Groups.Remove(group);
        }

        public static List<PropertyController> GetProperties()
        {
            return Properties;
        }

        public static PropertyController GetProperty(int id)
        {
            return Properties.Find(x => x.PropertyData.PropertyID == id);
        }

        public static PropertyController GetProperty(ColShape shape)
        {
            PropertyController rProperty = Properties.Find(x => x.ExteriorColShape == shape);
            if(rProperty == null) rProperty = Properties.Find(x => x.InteteriorColShape == shape);
            return rProperty;
        }

        public static void Add(PropertyController property)
        {
            Properties.Add(property);
        }

        public static void Remove(PropertyController property)
        {
            Properties.Remove(property);
        }

        public static JobController GetJob(int id)
        {
            return Jobs.Find(x => x.JobData.Id == id);
        }

        public static void Add(JobController job)
        {
            Jobs.Add(job);
        }

        public static void Remove(JobController job)
        {
            Jobs.Remove(job);
        }

        public static int GetNextID()
        {
            // int maxplayers = API.shared.getSetting<int>("maxplayers");
            for (int i = 0; i < 1000; i++) // maybe do 1 cuz duplicate 0 okgui
            {
                if (!accountDictionary.ContainsKey(i)) return i;
            }
            return -1;
        }
    }
}
