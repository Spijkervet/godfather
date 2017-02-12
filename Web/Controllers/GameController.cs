using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheGodfatherGM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using TheGodfatherGM.Web.Models.GameViewModel;

namespace TheGodfatherGM.Web.Controllers
{
    [Authorize]
    public class GameController : Controller
    {

        private class JSONData
        {
            public string UserName;
            public string SocialClub;
            public string CharacterName;
            public string Token;
        }

        public IActionResult CreateCharacter()
        {
            return View();
        }

        public IActionResult Voice()
        {
            return View();
        }

        [HttpPost]
        public async Task CreateCharacter(CharacterViewModel model, string socialClub)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = DefaultDbContext.Instance.Users.FirstOrDefault(x => x.Id == userId);

            
            var jsonData = new JSONData();
            jsonData.UserName = user?.UserName;
            jsonData.SocialClub = socialClub;
            jsonData.CharacterName = model.Name;

            // Send POST to GameServer's listening port:
            using (var client = new HttpClient())
            {

                string json = JsonConvert.SerializeObject(jsonData);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:3001/CreateCharacter", content);
                var responseString = await response.Content.ReadAsStringAsync();
            }
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            // await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null, string socialclub = null, string token = null)
        {

            return View();
        }

        public async Task SendUserLogin(string socialclub, string token)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = DefaultDbContext.Instance.Users.FirstOrDefault(x => x.Id == userId);

            var jsonData = new JSONData();
            jsonData.UserName = user?.UserName;
            jsonData.SocialClub = socialclub;
            jsonData.Token = token;

            using (var client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(jsonData);
                var content = new StringContent(json.ToString(), Encoding.UTF8, "application/json");
                var response = await client.PostAsync("http://localhost:3001/LoginAccount", content);

                var responseString = await response.Content.ReadAsStringAsync();
            }
        }
    }
}
