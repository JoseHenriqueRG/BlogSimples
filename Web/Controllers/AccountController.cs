using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Web.Models;
using Web.Models.Identities;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Falha ao realizar o login.");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid) 
            {
                User user = new()
                {
                    Name = model.Name,
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password!);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach(var error in result.Errors)
                {
                    var translatedMessage = TranslateErrorToPortuguese(error.Description);
                    ModelState.AddModelError("", translatedMessage); ;
                }

            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private string TranslateErrorToPortuguese(string errorDescription)
        {
            return errorDescription switch
            {
                "Passwords must be at least 6 characters." => "As senhas devem ter pelo menos 6 caracteres.",
                "Password too short." => "A senha é muito curta.",
                "Passwords must have at least one non alphanumeric character." => "A senha deve ter pelo menos um caractere não alfanumérico.",
                "Passwords must have at least one digit ('0'-'9')." => "A senha deve ter pelo menos um número.",
                "Passwords must have at least one uppercase ('A'-'Z')." => "A senha deve ter pelo menos uma letra maiúscula.",
                "Passwords must have at least one lowercase ('a'-'z')." => "A senha deve ter pelo menos uma letra minúscula.",
                "Email is already taken." => "O e-mail já está em uso.",
                "User name is already taken." => "O nome de usuário já está em uso.",
                _ => "Ocorreu um erro inesperado."
            };
        }
    }
}
