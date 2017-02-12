using System;
using System.Collections.Generic;
using GTANetworkServer;
using GTANetworkShared;
using TheGodfatherGM.Server.Jobs;
using System.Text.RegularExpressions;
using System.Linq;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Data;
using Data.Interfaces;

namespace TheGodfatherGM.Server.Characters
{
    public class CharacterController
    {

        public Character Character = new Character();

        public AccountController AccountController { get; private set; }
        public string FormatName { get; private set; }
        public JobController job;
        public GroupMember ActiveGroup = new GroupMember();

        public CharacterController(AccountController accountController, Data.Character characterData)
        {
            Character = characterData;
            Character.GroupMember = new List<GroupMember>(); // even kieken of 't nog nodig is.

            AccountController = accountController;

            FormatName = Character.Name.Replace("_", " ");
            // SetActiveGroup(Character.ActiveGroup); TODO: Fix

            LoadProperties();
            switch (Character.RegistrationStep)
            {
                case 0:
                    API.shared.sendChatMessageToPlayer(accountController.Client, string.Format("~w~Welcome {0} to ~g~{1}~w~. This is your first visit. Enjoy your stay!", FormatName, Global.GlobalVars.ServerName));
                    break;
                default:
                    API.shared.sendChatMessageToPlayer(accountController.Client, string.Format("~w~Welcome back {0}! Your last login was at {1}", FormatName, Character.LastLoginDate));
                    break;
            }

            Character.LastLoginDate = DateTime.Now;
            Character.Online = true;
            ContextFactory.Instance.SaveChanges();
        }

        public CharacterController(AccountController accountController, string name)
        {
            accountController.CharacterController = this;
            Character.AccountId = accountController.Account.Id;
            Character.Name = name;
            Character.RegisterDate = DateTime.Now;
            Character.Model = PedHash.DrFriedlander.GetHashCode(); //Global.GlobalVars._defaultPedModel.GetHashCode();
            Character.ModelName = "DrFriedlander";
            ContextFactory.Instance.Character.Add(Character);
            ContextFactory.Instance.SaveChanges();
        }

        public void Save()
        {
            Character.PosX = AccountController.Client.position.X;
            Character.PosY = AccountController.Client.position.Y;
            Character.PosZ = AccountController.Client.position.Z;
            Character.Rot = AccountController.Client.rotation.Z;
            Character.Model = AccountController.Client.model.GetHashCode();
        }

        private void OnEntityEnterColShapeHandler(ColShape colshape, NetHandle entity)
        {

        }

        private void OnPlayerDeathHandler(Client player, NetHandle entityKiller, int weapon)
        {
            API.shared.sendNativeToPlayer(player, Hash._RESET_LOCALPLAYER_STATE, player);
            API.shared.sendNativeToPlayer(player, Hash.RESET_PLAYER_ARREST_STATE, player);

            API.shared.sendNativeToPlayer(player, Hash.IGNORE_NEXT_RESTART, true);
            API.shared.sendNativeToPlayer(player, Hash._DISABLE_AUTOMATIC_RESPAWN, true);

            API.shared.sendNativeToPlayer(player, Hash.SET_FADE_IN_AFTER_DEATH_ARREST, true);
            API.shared.sendNativeToPlayer(player, Hash.SET_FADE_OUT_AFTER_DEATH, false);
            API.shared.sendNativeToPlayer(player, Hash.NETWORK_REQUEST_CONTROL_OF_ENTITY, player);

            API.shared.sendNativeToPlayer(player, Hash.FREEZE_ENTITY_POSITION, player, false);
            API.shared.sendNativeToPlayer(player, Hash.NETWORK_RESURRECT_LOCAL_PLAYER, player.position.X, player.position.Y, player.position.Z, player.rotation.Z, false, false);
            API.shared.sendNativeToPlayer(player, Hash.RESURRECT_PED, player);

            API.shared.sendNativeToPlayer(player, Hash.SET_PED_TO_RAGDOLL, player, true);
            AccountController account = player.getData("ACCOUNT");
        }

        public static void CreateCharacter(Client player)
        {
            API.shared.triggerClientEvent(player, "CEFController", Global.GlobalVars.WebServerConnectionString + "Game/CreateCharacter?socialclub=" + player.socialClubName);
        }

