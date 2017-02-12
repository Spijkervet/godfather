using GTANetworkServer;
using Newtonsoft.Json.Linq;
using System.Linq;
using TheGodfatherGM.Server.Characters;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Server.Extensions;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Data.Dictionaries;

namespace TheGodfatherGM.Server.http
{
    public class Account
    {
        public static void Listener(string server, string url, string post)
        {
            if (url == server + "LoginAccount")
            {
                var Args = JObject.Parse(post);
                var UserName = (string)Args.SelectToken("UserName");
                var SocialClub = (string)Args.SelectToken("SocialClub");

                Client player = AccountController.GetClientFromSocialClub(SocialClub);

                if (AccountController.IsAccountBanned(player))
                {
                    player.kick("~r~This account is banned.");
                }
                else
                {
                    var accountData = ContextFactory.Instance.Account.Where(x => x.UserName == UserName).FirstOrDefault();
                    new AccountController(accountData, player);
                }
                Global.CEFController.Close(player);
            }

            else if (url == server + "CreateCharacter")
            {
                var Args = JObject.Parse(post);
                var UserName = Args.SelectToken("UserName").ToString();
                var SocialClub = Args.SelectToken("SocialClub").ToString();
                var CharacterName = Args.SelectToken("CharacterName").ToString();

                Client player = AccountController.GetClientFromSocialClub(SocialClub);
                // Check null!
                Commands.CreateCharacter(player, CharacterName);
                Global.CEFController.Close(player);
            }

            else if (url == server + "PlayAnimation")
            {
                var args = JObject.Parse(post);
                var socialClub = args.SelectToken("SocialClub").ToString();
                var animation = args.SelectToken("AnimationName").ToString();

                Client player = AccountController.GetClientFromSocialClub(socialClub);
                AccountController accountController = player.GetAccountController();
                if (accountController == null) return;

                else if (!Animations.AnimationList.ContainsKey(animation))
                {
                    API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~Animation not found!");
                    return;
                }

                var flag = 0;
                accountController.CharacterController.PlayAnimation(player, Animations.AnimationList[animation].Split()[0], Animations.AnimationList[animation].Split()[1], flag);
            }

            else if (url == server + "PlayScenario")
            {
                var args = JObject.Parse(post);
                var socialClub = args.SelectToken("SocialClub").ToString();
                var scenario = args.SelectToken("ScenarioName").ToString();

                Client player = AccountController.GetClientFromSocialClub(socialClub);
                AccountController accountController = player.GetAccountController();
                if (accountController == null) return;

                else if (!Animations.ScenarioList.ContainsKey(scenario))
                {
                    API.shared.sendChatMessageToPlayer(player, "~r~ERROR: ~w~Scenario not found!");
                    return;
                }

                accountController.CharacterController.PlayScenario(player, Animations.ScenarioList[scenario]);
            }
        }
    }
}
