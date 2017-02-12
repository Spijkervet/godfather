using GTANetworkServer;
using GTANetworkShared;
using System.Linq;
using TheGodfatherGM.Server.DBManager;
using TheGodfatherGM.Data;

using TheGodfatherGM.Data.Extensions;
using TheGodfatherGM.Data.Attributes;

namespace TheGodfatherGM.Server.Jobs
{
    public class JobController : Script
    {

        public Data.Job JobData;
        public Groups.GroupController Group;

        Blip blip;
        Marker marker;
        ColShape colShape;
        TextLabel textLabel;

        public JobController() { }

        public JobController(Data.Job jobData)
        {
            JobData = jobData;
            EntityManager.Add(this);
        }

        public static void LoadJobs()
        {
            foreach (var job in ContextFactory.Instance.Job)
            {
                JobController jobController = new JobController(job);
                jobController.CreateWorldEntity();
            }
            API.shared.consoleOutput("[GM] Loaded jobs: " + ContextFactory.Instance.Job.Count());
        }

        public void CreateWorldEntity()
        {
            blip = API.createBlip(new Vector3(JobData.PosX, JobData.PosY, JobData.PosZ), 0, 0);
            API.setBlipSprite(blip, JobData.Type.GetAttributeOfType<BlipTypeAttribute>().BlipId);
            API.setBlipName(blip, (Group == null ? Type() : Group.Group.Name));

            marker = API.createMarker(1, new Vector3(JobData.PosX, JobData.PosY, JobData.PosZ) - new Vector3(0, 0, 1f), new Vector3(), new Vector3(),
               new Vector3(1f, 1f, 1f), 100, 255, 255, 255);

            textLabel = API.createTextLabel("~b~[Job (ID: " + JobData.Id + ")]" + (Group == null ? "" : "\n~w~Company: \n" +
                Group.Group.Name) + "\n~y~Job: " + Type() + "\n~w~Level: " + JobData.Level,
                new Vector3(JobData.PosX, JobData.PosY, JobData.PosZ) + new Vector3(0.0f, 0.0f, 0.5f), 15.0f, 0.5f);

            CreateColShape();
        }

        public void CreateColShape()
        {
            colShape = API.createCylinderColShape(new Vector3(JobData.PosX, JobData.PosY, JobData.PosZ), 2f, 3f);
            colShape.onEntityEnterColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.getPlayerFromHandle(entity)) != null)
                {
                    API.shared.sendNotificationToPlayer(player, Type() + ":\nUse /join to start.");
                    player.setData("AT_JOB", this);
                }
            };
            colShape.onEntityExitColShape += (shape, entity) =>
            {
                Client player;
                if ((player = API.getPlayerFromHandle(entity)) != null)
                {
                    player.resetData("AT_JOB");
                }
            };
        }


        public string Type()
        {
            return JobData.Type.GetDisplayName();
        }
    }
}
