using GTANetworkServer;
using System.Collections.Generic;
using System.Linq;
using GTANetworkShared;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Server.Characters;


namespace TheGodfatherGM.Server.Vehicles
{
    public class VehicleController : Script
    {
        public Data.Vehicle VehicleData;
        public Vehicle Vehicle;
        public Groups.GroupController Group;

        public VehicleController()
        {
            API.onVehicleDeath += OnVehicleExplode;
            API.onPlayerEnterVehicle += OnPlayerEnterVehicle;
            API.onPlayerExitVehicle += OnPlayerExitVehicle;
        }

        public VehicleController(Data.Vehicle VehicleData, Vehicle vehicle)
        {
            this.VehicleData = VehicleData;
            Vehicle = vehicle;
            API.setVehicleEngineStatus(vehicle, false); // Engine is always off.
            EntityManager.Add(this);
        }


        private void OnPlayerExitVehicle(Client player, NetHandle vehicle)
        {
            API.triggerClientEvent(player, "hide_vehicle_hud");
        }

        private void OnPlayerEnterVehicle(Client player, NetHandle vehicle)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;

            VehicleController _VehicleController = EntityManager.GetVehicle(vehicle);
            if (_VehicleController != null)
            {
                if (_VehicleController.VehicleData.Character == account.CharacterController.Character)
                {
                    API.sendNotificationToPlayer(player, "You have entered your car.");
                }
                else if (account.CharacterController.IsInGroup(_VehicleController.Group.Group.Id))
                {
                    API.sendNotificationToPlayer(player, "You have entered a vehicle of your organization.");

                }
            }
            API.triggerClientEvent(player, "show_vehicle_hud");
        }


        public void OnVehicleExplode(NetHandle vehicle)
        {
            /*
            * VehicleData VM = EntityManager.GetVehicle(vehicle);
            if (VM.respawnable)
            {
                API.sendChatMessageToAll("Vehicle respawning...");
                API.delay(10000, true, () => // 10 seconds between vehicle explosion and respawning
                {
                    API.sendChatMessageToAll("Vehicle respawned!...");
                    API.deleteEntity(vehicle);
                    VehicleData VM2 = new VehicleData(API.createVehicle((VehicleHash)VM.ModelHash, VM.vehPos, new Vector3(0, 0, VM.vehRotZ), VM.color1, VM.color1));
                    VM2.id = VM.id;
                });
            }
            */
        }

        public static void LoadVehicles()
        {
            foreach (var vehicle in ContextFactory.Instance.Vehicle.Where(x => x.Group != null).ToList())
            {
                VehicleController VehicleController = new VehicleController(vehicle, API.shared.createVehicle((VehicleHash)vehicle.Model, new Vector3(vehicle.PosX, vehicle.PosY, vehicle.PosZ), new Vector3(0.0f, 0.0f, vehicle.Rot), vehicle.Color1, vehicle.Color2));
                if (vehicle.Group != null) // -1 is reserved for non-group job vehicles.
                {
                    VehicleController.Group = EntityManager.GetGroup(vehicle.Group.Id);
                }
            }
            API.shared.consoleOutput("[GM] Loaded vehicles: " + ContextFactory.Instance.Vehicle.Count());
        }

        public static List<Data.Vehicle> GetVehicles(AccountController account)
        {
            return ContextFactory.Instance.Vehicle.Where(x => x.Character == account.CharacterController.Character).ToList();
        }

        public static void LoadVehicle(AccountController account, int id)
        {
            Data.Vehicle VM = ContextFactory.Instance.Vehicle.Where(x => x.Id == id).FirstOrDefault();
            if (VM != null)
            {
                VehicleController _vehicle = new VehicleController(VM, API.shared.createVehicle((VehicleHash)VM.Model, new Vector3(VM.PosX, VM.PosY, VM.PosZ), new Vector3(0.0f, 0.0f, VM.Rot), VM.Color1, VM.Color2));
                API.shared.sendNotificationToPlayer(account.Client, "You spawned your " + API.shared.getVehicleDisplayName((VehicleHash)VM.Model));
            }
        }

