using System;
using GTANetworkServer;
using System.Threading.Tasks;
using System.Linq;

namespace TheGodfatherGM.Server.Global
{
    public static class Util
    {
        private static void Delay(int ms, Action action)
        {
            new Task(() => {
                API.shared.sleep(ms);
                action();
            }).Start();
        }

        public static string GenerateToken()
        {
            byte[] time = BitConverter.GetBytes(DateTime.UtcNow.ToBinary());
            byte[] key = Guid.NewGuid().ToByteArray();
            string token = Convert.ToBase64String(time.Concat(key).ToArray());
            return token;
        }

        public static void delay(int ms, Action action) { Delay(ms, action); }
    }
}