        public static void CharacterMenu(AccountController AccountController)
        {
            var CharacterMenuEntries = new List<string>();
            var Characters = AccountController.Account.Character.ToList();
            if (Characters == null || Characters.Count == 0)
            {
                CharacterMenuEntries.Add("Create Character");
                API.shared.triggerClientEvent(AccountController.Client, "create_menu", 0, null, "Characters", true, CharacterMenuEntries.ToArray());
            }
            else
            {
                foreach (var character in Characters)
                {
                    CharacterMenuEntries.Add(character.Name.Replace("_", " "));
                }
                CharacterMenuEntries.Add("Create new character");
                API.shared.triggerClientEvent(AccountController.Client, "create_menu", 0, null, "Characters", true, CharacterMenuEntries.ToArray());
            }
        }

        public static void SelectCharacter(Client player, int selectId)
        {
            AccountController account = player.getData("ACCOUNT");
            if (account == null) return;

            if (account.Account.Character.Count() == 0)
            {
                API.shared.sendChatMessageToPlayer(player, "You have no characters.");
            }
            else
            {
                int characterid = account.Account.Character.ToList()[selectId - 1].Id;
                Data.Character characterData = ContextFactory.Instance.Character.FirstOrDefault(x => x.Id == characterid);
                CharacterController characterController = new CharacterController(account, characterData);
                characterController.LoginCharacter(account);
            }
        }

        public void LoginCharacter(AccountController accountController)
        {
            AccountController = accountController;
            accountController.CharacterController = this;

            ChatController.LoginMessages(this);
            SpawnManager.SpawnCharacter(this);

            API.shared.triggerClientEvent(accountController.Client, "stopAudio");
            accountController.Client.freeze(false);
            accountController.Client.transparency = 255;
            accountController.Client.nametag = FormatName + " (" + accountController.PlayerId + ")";
            API.shared.triggerClientEvent(accountController.Client, "update_money_display", Character.Cash);
        }

        public void LoadProperties()
        {
            if (Character.Property == null) return;
            foreach (Data.Property property in Character.Property)
            {
                API.shared.triggerClientEvent(AccountController.Client, "create_blip", new Vector3(property.ExtPosX, property.ExtPosY, property.ExtPosZ), 0, 0);
            }
        }

        public int CalculateExperience(int val)
        {
            return ((50 * (val) * (val) * (val) - 150 * (val) * (val) + 600 * (val)) / 5);
        }

        public GroupMember GetGroupInfo(int GroupId)
        {
            return Character.GroupMember.FirstOrDefault(x => x.Group.Id == GroupId);
        }

        public bool IsInGroup(int GroupId)
        {
            if (Character.GroupMember.FirstOrDefault(x => x.Group.Id == GroupId) != null) return true;
            return false;
        }

        public void AddGroup(Data.Group group, bool leader)
        {
            GroupMember memberEntry = new GroupMember();
            memberEntry.Character = Character;
            memberEntry.Group = group;
            memberEntry.Leader = leader;
            Character.GroupMember.Add(memberEntry);
            ContextFactory.Instance.SaveChanges();
        }

        public void SetActiveGroup(Data.Group group)
        {
            if (group == null) return;
            GroupMember GroupInfo = GetGroupInfo(group.Id);
            if (GroupInfo != null)
            {
                ActiveGroup = new GroupMember();
                ActiveGroup.Group = group;
                ActiveGroup.Group = group;
                ActiveGroup.Leader = GroupInfo.Leader;
                Character.ActiveGroupID = GroupInfo.Group.Id;
                API.shared.consoleOutput("Set active group to: " + ActiveGroup.Group.Name);
            }
        }

        public string ListGroups()
        {
            string returnstring = "Groups: ";
            int count = 0;
            foreach (var group in Character.GroupMember)
            {
                if (group == null) returnstring += "None, ";
                else returnstring += "TODO";
                count++;
            }
            if (count == 0) returnstring += "None";
            return returnstring;
        }

        public static bool NameValidityCheck(Client player, string name)
        {
            if (!name.Contains("_") || name.Count(x => x == '_') > 1)
            {
                API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~Your name must have a last name. Please seperate your first- and lastname with an '_'.");
                return false;
            }
            else if (Regex.IsMatch(name, @"^[a-zA-Z_]+$")) return true;
            API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~You entered an invalid name.");
            return false;
        }

        public void PlayScenario(Client player, string scenario)
        {
            player.playScenario(scenario);
            API.shared.triggerClientEvent(player, "animation_text");
        }

        public void PlayAnimation(Client player, string animDict, string animName, int flag)
        {
            player.playAnimation(animDict, animName, flag);
            API.shared.triggerClientEvent(player, "animation_text");
        }

        public static void StopAnimation(Client player)
        {
            player.stopAnimation();
            API.shared.triggerClientEvent(player, "animation_text");
        }
    }
}