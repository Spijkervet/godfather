using TheGodfatherGM.Data.Attributes;
using TheGodfatherGM.Data.Enums;
using TheGodfatherGM.Data.Extensions;
using GTANetworkServer;
using GTANetworkShared;
using System.Collections.Generic;
using System.Linq;
using TheGodfatherGM.Data;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.Property
{
    public class PropertyController : Script
    {
        public Data.Property PropertyData;

        private string ownername = "None";
        public Groups.GroupController GroupController { get; private set; }

        public Marker ExteriorMarker { get; private set; }
        public TextLabel ExteriorTextLabel { get; private set; }
        public ColShape ExteriorColShape { get; private set; }
        public Marker InteriorMarker { get; private set; }
        public TextLabel InteriorTextLabel { get; private set; }
        public ColShape InteteriorColShape { get; private set; }
        public Blip Blip { get; private set; }

        public PropertyController() { }

        public PropertyController(Data.Property PropertyData)
        {
            this.PropertyData = PropertyData;
        }

        public static void LoadProperties()
        {
            foreach (var property in ContextFactory.Instance.Property.ToList())
            {
                PropertyController propertyController = new PropertyController(property);
                if (property.Group != null)
                {
                    propertyController.GroupController = EntityManager.GetGroup(property.Group.Id);
                    API.shared.consoleOutput("Loaded property " + property.PropertyID + " with group: " + propertyController.GroupController.Group.Name);
                    if (propertyController.GroupController != null)
                    {
                        string name = property.Group.Name;
                        if (name == null) propertyController.ownername = "None";
                        else propertyController.ownername = property.Name;

                        if (propertyController.GroupController.Group.Type == GroupType.Business) // If property's group is a business, initialize items.
                        {
                        }
                    }
                }
                else if (property.Character != null)
                {
                    string name = property.Character.Name;
                    if (name == null) propertyController.ownername = "None";
                    else propertyController.ownername = name.Replace("_", " ");
                }

                propertyController.CreateWorldEntity();
                EntityManager.Add(propertyController);

            }
            API.shared.consoleOutput("[GM] Loaded properties: " + ContextFactory.Instance.Property.Count());
        }

        public void CreateWorldEntity()
        {
            ExteriorMarker = API.shared.createMarker(1, new Vector3(PropertyData.ExtPosX, PropertyData.ExtPosY, PropertyData.ExtPosZ) - new Vector3(0, 0, 1f), new Vector3(), new Vector3(),
               new Vector3(1f, 1f, 1f), 100, 255, 255, 255);
            InteriorMarker = API.shared.createMarker(1, new Vector3(PropertyData.IntPosX, PropertyData.IntPosY, PropertyData.IntPosZ) - new Vector3(0, 0, 1f), new Vector3(), new Vector3(),
               new Vector3(1f, 1f, 1f), 100, 255, 255, 255);

            ExteriorTextLabel = API.createTextLabel("~g~[Property (ID: " + PropertyData.PropertyID + ")]\n~w~" + PropertyData.Name + "\n~c~Type: " + Type() + "\nOwner: " + ownername, new Vector3(PropertyData.ExtPosX, PropertyData.ExtPosY, PropertyData.ExtPosZ) + new Vector3(0.0f, 0.0f, 0.5f), 15.0f, 0.5f);
            InteriorTextLabel = API.createTextLabel("~w~[Property Exit (ID: " + PropertyData.PropertyID + ")]\n~w~" + PropertyData.Name + "\n~c~Type: " + Type() + "\nOwner: " + ownername, new Vector3(PropertyData.IntPosX, PropertyData.IntPosY, PropertyData.IntPosZ) + new Vector3(0.0f, 0.0f, 0.5f), 15.0f, 0.5f);

            if (PropertyData.Type == PropertyType.Building)
            {
                Blip = API.createBlip(new Vector3(PropertyData.ExtPosX, PropertyData.ExtPosY, PropertyData.ExtPosZ), 0);
                Blip.sprite = GetBlip();
                Blip.name = PropertyData.Name;
            }
            CreateColShape();
        }

        public void CreateColShape()
        {
            ExteriorColShape = API.createCylinderColShape(new Vector3(PropertyData.ExtPosX, PropertyData.ExtPosY, PropertyData.ExtPosZ), 2f, 3f);
            ExteriorColShape.onEntityEnterColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.getPlayerFromHandle(entity)) != null)
                {
                    if (PropertyData.Enterable)
                    {
                        API.shared.sendNotificationToPlayer(player, "This is a " + Type() + ".\nPress N to enter.");
                        player.setData("AT_PROPERTY", this);
                    }
                }
            };
            ExteriorColShape.onEntityExitColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.getPlayerFromHandle(entity)) != null)
                {
                    if (PropertyData.Enterable) player.resetData("AT_PROPERTY");
                }
            };

            InteteriorColShape = API.createCylinderColShape(new Vector3(PropertyData.IntPosX, PropertyData.IntPosY, PropertyData.IntPosZ), 2f, 3f);
            InteteriorColShape.onEntityEnterColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.getPlayerFromHandle(entity)) != null)
                {
                    if (PropertyData.Enterable)
                    {
                        API.shared.sendNotificationToPlayer(player, "This is a " + Type() + ".\nPress N to exit.");
                        player.setData("AT_PROPERTY", this);
                    }
                }
            };
            InteteriorColShape.onEntityExitColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.getPlayerFromHandle(entity)) != null)
                {
                    if (PropertyData.Enterable) player.resetData("AT_PROPERTY");
                }
            };
        }

        public static void CreateProperty(Client player, string ownerType, PropertyType type, string Name)
        {
            Data.Property propertyData = new Data.Property();
            string ownerName;
            if (ownerType == "player")
            {
                AccountController TargetAccountController = AccountController.GetAccountControllerFromName(Name);
                if (TargetAccountController == null) return;
                propertyData.Character = TargetAccountController.CharacterController.Character;
                ownerName = TargetAccountController.CharacterController.FormatName;
            }
            else if (ownerType == "group")
            {
                Groups.GroupController GroupController = EntityManager.GetGroup(player, Name);
                if (GroupController == null) return;

                propertyData.Group = GroupController.Group;
                ownerName = GroupController.Group.Name;
            }
            else
            {
                API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You specified an invalid owner type (player/group");
                return;
            }

            PropertyController PropertyController = new PropertyController(propertyData);

            propertyData.Type = type;
            propertyData.ExtPosX = player.position.X;
            propertyData.ExtPosY = player.position.Y;
            propertyData.ExtPosZ = player.position.Z;
            PropertyController.ownername = ownerName;

            ContextFactory.Instance.Property.Add(propertyData);
            ContextFactory.Instance.SaveChanges();
            PropertyController.CreateWorldEntity();
            API.shared.sendChatMessageToPlayer(player, "~g~Server: ~w~ Added a " + PropertyController.Type() + " owned by " + ownerName);
        }

        public int GetBlip()
        {
            const int DefaultBlipId = 1;

            if (PropertyData.Character != null) return 40;
            if(GroupController.Group.ExtraType != 0)
            {
                return GroupController.Group.ExtraType.GetAttributeOfType<BlipTypeAttribute>()?.BlipId ?? DefaultBlipId;
            }
            return GroupController.Group.Type.GetAttributeOfType<BlipTypeAttribute>()?.BlipId ?? DefaultBlipId;
        }

        public string Type()
        {
            return PropertyData.Type.GetDisplayName();
        }

        public void PropertyDoor(Client player)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;
            if (player.isInVehicle) return;

           
            if (!PropertyData.Enterable)
            {
                API.shared.sendNotificationToPlayer(player, "~g~Server: ~w~You cannot enter this property.");
                return;
            }

            if (player.getData("IN_PROP") == this)
            {
                if (PropertyData.IPL != null) API.shared.sendNativeToPlayer(player, Hash.REMOVE_IPL, PropertyData.IPL); // API.removeIpl(property.IPL);
                player.resetData("IN_PROP");
                player.dimension = 0;
                player.position = ExteriorMarker.position;
            }
            else
            {
                if (PropertyData.IPL != null) API.shared.sendNativeToPlayer(player, Hash.REQUEST_IPL, PropertyData.IPL);  // API.requestIpl(property.IPL);
                player.setData("IN_PROP", this);
                player.position = InteriorMarker.position;
                player.dimension = PropertyData.PropertyID;
            }
        }

        [Command]
        public void door(Client player)
        {
            PropertyDoor(player);
        }
    }
}
