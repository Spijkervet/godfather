using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using GTANetworkServer;
using Newtonsoft.Json.Linq;
using TheGodfatherGM.Server.User;

namespace TheGodfatherGM.Server.http
{
    public class HttpController : Script
    {
        public HttpController()
        {
            API.onResourceStart += OnResourceStart;
        }

        private void OnResourceStart()
        {
            Global.GlobalVars.ListeningServer = API.getSetting<string>("auth_listening_server");
            Global.GlobalVars.ListeningPort = API.getSetting<int>("auth_listening_port");
            Global.GlobalVars.ListeningString = Global.GlobalVars.ListeningServer + ":" + Global.GlobalVars.ListeningPort + "/";

            Global.GlobalVars.WebServer = API.getSetting<string>("web_server");
            Global.GlobalVars.WebServerPort = API.getSetting<int>("web_server_port");
            Global.GlobalVars.WebServerConnectionString = Global.GlobalVars.WebServer + ":" + Global.GlobalVars.WebServerPort + "/";

            Global.GlobalVars.WebRTCServer = API.getSetting<string>("web_rtc_server");
            Global.GlobalVars.WebRTCServerPort = API.getSetting<int>("web_rtc_port");
            Global.GlobalVars.WebRTCServerConnectionString = Global.GlobalVars.WebRTCServer + ":" + Global.GlobalVars.WebRTCServerPort + "/Game/Voice";

            // new Thread(() => HttpListenerThread("http://" + API.getSetting<string>("nodejs_server") + ":" + API.getSetting<int>("nodejs_port") + "/"));
            HttpListenerThread(Global.GlobalVars.ListeningString);
        }

        public static void HttpListenerThread(string server)
        {
            HttpListener listener = new HttpListener();

            listener.Prefixes.Add(server);
            listener.Start();

            while (true)
            {
                HttpListenerContext ctx = listener.GetContext();

                var request = ctx.Request;
                string post;
                using (var reader = new StreamReader(request.InputStream, request.ContentEncoding))
                {
                    post = reader.ReadToEnd();
                }

                API.shared.consoleOutput("POST RECEIVED: " + post);

                Account.Listener(server, ctx.Request.Url.ToString(), post);
                Property.Listener(server, ctx.Request.Url.ToString(), post);
                Vehicle.Listener(server, ctx.Request.Url.ToString(), post);

                byte[] buf = Encoding.UTF8.GetBytes("");
                ctx.Response.ContentEncoding = Encoding.UTF8;
                ctx.Response.ContentType = "text/html";
                ctx.Response.ContentLength64 = buf.Length;

                API.shared.sendChatMessageToAll("~g~URL", ctx.Request.Url.ToString());
                ctx.Response.OutputStream.Write(buf, 0, buf.Length);
                ctx.Response.Close();
            }
        }

        public static void ProcessRequest(string args)
        {
            API.shared.sendChatMessageToAll("~g~Post:", args); // Debug.
            try
            {
                var Args = JObject.Parse(args);

                if ((string)Args.SelectToken("session_id") == "") return;
                if ((string)Args.SelectToken("socialclub_id") == "") return;
                if ((string)Args.SelectToken("command") == "") return;

                if (!AccountController.IsSessionIDValid((string)Args.SelectToken("socialclub_id"), (string)Args.SelectToken("session_id"))) return;

                Client sender = AccountController.GetClientFromSocialClub((string)Args.SelectToken("socialclub_id"));

                if (sender == null) return;

                switch ((string)Args.SelectToken("command"))
                {
                    case "CEF_CLOSE":
                        API.shared.triggerClientEvent(sender, "CEF_CLOSE", (string)Args.SelectToken("args"));
                        return;
                    case "ADMIN_EVAL":
                        string code = (string)Args.SelectToken("args.code");
                        string[] targets = ((string)Args.SelectToken("args.targets")).Split(',');

                        if (targets.Contains("global"))
                        {
                            API.shared.triggerClientEventForAll("ADMIN_EVAL", code);
                        }
                        else
                        {
                            foreach (string target in targets)
                            {
                                if (target == "local")
                                {
                                    API.shared.triggerClientEvent(sender, "ADMIN_EVAL", code);
                                }

                                Client p_target = AccountController.GetClientFromSocialClub(target);
                                if (p_target != null)
                                {
                                    if (p_target != sender || !targets.Contains("local"))
                                    {
                                        API.shared.triggerClientEvent(p_target, "ADMIN_EVAL", code);
                                    }
                                }
                            }
                        }
                        return;
                    /*
                     * case "ADMIN_CLOTHES":
                        string type = (string)args.SelectToken("args.type");
                        string index = (string)args.SelectToken("args.index");

                        int index_c = int.Parse(index);
                        int index_s = 0;

                        if ((string)args.SelectToken("args.index_s") != "")
                        {
                            index_s = int.Parse((string)args.SelectToken("args.index_s"));
                        }

                        API.setPlayerClothes(sender, ClothingParts[type], index_c, index_s);
                        return;
                        */
                    case "REGISTER":

                        /*
                         * if (player_isRegistered(sender))
                        {
                            API.shared.sendChatMessageToAll("~r~Cancel:", "6");
                            return;
                        }

                        string firstname = (string)args.SelectToken("args.vorname");
                        string lastname = (string)args.SelectToken("args.nachname");
                        bool gender = (bool)args.SelectToken("args.gender");

                        player_register(sender, firstname, lastname, gender);
                        */
                        return;
                    case "PLAYER_DISCONNECT":
                        API.shared.kickPlayer(sender, (string)Args.SelectToken("args"));
                        return;
                }

            }
            catch (Exception e)
            {
                API.shared.sendChatMessageToAll("~r~ERROR:", e.ToString());
                return;
            }
        }

        public static string VerifySessionData(string post_raw)
        {
            try
            {
                var args = JObject.Parse(post_raw);

                if ((string)args.SelectToken("session_id") == "") return "0";
                if ((string)args.SelectToken("socialclub_id") == "") return "0";

                if (AccountController.IsSessionIDValid((string)args.SelectToken("socialclub_id"), (string)args.SelectToken("session_id")))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            catch //(Exception e)
            {
                return "0";
            }
        }
    }
}
