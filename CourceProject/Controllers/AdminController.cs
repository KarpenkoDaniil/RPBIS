using CourceProject.Models;
using DBModels;
using DBModels.Migrations;
using Filtration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CourceProject.Controllers
{
    [Authorize(Roles = "SuperUser")]
    public class AdminController : Controller
    {
        MaterialsSuplyContext context;
        UserManager<User> userManager;
        SignInManager<User> signInManager;
        RoleManager<IdentityRole> role;
        IMemoryCache memoryCache; 

        public AdminController(MaterialsSuplyContext materialsSuplyContext, UserManager<User> userManager, 
            SignInManager<User> signInManager, RoleManager<IdentityRole> role, IMemoryCache memoryCache)
        {
            this.context = materialsSuplyContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.role = role;
            this.memoryCache = memoryCache;
        }
        private void ChangeCash(List<User> users)
        {
            if (!memoryCache.TryGetValue("CashUsers", out var cachedData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Время хранения в кэше
                };

                memoryCache.Set("CashUsers", users, cacheEntryOptions);
            }
        }

        public IActionResult AdministrationRoot(string sumbitButton)
        {
            memoryCache.TryGetValue("CashUsers", out List<User> result);
            if (sumbitButton == "menu_button_0")
            {
                return View("AdminView", HttpContext.Session.Get<List<User>>("Users") ?? result);
            }
            else if (sumbitButton == "menu_button_1")
            {
                return View("AdminPageRedactProfilesOfUser", HttpContext.Session.Get<List<User>>("Users") ?? result);
            }
            else if (sumbitButton == "menu_button_2")
            {
                return AdministrationPageBanedUsersGet();
            }
            else
            {
                return View("AdminPageCreateRoles");
            }
        }

        public IActionResult AdminPageRead(string Id, string FirstName, string LastName, string Login, string PhoneNumber, string Email, string[] SelectRoles, string TypePage)
        {
            List<string> list = new List<string>
            {
                Id, FirstName, LastName, Login, PhoneNumber, Email   
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            var users =  filtre.UserFilter(userManager, list, SelectRoles).Result.ToList();
            var someUsers = PaginationSize(users, ViewBag.PageSize, ViewBag.CurentPage);
            HttpContext.Session.Set<List<User>>("CashUsers", users);

            if (TypePage == "UpdatePage")
            {
                return View("AdminPageRedactProfilesOfUser", someUsers);
            }
            else if (TypePage == "AdminPageBanOrUnban")
            {
                return View("AdminPageBanOrUnban", someUsers);
            }
            else if (TypePage == "AdminCreateRoles")
            {
                return View("AdminCreateRoles");
            }
            else
            {
                return View("AdminView", someUsers);
            }
        }

        [HttpGet]
        public IActionResult AdministrationPage()
        {
            ChangeCash(context.Users.ToList());
            return View("AdminView");
        }

        [HttpGet]
        public IActionResult AdministrationPageBanedUsersGet(int CurentPage = 1,  int PageSize = 30)
        {
            memoryCache.TryGetValue("CashUsers", out List<User> users);
            var someUsers = PaginationSize(users, ViewBag.PageSize = PageSize, ViewBag.CurentPage = CurentPage);
            return View("AdminPageBanOrUnban", someUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AdministrationPageBanedUsersPost(string ID, string ButtonBan)
        {
            var user = await userManager.FindByIdAsync(ID);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(ButtonBan))
                {
                    if (ButtonBan == "B")
                    {
                        var lockoutEndDate = DateTimeOffset.UtcNow.AddMinutes(5); // Блокировка пользователя на один день
                        await userManager.SetLockoutEndDateAsync(user, lockoutEndDate);
                        ChangeCash(context.Users.ToList());
                    }
                    else
                    {
                        await userManager.SetLockoutEndDateAsync(user, null);
                        ChangeCash(context.Users.ToList());
                    }
                }
            }
            memoryCache.TryGetValue("CashUsers", out List<User> users);
            var someUsers = PaginationSize(users, ViewBag.PageSize, ViewBag.CurentPage);
            return View("AdminPageBanOrUnban", someUsers);
        }

        [HttpGet]
        public async Task<IActionResult> AdministrationCreateRole(string NameOfRole)
        {
            if (! await role.RoleExistsAsync(NameOfRole))
            {
                var newRole = new IdentityRole(NameOfRole);
                var result = role.CreateAsync(newRole);
                if (result.Result.Succeeded)
                {
                    return View("AdminPageCreateRoles");
                }
                else
                {
                    ModelState.AddModelError("", "Не удалось создать роль. Пожалуйста, проверьте данные и попробуйте снова.");
                    return View("AdminPageCreateRoles");
                }
            }
            ModelState.AddModelError("", "Данная роль уже создан, повторно данную роль добавить невозможно");
            return View("AdminPageCreateRoles");
        }

        [HttpPost]
        public async Task<IActionResult> AdministrationPageRedactUsersProfiles(string Id, string FirstName, string LastName, string Login, string PhoneNumber, string Email, string[] SelectRoles,int PageSize = 30, int CurentPage = 1)
        {
            var user = userManager.Users.First(x => x.Id == Id);
            if (user != null)
            {
                if (!string.IsNullOrEmpty(FirstName))
                {
                    user.FirstName = FirstName;
                }

                if (!string.IsNullOrEmpty(LastName))
                {
                    user.LastName = LastName;
                }

                if (!string.IsNullOrEmpty(Login))
                {
                    user.UserName = Login;
                }

                if (!string.IsNullOrEmpty(PhoneNumber))
                {
                    user.PhoneNumber = PhoneNumber;
                }

                if (!string.IsNullOrEmpty(Email))
                {
                    user.Email = Email;
                }
                 
                if (SelectRoles.Length != 0)
                {
                    foreach (var role in SelectRoles)
                    {
                        var isInRole = await userManager.IsInRoleAsync(user, role);

                        if (!isInRole)
                        {
                            var result = await userManager.AddToRoleAsync(user, role);
                        }
                    }
                }
            }
            var someUsers = PaginationSize(context.Users.ToList(), ViewBag.PageSize = PageSize, ViewBag.CurentPage = CurentPage);
            return View("AdminPageRedactProfilesOfUser", someUsers);
        }

        private List<User> PaginationSize(List<User> someUsers, int pageSize = 30, int curentPage = 1)
        {
            var totalCount = someUsers.Count(); // Получаем общее количество пользователей
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // Вычисляем общее количество страниц

            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            var usersOnPage = someUsers
                .Skip((curentPage - 1) * pageSize)
                .Take(pageSize).ToList();

            return usersOnPage.ToList();
        }
    }
}