        public static void UnloadVehicles(AccountController account)
        {
            List<VehicleController> Vehicles = EntityManager.GetVehicleControllers(account);
            foreach (var vehicle in Vehicles)
            {
                if (vehicle != null)
                {
                    vehicle.UnloadVehicle(account);
                }
            }
        }

        public void UnloadVehicle(AccountController account)
        {
            API.sendNotificationToPlayer(account.Client, "You stored your " + API.getVehicleDisplayName((VehicleHash)Vehicle.model));
            EntityManager.Remove(this);
            Vehicle.delete();
        }

        public static void TriggerDoor(Vehicle vehicle, int DoorID)
        {
            if (vehicle.isDoorOpen(DoorID)) vehicle.closeDoor(DoorID);
            else vehicle.openDoor(DoorID);
        }

        [Command("vstorage")]
        public static void VehicleStorage(Client player)
        {
            AccountController account = API.shared.getEntityData(player, "ACCOUNT");
            if (account == null) return;

            if (account.CharacterController.Character.Vehicle == null)
            {
                API.shared.sendChatMessageToPlayer(account.Client, "You have no vehicles.");
                return;
            }
            else if (account.CharacterController.Character.Vehicle.Count == 0)
            {
                API.shared.sendChatMessageToPlayer(account.Client, "You have no vehicles.");
            }
            else
            {
                List<string> VehicleNames = new List<string>();
                List<int> VehicleIDs = new List<int>();
                foreach (var VehicleData in account.CharacterController.Character.Vehicle)
                {
                    VehicleController _VehicleController = EntityManager.GetVehicle(VehicleData);
                    string isSpawned = (_VehicleController == null ? " (stored)" : " (spawned)");
                    VehicleNames.Add(API.shared.getVehicleDisplayName((VehicleHash)VehicleData.Model) + isSpawned);
                    VehicleIDs.Add(VehicleData.Id);
                }

                player.setData("VSTORAGE", VehicleIDs);
                API.shared.triggerClientEvent(account.Client, "create_menu", 1, null, "Vehicles", false, VehicleNames.ToArray());
            }
        }

        public bool CheckAccess(AccountController account)
        {
            if(Group == null)
            {
                if (VehicleData.Character == account.CharacterController.Character) return true;
            }
            else
            {
                if (account.CharacterController.IsInGroup(Group.Group.Id)) return true;
            }
            return false;
        }

        public bool CheckAccess(AccountController account, CharacterController Character)
        {
            if (VehicleData.Character == Character.Character) return true;
            return false;
        }

        public void ParkVehicle(Client player)
        {
            if (player.velocity != new Vector3(0.0f, 0.0f, 0.0f))
            {
                API.sendNotificationToPlayer(player, "You must not be moving to park your vehicle.");
                return;
            }

            Vector3 firstPos = player.vehicle.position;
            API.sendNotificationToPlayer(player, "Don't move while parking your vehicle.");
            Global.Util.delay(2500, () =>
            {
                if (player.vehicle != null)
                {
                    if (firstPos.DistanceTo(player.vehicle.position) <= 5.0f)
                    {
                        VehicleController _VehicleController = EntityManager.GetVehicle(player.vehicle);
                        Vector3 newPos = player.vehicle.position + new Vector3(0.0f, 0.0f, 0.5f);
                        _VehicleController.VehicleData.PosX = newPos.X;
                        _VehicleController.VehicleData.PosX = newPos.Y;
                        _VehicleController.VehicleData.PosX = newPos.Z;

                        API.sendNotificationToPlayer(player, "~g~Server: ~w~Your vehicle is parked!");

                    }
                    else API.sendNotificationToPlayer(player, "~y~You moved the car while trying to park your vehicle.");
                }
            });

        }
    }
}
