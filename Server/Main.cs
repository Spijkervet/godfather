using GTANetworkServer;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace TheGodfatherGM.Server
{
    class Main : Script
    {
        public Main()
        {
            API.onResourceStart += OnResourceStart;
            API.onResourceStop += OnResourceStop;
        }

        private void OnResourceStart()
        {
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.GetCultureInfo("en-GB");
            Console.BackgroundColor = ConsoleColor.Blue;
            API.consoleOutput(Global.GlobalVars.ServerName + " was started at " + DateTime.Now);
            Console.ResetColor();

        }

        private void OnResourceStop()
        {
            API.consoleOutput("Resetting active sessions...");
            Task DBTerminate = Task.Run(() =>
            {
                DatabaseManager.ResetSessions();
            });
            DBTerminate.Wait();
            API.consoleOutput(Global.GlobalVars.ServerName + " was stopped at " + DateTime.Now);        
        }
    }
}