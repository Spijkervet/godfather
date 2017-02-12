using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheGodfatherGM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace TheGodfatherGM.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        public SignInManager<Account> _signInManager;
        private UserManager<Account> _userManager;
        private DefaultDbContext _dbContext;

        private Task<Account> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);




        public AccountController(
            UserManager<Account> userManager,
            SignInManager<Account> signInManager,
            DefaultDbContext dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var model = new IndexViewModel
            {

            };

            return View();
        }



        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            // Clear the existing external cookie to ensure a clean login process
            // await HttpContext.Authentication.SignOutAsync(_externalCookieScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult Login(Models.AccountViewModel.LoginViewModel obj)
        {
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync
                (obj.EmailAddress, obj.Password,
                  obj.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login!");
            }
            return View(obj);
        }

        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login));
        }



        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(Models.AccountViewModel.RegisterViewModel model, string returnUrl = null)
        {
            Debug.Write("Hello");
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new Account { UserName = model.EmailAddress, Email = model.EmailAddress };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=532713
                    // Send an email with this link
                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Action(nameof(ConfirmEmail), "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                    //await _emailSender.SendEmailAsync(model.Email, "Confirm your account",
                    //    $"Please confirm your account by clicking this link: <a href='{callbackUrl}'>link</a>");
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    //_logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToLocal(returnUrl);
                }
                // AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }



        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult GameLogin(Models.GameViewModel.LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var result = _signInManager.PasswordSignInAsync
                (model.EmailAddress, model.Password,
                  model.RememberMe, false).Result;

                if (result.Succeeded)
                { 
                    return RedirectToAction("SendUserLogin", "Game", new { socialclub = model.SocialClub, token = model.Token });
                }
                else RedirectToAction("Login", "Game"); //View(model);
                // ModelState.AddModelError(string.Empty, "Invalid login!");
            }
            return BadRequest(); 
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GameRegister(Models.GameViewModel.RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            // if (ModelState.IsValid) -> Doesn't work yet.
            {
                var user = new Account { UserName = model.EmailAddress, Email = model.EmailAddress, SocialClub = model.SocialClub };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("SendUserLogin", "Game", new { socialclub = model.SocialClub, token = model.Token });
                }
                else return RedirectToAction("Register", "Game"); //View(model);
            }
            // return BadRequest();
        }

        public IActionResult Error()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
    }
}
