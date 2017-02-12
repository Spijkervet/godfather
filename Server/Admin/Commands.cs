using GTANetworkServer;
using GTANetworkShared;
using TheGodfatherGM.Server.Property;
using TheGodfatherGM.Server.Jobs;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Data.Enums;

namespace TheGodfatherGM.Server.Admin
{
    public class Commands : Script
    {
        [Command("anim")]
        public void anim(Client player, string animDict, string animName, int flag)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("goto", account)) return;

            account.CharacterController.PlayAnimation(player, animDict, animName, flag);
        }

        [Command("scene")]
        public void scene(Client player, string scenario)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("goto", account)) return;

            account.CharacterController.PlayScenario(player, scenario);
        }

        [Command("gotoco")]
        public void gotoco(Client player, float x, float y, float z)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("goto", account)) return;

            player.position = new Vector3(x, y, z);
        }

        [Command("gotoid", GreedyArg = true)]
        public void gotoid(Client player, string NameOrID)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("goto", account)) return;

            AccountController target = EntityManager.GetUserAccount(player, NameOrID);
            if (target == null) return;
            player.position = target.Client.position;
            player.dimension = target.Client.dimension;
        }

        [Command("gethere", GreedyArg = true)]
        public void gethere(Client player, string NameOrID)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("goto", account)) return;

            AccountController target = EntityManager.GetUserAccount(player, NameOrID);
            if (target == null) return;
            target.Client.position = player.position;
            target.Client.dimension = player.dimension;
        }

        [Command("goto", "/goto [type (property/job/place)] [id]")]
        public void go(Client player, string type, int id)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;

            if (!AdminController.AdminRankCheck("goto", account)) return;

            player.dimension = 0;
            if (type == "property")
            {
                PropertyController _PropertyController = EntityManager.GetProperty(id);
                if (_PropertyController == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You specified an invalid ID.");
                }
                else player.position = _PropertyController.ExteriorMarker.position;
            }
            else if (type == "job")
            {
                JobController job = EntityManager.GetJob(id);
                if (job == null)
                {
                    API.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You specified an invalid ID.");
                }
                else player.position = new Vector3(job.JobData.PosX, job.JobData.PosY, job.JobData.PosZ);
            }
            else
            {
                player.dimension = 0;
                if (id == 0)
                {
                    player.position =  SpawnManager.GetSpawnPosition();
                    player.dimension = SpawnManager.GetSpawnDimension();
                }
                else if (id == 1) API.setEntityPosition(player, new Vector3(-365.425, -131.809, 37.873));
                else if (id == 2) API.setEntityPosition(player, new Vector3(-2023.661, -1038.038, 5.577));
                else if (id == 3) API.setEntityPosition(player, new Vector3(3069.330, -4704.220, 15.043));
                else if (id == 4) API.setEntityPosition(player, new Vector3(2052.000, 3237.000, 1456.973));
                else if (id == 5) API.setEntityPosition(player, new Vector3(-129.964, 8130.873, 6705.307));
                else if (id == 6) API.setEntityPosition(player, new Vector3(134.085, -637.859, 262.851));
                else if (id == 7) API.setEntityPosition(player, new Vector3(150.126, -754.591, 262.865));
                else if (id == 8) API.setEntityPosition(player, new Vector3(-75.015, -818.215, 326.176));
                else if (id == 9) API.setEntityPosition(player, new Vector3(450.718, 5566.614, 806.183));
                else if (id == 10) API.setEntityPosition(player, new Vector3(24.775, 7644.102, 19.055));
                else if (id == 11) API.setEntityPosition(player, new Vector3(686.245, 577.950, 130.461));
                else if (id == 12) API.setEntityPosition(player, new Vector3(205.316, 1167.378, 227.005));
                else if (id == 13) API.setEntityPosition(player, new Vector3(-20.004, -10.889, 500.602));
                else if (id == 14) API.setEntityPosition(player, new Vector3(-438.804, 1076.097, 352.411));
                else if (id == 15) API.setEntityPosition(player, new Vector3(-2243.810, 264.048, 174.615));
                else if (id == 16) API.setEntityPosition(player, new Vector3(-3426.683, 967.738, 8.347));
                else if (id == 17) API.setEntityPosition(player, new Vector3(-275.522, 6635.835, 7.425));
                else if (id == 18) API.setEntityPosition(player, new Vector3(-1006.402, 6272.383, 1.503));
                else if (id == 19) API.setEntityPosition(player, new Vector3(-517.869, 4425.284, 89.795));
                else if (id == 20) API.setEntityPosition(player, new Vector3(-1170.841, 4926.646, 224.295));
                else if (id == 21) API.setEntityPosition(player, new Vector3(-324.300, -1968.545, 67.002));
                else if (id == 22) API.setEntityPosition(player, new Vector3(-1868.971, 2095.674, 139.115));
                else if (id == 23) API.setEntityPosition(player, new Vector3(2476.712, 3789.645, 41.226));
                else if (id == 24) API.setEntityPosition(player, new Vector3(-2639.872, 1866.812, 160.135));
                else if (id == 25) API.setEntityPosition(player, new Vector3(-595.342, 2086.008, 131.412));
                else if (id == 26) API.setEntityPosition(player, new Vector3(2208.777, 5578.235, 53.735));
                else if (id == 27) API.setEntityPosition(player, new Vector3(126.975, 3714.419, 46.827));
                else if (id == 28) API.setEntityPosition(player, new Vector3(2395.096, 3049.616, 60.053));
                else if (id == 29) API.setEntityPosition(player, new Vector3(2034.988, 2953.105, 74.602));
                else if (id == 30) API.setEntityPosition(player, new Vector3(2062.123, 2942.055, 47.431));
                else if (id == 31) API.setEntityPosition(player, new Vector3(2026.677, 1842.684, 133.313));
                else if (id == 32) API.setEntityPosition(player, new Vector3(1051.209, 2280.452, 89.727));
                else if (id == 33) API.setEntityPosition(player, new Vector3(736.153, 2583.143, 79.634));
                else if (id == 34) API.setEntityPosition(player, new Vector3(2954.196, 2783.410, 41.004));
                else if (id == 35) API.setEntityPosition(player, new Vector3(2732.931, 1577.540, 83.671));
                else if (id == 36) API.setEntityPosition(player, new Vector3(486.417, -3339.692, 6.070));
                else if (id == 37) API.setEntityPosition(player, new Vector3(899.678, -2882.191, 19.013));
                else if (id == 38) API.setEntityPosition(player, new Vector3(-1850.127, -1231.751, 13.017));
                else if (id == 39) API.setEntityPosition(player, new Vector3(-1475.234, 167.088, 55.841));
                else if (id == 40) API.setEntityPosition(player, new Vector3(3059.620, 5564.246, 197.091));
                else if (id == 41) API.setEntityPosition(player, new Vector3(2535.243, -383.799, 92.993));
                else if (id == 42) API.setEntityPosition(player, new Vector3(971.245, -1620.993, 30.111));
                else if (id == 43) API.setEntityPosition(player, new Vector3(293.089, 180.466, 104.301));
                else if (id == 44) API.setEntityPosition(player, new Vector3(-1374.881, -1398.835, 6.141));
                else if (id == 45) API.setEntityPosition(player, new Vector3(718.341, -1218.714, 26.014));
                else if (id == 46) API.setEntityPosition(player, new Vector3(925.329, 46.152, 80.908));
                else if (id == 47) API.setEntityPosition(player, new Vector3(-1696.866, 142.747, 64.372));
                else if (id == 48) API.setEntityPosition(player, new Vector3(-543.932, -2225.543, 122.366));
                else if (id == 49) API.setEntityPosition(player, new Vector3(1660.369, -12.013, 170.020));
                else if (id == 50) API.setEntityPosition(player, new Vector3(2877.633, 5911.078, 369.624));
                else if (id == 51) API.setEntityPosition(player, new Vector3(-889.655, -853.499, 20.566));
                else if (id == 52) API.setEntityPosition(player, new Vector3(-695.025, 82.955, 55.855));
                else if (id == 53) API.setEntityPosition(player, new Vector3(-1330.911, 340.871, 64.078));
                else if (id == 54) API.setEntityPosition(player, new Vector3(711.362, 1198.134, 348.526));
                else if (id == 55) API.setEntityPosition(player, new Vector3(-1336.715, 59.051, 55.246));
                else if (id == 56) API.setEntityPosition(player, new Vector3(-31.010, 6316.830, 40.083));
                else if (id == 57) API.setEntityPosition(player, new Vector3(-635.463, -242.402, 38.175));
                else if (id == 58) API.setEntityPosition(player, new Vector3(-3022.222, 39.968, 13.611));
                else if (id == 59) API.setEntityPosition(player, new Vector3(-1659993, -128.399, 59.954));
                else if (id == 60) API.setEntityPosition(player, new Vector3(-549.467, 5308.221, 114.146));
                else if (id == 61) API.setEntityPosition(player, new Vector3(1070.206, -711.958, 58.483));
                else if (id == 62) API.setEntityPosition(player, new Vector3(1608.698, 6438.096, 37.637));
                else if (id == 63) API.setEntityPosition(player, new Vector3(3430.155, 5174.196, 41.280));
                else if (id == 64) API.setEntityPosition(player, new Vector3(3464.689, 5252.736, 20.29798));
                else if (id == 65) API.setEntityPosition(player, new Vector3(1675.97961, 2585.18457, 45.92)); // Prison
            }
        }

        [Command("makeadmin")]
        public void MakeAdmin(Client player, string IDOrName, int Level)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (AdminController.AdminRankCheck("makeadmin", account))
            {
                AccountController target = EntityManager.GetUserAccount(player, IDOrName);
                if (target == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
                else
                {
                    API.shared.sendChatMessageToPlayer(target.Client, "~y~You were made a " + AdminController.GetAdminRank(Level) + " by " + account.CharacterController.FormatName + ".");
                    API.shared.sendChatMessageToPlayer(player, "~y~You made " + target.CharacterController.FormatName + " a " + AdminController.GetAdminRank(Level) + ".");
                    target.CharacterController.Character.Admin = Level;

                    // DatabaseManager.SaveCharacter(target.Character);
                    // DatabaseManager.executeQuery("UPDATE `characters` SET `Admin` = " + Level + " WHERE `id` = " + target.id);
                }

            }
        }

        [Command]
        public void invincible(Client player)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("makeadmin", account)) return;

            player.invincible = !player.invincible;
            if (player.invincible) API.sendNotificationToPlayer(player, "You are now invincible.");
            else API.sendNotificationToPlayer(player, "You are no longer invincible.");
        }

        [Command]
        public void givegun(Client player, string IDOrName, WeaponHash weapon, int ammo)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (AdminController.AdminRankCheck("givegun", account))
            {
                AccountController target = EntityManager.GetUserAccount(player, IDOrName);
                if (target == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
                else
                {
                    //Storage.ItemController.GiveItem(target.Character, weapon, ammo);
                    API.givePlayerWeapon(target.Client, weapon, ammo, false, false);
                    API.sendChatMessageToPlayer(target.Client, "Administrator " + account.CharacterController.FormatName + " gave you a " + weapon);
                }
            }
        }

        [Command]
        public void setskin(Client player, string IDOrName, PedHash Model)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (AdminController.AdminRankCheck("setskin", account))
            {
                AccountController targetAccount = EntityManager.GetUserAccount(player, IDOrName);
                if (targetAccount == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
                else
                {

                    API.setPlayerSkin(targetAccount.Client, Model);
                    API.shared.sendChatMessageToPlayer(targetAccount.Client, "Administrator " + account.CharacterController.FormatName + " set your skin to: " + Model.ToString());
                    targetAccount.CharacterController.Character.Model = Model.GetHashCode();
                }

            }
        }


        [Command]
        public void sethealth(Client player, string IDOrName, int health)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("sethealth", account)) return;
            AccountController targetAccount = EntityManager.GetUserAccount(player, IDOrName);
            if (targetAccount == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
            else
            {
                targetAccount.Client.health = health;
            }
        }

        [Command]
        public void setarmor(Client player, string IDOrName, int armor)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("setarmor", account)) return;
            AccountController targetAccount = EntityManager.GetUserAccount(player, IDOrName);
            if (targetAccount == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
            else
            {
                targetAccount.Client.armor = armor;
            }
        }

        [Command]
        public void setmoney(Client player, string IDOrName, int money)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("setmoney", account)) return;
            AccountController targetAccount = EntityManager.GetUserAccount(player, IDOrName);
            if (targetAccount == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
            else
            {
                targetAccount.CharacterController.Character.Cash = money;
                account.Client.sendChatMessage("~y~You set " + targetAccount.CharacterController.FormatName + "'s money to $" + money);
                targetAccount.Client.sendChatMessage("~g~SERVER: ~w~Adminitrator " + account.CharacterController.FormatName + " set your money to $" + money);
                API.triggerClientEvent(targetAccount.Client, "update_money_display", targetAccount.CharacterController.Character.Cash);
                targetAccount.CharacterController.Save();
            }
        }

        [Command]
        public void givemoney(Client player, string IDOrName, int money)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("setmoney", account)) return;
            AccountController targetAccount = EntityManager.GetUserAccount(player, IDOrName);
            if (targetAccount == null) API.sendChatMessageToPlayer(player, "~r~[ERROR]: ~w~You specified an invalid player ID.");
            else
            {
                targetAccount.CharacterController.Character.Cash += money;
                account.Client.sendChatMessage("~y~You gave " + targetAccount.CharacterController.FormatName + " $" + money);
                targetAccount.Client.sendChatMessage("~g~SERVER: ~w~Adminitrator " + account.CharacterController.FormatName + " gave you $" + money);
                API.triggerClientEvent(targetAccount.Client, "update_money_display", targetAccount.CharacterController.Character.Cash);
                targetAccount.CharacterController.Save();
            }
        }

        [Command("createvehicle", "~y~Usage: ~w~/createvehicle [player/group] [ID/Part of Name] [Model] [Color1] [Color2]")]
        public void createvehicle(Client player, string OwnerType, string Name, VehicleHash hash, int color1, int color2)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("createvehicle", account)) return;

            Data.Vehicle VehicleData = new Data.Vehicle();
            if (OwnerType == "player")
            {
                AccountController TargetAccountController = AccountController.GetAccountControllerFromName(Name);
                if (TargetAccountController == null) return;
                VehicleData.Character = TargetAccountController.CharacterController.Character;
            }
            else if (OwnerType == "group")
            {
                Groups.GroupController GroupController = EntityManager.GetGroup(player, Name);
                VehicleData.Group = GroupController.Group;
            }
            else
            {
                API.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You specified an invalid owner type (player/group");
                return;
            }

            Vehicles.VehicleController VehicleController = new Vehicles.VehicleController(VehicleData, API.createVehicle(hash, player.position, player.rotation, color1, color2, 0));

            VehicleData.Model = hash.GetHashCode();
            VehicleData.PosX = player.position.X;
            VehicleData.PosY = player.position.Y;
            VehicleData.PosZ = player.position.Z;
            VehicleData.Rot = player.rotation.Z;
            VehicleData.Color1 = color1;
            VehicleData.Color2 = color2;

            ContextFactory.Instance.Vehicle.Add(VehicleData);
            ContextFactory.Instance.SaveChanges();
        }

        [Command("editvehicle", "~y~Usage: ~w~/editvehicle [position]")]
        public void editvehicle(Client player, string choice)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("createvehicle", account)) return;

            Vehicles.VehicleController _VehicleController = EntityManager.GetVehicle(player.vehicle);
            Data.Vehicle VM = _VehicleController.VehicleData;
            if (VM != null)
            {
                if (choice == "position")
                {
                    Vector3 newPos = player.vehicle.position + new Vector3(0.0f, 0.0f, 0.5f);
                    VM.PosX = newPos.X;
                    VM.PosY = newPos.Y;
                    VM.PosZ = newPos.Z;

                    player.sendChatMessage("You ~g~successfully ~w~set the vehicle's position.");
                    ContextFactory.Instance.SaveChanges();
                }
            }
            else player.sendChatMessage("~r~ERROR: ~w~You are not in a vehicle.");
        }

        [Command("createproperty", "~y~Usage: ~w~/createproperty [player/group] [type] [ID/Part of Name]\nTypes: House (0), Door (1), Building (2)")]
        public void createproperty(Client player, string OwnerType, PropertyType type, string IDOrName)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("createproperty", account)) return;

            PropertyController.CreateProperty(player, OwnerType, type, IDOrName);
        }

        [Command("setproperty", "~y~Usage: ~w~/setproperty [id] [interior/exterior/IPL/Enterable]")]
        public void setproperty(Client player, int id, string msg)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (!AdminController.AdminRankCheck("setproperty", account)) return;

            PropertyController PropertyController = EntityManager.GetProperty(id);
            if (PropertyController == null)
            {
                API.sendNotificationToPlayer(player, "You specified an invalid property.");
                return;
            }

            if (msg.ToLower() == "interior")
            {
                PropertyController.PropertyData.IntPosX = player.position.X;
                PropertyController.PropertyData.IntPosY = player.position.Y;
                PropertyController.PropertyData.IntPosZ = player.position.Z;

                PropertyController.InteriorMarker.position = player.position - new Vector3(0.0f, 0.0f, 1.0f);
                PropertyController.InteriorTextLabel.position = player.position + new Vector3(0.0f, 0.0f, 0.5f);
                API.deleteColShape(PropertyController.InteteriorColShape);
                PropertyController.CreateColShape();
                ContextFactory.Instance.SaveChanges();
            }
            else if (msg.ToLower() == "exterior")
            {
                PropertyController.PropertyData.ExtPosX = player.position.X;
                PropertyController.PropertyData.ExtPosY = player.position.Y;
                PropertyController.PropertyData.ExtPosZ = player.position.Z;

                PropertyController.ExteriorMarker.position = player.position - new Vector3(0.0f, 0.0f, 1.0f);
                PropertyController.ExteriorTextLabel.position = player.position + new Vector3(0.0f, 0.0f, 0.5f);
                API.deleteColShape(PropertyController.ExteriorColShape);
                PropertyController.CreateColShape();
                ContextFactory.Instance.SaveChanges();
            }
            else if (msg.ToLower() == "ipl")
            {
                PropertyController.PropertyData.IPL = msg;
                ContextFactory.Instance.SaveChanges();
            }
            else if (msg.ToLower() == "enterable")
            {
                bool IsEnterable = PropertyController.PropertyData.Enterable;
                PropertyController.PropertyData.Enterable = !IsEnterable;
                API.sendChatMessageToPlayer(player, "You set the property to: " + (IsEnterable ? "Enterable" : "Closed"));
                ContextFactory.Instance.SaveChanges();
            }
            else API.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You specified an invalid option.");
        }
        
    }
}
