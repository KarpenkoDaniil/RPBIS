using DBModels;
using Filtration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace CourceProject.Controllers
{
    [Authorize]
    public class EmployeTableController : Controller
    {
        MaterialsSuplyContext context;

        public EmployeTableController(MaterialsSuplyContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult EmployeGetPage()
        {
            return View("EmployePageRead", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
        }

        [HttpGet]
        public IActionResult EmployePageRoot(string sumbitButton)
        {
            if (sumbitButton == "menu_button_0")//create
            {
                return View("EmployePageCreate", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
            }
            else if (sumbitButton == "menu_button_1")//read
            {
                return View("EmployePageRead", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
            }
            else if (sumbitButton == "menu_button_2")//update
            {
                return View("EmployePageUpdate", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
            }
            else if (sumbitButton == "menu_button_3") //delete
            {
                return View("EmployePageDelete", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
            }
            else
            {
                return RedirectToAction("BasePageView", "BasePage");
            }
        }

        [HttpPost]
        public IActionResult EmploePageRead(int EmployeeId, string Post, string FirstName, string LastName, string TypePage)
        {
            List<Employe> employes = new List<Employe>();

            //Создание списка с фильтрующими полями
            List<string> list = new List<string>
            {
                EmployeeId.ToString(),
                Post,
                FirstName,
                LastName
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();

            //Получение отфильтрованных полей
            employes = filtre.EmployesFilter(context, list).ToList();
            HttpContext.Session.Set<List<Employe>>("EmployeSesion", employes);

            if (TypePage == "CreatePage")
            {
                return View("EmployePageCreate", employes);
            }
            else if (TypePage == "DeletePage")
            {
                return View("EmployePageDelete", employes);
            }
            else if (TypePage == "ReadPage")
            {
                return View("EmployePageRead", employes);
            }
            else if (TypePage == "UpdatePage")
            {
                return View("EmployePageUpdate", employes);
            }

            return View("EmploePageRead", list);
        }

        //Метод для создания новых материалов
        [HttpPost]
        public IActionResult EmployePageCreate(string Post, string FirstName, string LastName)
        {
            var employe = new Employe();
            employe.Post = Post;
            employe.FirstName = FirstName;
            employe.LastName = LastName;

            context.Employes.Update(employe);
            context.SaveChanges();

            return View("EmployePageCreate", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
        }

        [HttpPost]
        public IActionResult EmployePageDelete(int EmployeeId, string Post, string FirstName, string LastName)
        {
            List<string> list = new List<string>
            {
                EmployeeId.ToString(),
                Post,
                FirstName,
                LastName
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            List<Employe> employes = new List<Employe>();
            employes = filtre.EmployesFilter(context, list).ToList();

            if (employes.Count != 0)
            {
                context.Employes.RemoveRange(employes);
                context.SaveChanges();
            }

            return View("EmployePageDelete", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
        }

        [HttpPost]
        public IActionResult EmployePageUpdate(int EmployeeId, string Post, string FirstName, string LastName)
        {
            var employe = context.Employes.Find(EmployeeId);
            if (employe != null)
            {
                employe.Post = Post;
                employe.FirstName = FirstName;
                employe.LastName = LastName;

                context.Employes.Update(employe);
                context.SaveChanges();

                var str = HttpContext.Session.Get<List<Material>>("EmployeSesion");
            }
            return View("EmployePageUpdate", HttpContext.Session.Get<List<Employe>>("EmployeSesion") ?? new List<Employe>());
        }
    }
}
