using GTANetworkServer;
using TheGodfatherGM.Server.Characters;
using System.Collections.Generic;
using TheGodfatherGM.Server.Property;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Data;
using TheGodfatherGM.Data.Enums;

namespace TheGodfatherGM.Server.Menu
{
    public class MenuManager : Script
    {
        public MenuManager()
        {
            API.onClientEventTrigger += onClientEventTrigger;
        }

        private void onClientEventTrigger(Client player, string eventName, object[] args)
        {
            if(eventName == "menu_handler_select_item")
            {
                int callback = (int)args[0];
                if(callback == 0) // Character Menu
                {
                    if ((int)args[1] == (int)args[2]-1) CharacterController.CreateCharacter(player);
                    else CharacterController.SelectCharacter(player, (int)args[1]+1);
                }
                else if(callback == 1) // Vehicle Menu
                {
                    AccountController account = player.getData("ACCOUNT");
                    List<int> VehicleIDs = player.getData("VSTORAGE");

                    int vehID = VehicleIDs[(int)args[1]];
                    Vehicles.VehicleController _VehicleController = EntityManager.GetVehicle(vehID);
                    if (_VehicleController == null) Vehicles.VehicleController.LoadVehicle(account, vehID);
                    else _VehicleController.UnloadVehicle(account);
                    player.resetData("VSTORAGE");
                }
            }
        }
    }
}
