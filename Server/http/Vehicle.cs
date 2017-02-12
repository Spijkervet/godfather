using GTANetworkServer;
using Newtonsoft.Json.Linq;
using TheGodfatherGM.Server.User;
using TheGodfatherGM.Server.Vehicles;

namespace TheGodfatherGM.Server.http
{
    public class Vehicle
    {
        public static void Listener(string server, string url, string post)
        {
            if (url == server + "VehicleStorage")
            {
                var Args = JObject.Parse(post);
                var UserName = (string)Args.SelectToken("UserName");
                var SocialClub = (string)Args.SelectToken("SocialClub");

                Client player = AccountController.GetClientFromSocialClub(SocialClub);

                VehicleController.VehicleStorage(player);
                Global.CEFController.Close(player);
            }
        }
    }
}
