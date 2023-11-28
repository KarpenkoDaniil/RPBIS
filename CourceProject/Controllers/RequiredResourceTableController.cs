using DBModels;
using Filtration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourceProject.Controllers
{
    [Authorize]
    public class RequiredResourceTableController : Controller
    {
        MaterialsSuplyContext context;

        public RequiredResourceTableController(MaterialsSuplyContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult RequiredResourceGetPage()
        {
            return View("RequiredResourcePageRead", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
        }

        [HttpGet]
        public IActionResult RequiredResourcePageRoot(string sumbitButton)
        {
            if (sumbitButton == "menu_button_0")//create
            {
                return View("RequiredResourcePageCreate", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
            }
            else if (sumbitButton == "menu_button_1")//read
            {
                return View("RequiredResourcePageRead", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
            }
            else if (sumbitButton == "menu_button_2")//update
            {
                return View("RequiredResourcePageUpdate", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
            }
            else if (sumbitButton == "menu_button_3") //delete
            {
                return View("RequiredResourcePageDelete", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
            }
            else
            {
                return RedirectToAction("BasePageView", "BasePage");
            }
        }

        [HttpPost]
        public IActionResult RequiredResourcePageRead(int RequiredResourcesId, int MaterialId, int Year,
            int Quarter, int SizeOfResurces, string TypePage)
        {
            List<RequiredResource> requiredResource = new List<RequiredResource>();

            //Создание списка с фильтрующими полями
            List<string> list = new List<string>
            {
                RequiredResourcesId.ToString(),
                MaterialId.ToString(),
                Year.ToString(),
                Quarter.ToString(),
                SizeOfResurces.ToString()
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();

            //Получение отфильтрованных полей
            requiredResource = filtre.RequiredResourcesFilter(context, list).ToList();
            HttpContext.Session.Set<List<RequiredResource>>("RequiredResourceSesion", requiredResource);

            if (TypePage == "CreatePage")
            {
                return View("RequiredResourcePageCreate", requiredResource);
            }
            else if (TypePage == "DeletePage")
            {
                return View("RequiredResourcePageDelete", requiredResource);
            }
            else if (TypePage == "ReadPage")
            {
                return View("RequiredResourcePageRead", requiredResource);
            }
            else if (TypePage == "UpdatePage")
            {
                return View("RequiredResourcePageUpdate", requiredResource);
            }

            return View("RequiredResourcePageRead", list);
        }

        //Метод для создания новых материалов
        [HttpPost]
        public IActionResult RequiredResourcePageCreate(int MaterialId, int Year,
            int Quarter, int SizeOfResurces)
        {
            var requiredResource = new RequiredResource();
            requiredResource.MaterialId = MaterialId;
            requiredResource.Year = Year;
            requiredResource.Quarter = Quarter;
            requiredResource.SizeOfResurces = SizeOfResurces;

            context.RequiredResources.Update(requiredResource);
            context.SaveChanges();

            return View("RequiredResourcePageCreate", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
        }

        [HttpPost]
        public IActionResult RequiredResourcePageDelete(int RequiredResourcesId, int MaterialId, int Year,
            int Quarter, int SizeOfResurces)
        {
            List<string> list = new List<string>
            {
                RequiredResourcesId.ToString(),
                MaterialId.ToString(),
                Year.ToString(),
                Quarter.ToString(),
                SizeOfResurces.ToString()
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            List<RequiredResource> requiredResource = new List<RequiredResource>();
            requiredResource = filtre.RequiredResourcesFilter(context, list).ToList();

            if (requiredResource.Count != 0)
            {
                context.RequiredResources.RemoveRange(requiredResource);
                context.SaveChanges();
            }

            return View("RequiredResourcePageDelete", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
        }

        [HttpPost]
        public IActionResult RequiredResourcePageUpdate(int RequiredResourcesId, int MaterialId, int Year,
            int Quarter, int SizeOfResurces)
        {
            var requiredResources = context.RequiredResources.Find(RequiredResourcesId);
            if (requiredResources != null)
            {
                requiredResources.MaterialId = MaterialId;
                requiredResources.Year = Year;
                requiredResources.Quarter = Quarter;
                requiredResources.SizeOfResurces = SizeOfResurces;

                context.RequiredResources.Update(requiredResources);
                context.SaveChanges();
            }
            return View("RequiredResourcePageUpdate", HttpContext.Session.Get<List<RequiredResource>>("RequiredResourceSesion") ?? new List<RequiredResource>());
        }
    }
}
