using GTANetworkServer;

namespace TheGodfatherGM.Server.Global
{
    class GlobalVars : Script
    {
        public static string ListeningServer;
        public static int ListeningPort;
        public static string ListeningString;

        public static string WebServer;
        public static int WebServerPort;
        public static string WebServerConnectionString;

        public static string WebRTCServer;
        public static int WebRTCServerPort;
        public static string WebRTCServerConnectionString;



        public static string ServerName = "The Godfather";
		public static PedHash DefaultPedModel = PedHash.DrFriedlander;
    }
}
