using GTANetworkServer;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.Extensions
{
    public static class ClientExtensions
    {
        public static AccountController GetAccountController(this Client client)
        {
            return client.getData("ACCOUNT") as AccountController;
        }
    }
}
