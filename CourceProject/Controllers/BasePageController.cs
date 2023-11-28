using CourceProject.Models;
using DBModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CourceProject.Controllers
{
    [Authorize]
    public class BasePageController : Controller
    {
        MaterialsSuplyContext materialsSuplyContext;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private RoleManager<IdentityRole> _role;

        public BasePageController(MaterialsSuplyContext materialsSuplyContext, UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> role)
        {
            this.materialsSuplyContext = materialsSuplyContext;
            this._userManager = userManager; 
            this._signInManager = signInManager;
            this._role = role;
        }

        private async void CreateRolesForUsers(bool create)
        {
            if(create)
            {
                var user = await _userManager.FindByNameAsync("CopyKeedr");
                if (user != null)
                {
                    var isInRole = await _userManager.IsInRoleAsync(user, "SuperUser");
                    var roleExists = await _role.RoleExistsAsync("SuperUser");
                    if (!roleExists)
                    {
                        var role = new IdentityRole("SuperUser");
                        var result = await _role.CreateAsync(role);
                        role = new IdentityRole("User");
                        result = await _role.CreateAsync(role);

                    }
                    else if (roleExists && !isInRole)
                    {
                        await _userManager.AddToRoleAsync(user, "SuperUser");
                    }
                }
            }
        }

        public async Task<IActionResult> BasePageView()
        {
            var r = materialsSuplyContext.ActualyDeliveryViewTables.Take(10).ToList();
            CreateRolesForUsers(false);
            return View("BasePage");
        }

        [HttpPost]
        public IActionResult RedirectToAdminPage()
        {
            return RedirectToAction("AdministrationPage", "Admin");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("GetLoger", "Loger");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("GetLoger", "Loger");
        }

        [HttpGet]
        public async Task<IActionResult> RootManager(string someTable)
        {
            switch (someTable)
            {
                case "materialTable":
                    return RedirectToAction("MaterialGetPage", "MaterialTable");
                case "employeTable":
                    return RedirectToAction("EmployeGetPage", "EmployeTable");
                case "deliveredResorcesTable":
                    return RedirectToAction("DeliveredResourceGetPage", "DeliveredResourceTable");
                case "requiredResourcesTable":
                    return RedirectToAction("RequiredResourceGetPage", "RequiredResourceTable");
                case "supplyContractsTable":
                    return RedirectToAction("SuplyContractGetPage", "SuplyContractTable");
            }
            return View("BasePage");
        }
    }
}
