using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Domain;
using Web.Models.AccountDtoModels;
using Microsoft.Win32.SafeHandles;
using Web.Application.Interfaces;

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
            if (User.Identity.IsAuthenticated) return Redirect("/");
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
                            string ReturnUrl = loginModel.ReturnUrl;
                            ReturnUrl ??= "/";
                            return Redirect(ReturnUrl);
                        }
                    }
                }
                ModelState.AddModelError("", "Неверный логин или пароль");
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
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
    }
}