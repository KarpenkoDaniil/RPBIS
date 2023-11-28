using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DBModels;
using CourceProject.Models;
using Microsoft.AspNetCore.Identity;

namespace CourceProject.Controllers
{
    public class LogerController : Controller
    {
        MaterialsSuplyContext materialsSuplyContext;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LogerController(MaterialsSuplyContext materialsSuply , UserManager<User> userManager, SignInManager<User> signInManager)
        {
            materialsSuplyContext = materialsSuply;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult GetLoger()
        {
            return View("LogerView");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistrationResult(RegistrationViewModel model)
        {
            if(ModelState.IsValid)
            {
                User user = new User{ UserName = model.Login, FirstName = model.FirstName, LastName = model.LastName, UserOrSuperUser = "N"};

                var result = await _userManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded) 
                {
                    await _signInManager.SignInAsync(user, false);
                    await _userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("BasePageView", "BasePage");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View("Register");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResultLoger(LogerViewModel logerViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(logerViewModel.Login, logerViewModel.Password, true, false);

                if (result.Succeeded)
                {
                    // Вход выполнен успешно, выполните необходимые действия, например, перенаправление пользователя
                    return RedirectToAction("BasePageView", "BasePage");
                }
                else
                {
                    // Обработка ошибок входа
                    ModelState.AddModelError(string.Empty, "Ошибка входа.");
                }
            }

            return View("LogerView");
        }
    }
}
