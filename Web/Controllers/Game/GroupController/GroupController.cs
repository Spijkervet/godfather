using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using TheGodfatherGM.Web.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using TheGodfatherGM.Web.Models.GameViewModel.Group;
using TheGodfatherGM.Data.Enums;
using TheGodfatherGM.Data.MessagingModels;

namespace TheGodfatherGM.Web.Controllers.Game.Group
{
    [Authorize]
    public class GroupController : Controller
    {
        private class JSONData
        {
            public string UserName;
            public string SocialClub;
            public string Token;

            public string GroupName;
            public GroupType GroupType;
            public GroupExtraType GroupExtraType;
        }

        public IActionResult CreateGroup()
        {
            return View();
        }

        [HttpPost]
        public async Task CreateGroup(CreateGroupViewModel model)
        {
            var groupData = new Data.Group();
            groupData.Name = model.Name;
            groupData.Type = model.Type;
            groupData.ExtraType = model.ExtraType;
            DefaultDbContext.Instance.Group.Add(groupData);
            DefaultDbContext.Instance.SaveChanges();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = DefaultDbContext.Instance.Users.FirstOrDefault(x => x.Id == userId);


            var createGroupMessage = new CreateGroupMessage
            {
                UserName = user?.UserName,
                GroupName = model.Name,
                SocialClub = model.socialclub,
                GroupExtraType = model.ExtraType,
                GroupType = model.Type
            };

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(createGroupMessage), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:3001/CreateGroup", content);

                var responseString = await response.Content.ReadAsStringAsync();
            }
        }

    }
}