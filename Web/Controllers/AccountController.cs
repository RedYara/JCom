using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Web.Models.AccountDtoModels;
using Microsoft.Win32.SafeHandles;
using Web.Application.Interfaces;
using System.Security.Claims;
using Web.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Web.Controllers
{
    [Authorize]
    public class AccountController(UserManager<User> userMng, SignInManager<User> signInMng, IWebHostEnvironment hostEnvironment, IDbContext dbContext) : BaseController
    {
        private readonly UserManager<User> _userManager = userMng;
        private readonly SignInManager<User> _signInManager = signInMng;
        private readonly IWebHostEnvironment _hostEnvironment = hostEnvironment;
        private readonly IDbContext _dbContext = dbContext;

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (User.Identity.IsAuthenticated) return Redirect("/home/index");
            return View(new LoginModelDto { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModelDto loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await _userManager.FindByNameAsync(loginModel.Name);
                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();
                        if ((await _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false)).Succeeded)
                        {
                            var claims = new List<Claim>
                            {
                                new(CustomClaimTypes.UserNameIdentifier, user.UserName),
                                new(CustomClaimTypes.UserIdIdentifier, user.Id.ToString()),
                                new(CustomClaimTypes.UserTagIdentifier, user.UserTag),
                                new(CustomClaimTypes.UserImagePathIdentifier, _dbContext.UserImages.FirstOrDefault(x => x.User == user).Path)
                            };

                            await _signInManager.SignInWithClaimsAsync(user, true, claims);

                            string ReturnUrl = loginModel.ReturnUrl;
                            ReturnUrl ??= "/home/index";
                            return Redirect(ReturnUrl);
                        }
                    }
                }
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e);
            }

            return View(loginModel);
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(RegisterModelDto registerData)
        {
            var entity = await _userManager.FindByNameAsync(registerData.Username);
            if (entity != null) return BadRequest("Пользователь с данным именем уже зарегистрирован");

            var user = new User
            {
                Email = registerData.Email,
                UserName = registerData.Username,
                SecurityStamp = "dummyStamp",
                UserTag = registerData.UserTag,
                Name = registerData.FirstName,
                Surname = registerData.LastName
            };

            string uploadPath = Path.Join(_hostEnvironment.WebRootPath, "UserAvatars", registerData.Logo.FileName);
            using FileStream ms = new(uploadPath, FileMode.Create);
            await registerData.Logo.CopyToAsync(ms);

            UserImage userImage = new()
            {
                Path = Path.Join("UserAvatars", registerData.Logo.FileName),
                User = user
            };

            user.UserImage = userImage;

            var result = await _userManager.CreateAsync(user, registerData.Password);
            if (result.Succeeded)
            {
                // Optionally save the UserImage to the database if needed
                await _dbContext.UserImages.AddAsync(userImage);
                await _dbContext.SaveChangesAsync(new CancellationToken());

                return Redirect("Login");
            }
            else
            {
                return BadRequest("Could not create user.");
            }
        }

        [AllowAnonymous]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectPermanent("Login");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckTag(string UserTag)
        {
            string formattedTag = UserTag.Trim();
            if (_dbContext.Users.Any(x => x.UserTag == formattedTag))
                return Json(false);
            return Json(true);
        }

        [AllowAnonymous]
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckUserName(string UserName)
        {
            string formattedUserName = UserName.Trim();
            if (_dbContext.Users.Any(x => x.UserName == formattedUserName))
                return Json(false);
            return Json(true);
        }
    }
}