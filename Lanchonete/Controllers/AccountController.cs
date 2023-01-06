using Lanchonete.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;

        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm)
        {
            if (!ModelState.IsValid)
                return View(loginVm);

            var user = await _userManager.FindByNameAsync(loginVm.UserName);

            if (user != null)
            {
                var res = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);

                if (res.Succeeded)
                {
                    if (string.IsNullOrEmpty(loginVm.ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(loginVm.ReturnUrl);
                }
            }
            ModelState.AddModelError("", "Falha ao realizar login");
            return View(loginVm);
        }

        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(LoginViewModel registroVM)
        {
            if (ModelState.IsValid)
            {
                var usr = new IdentityUser { UserName = registroVM.UserName };
                var res = await _userManager.CreateAsync(usr, registroVM.Password);

                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(usr, "Member");
                    return RedirectToAction("Login", "Account");
                }
                else
                    this.ModelState.AddModelError("Registro", "Falhar ao registrar usuário");
            }

            return View(registroVM);
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.User = null;
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");

        }

        public IActionResult AccessDenied()
        {
            return View();  
        }


    }

}