using GTANetworkServer;
using TheGodfatherGM.Data.Enums;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.Vehicles
{
    class Commands : Script
    {
        public Commands() { }

        [Command("car", "~y~USAGE: ~w~/car [engine/hood/trunk]")]
        public void car(Client player, string Choice)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;

            if (Choice == "engine")
            {
                VehicleController VehicleController = EntityManager.GetVehicle(player.vehicle);
                if (VehicleController == null || player.vehicleSeat != -1)
                {
                    API.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You are not in a vehicle or on the driver's seat.");
                    return;
                }

                if (!VehicleController.CheckAccess(account))
                {
                    API.sendNotificationToPlayer(player, "You cannot operate this vehicle.");
                    return;
                }
                else
                {
                    if (API.getVehicleEngineStatus(VehicleController.Vehicle))
                    {
                        VehicleController.Vehicle.engineStatus = false;
                        ChatController.sendProxMessage(player, 15.0f, "~#C2A2DA~", account.CharacterController.FormatName + " turns the key in the ignition and the engine stops.");
                    }
                    else
                    {
                        VehicleController.Vehicle.engineStatus = true;
                        ChatController.sendProxMessage(player, 15.0f, "~#C2A2DA~", account.CharacterController.FormatName + " turns the key in the ignition and the engine starts.");
                    }
                }
            }
            else if(Choice == "park")
            {
                VehicleController VehicleController = EntityManager.GetVehicle(player.vehicle);
                Data.Vehicle VM = VehicleController.VehicleData;
                if (VM == null || player.vehicleSeat != -1)
                {
                    API.sendNotificationToPlayer(player, "~r~ERROR: ~w~You are not in a vehicle or on the driver's seat.");
                    return;
                }

                if (VehicleController.CheckAccess(account, account.CharacterController))
                {
                    VehicleController.ParkVehicle(player);
                }
                else API.sendNotificationToPlayer(player, "~r~ERROR: ~w~You cannot park this car.");
            }

            else if (Choice == "hood" || Choice == "trunk")
            {
                VehicleController VehicleController = null;
                if (player.isInVehicle) VehicleController = EntityManager.GetVehicle(player.vehicle);
                else VehicleController = EntityManager.GetVehicleControllers().Find(x => x.Vehicle.position.DistanceTo(player.position) < 3.0f);

                if(VehicleController == null)
                {
                    API.sendNotificationToPlayer(player, "You are not near a vehicle.");
                    return;
                }

                if (VehicleController.CheckAccess(account))
                {
                    if (Choice == "hood") VehicleController.TriggerDoor(VehicleController.Vehicle, 4);
                    else VehicleController.TriggerDoor(VehicleController.Vehicle, 5);
                }
                else API.sendNotificationToPlayer(player, "~r~ERROR: ~w~You cannot park this car.");
            }
        }
    }
}