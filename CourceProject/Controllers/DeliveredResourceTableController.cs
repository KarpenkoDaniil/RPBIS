using CourceProject.Models;
using DBModels;
using Filtration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CourceProject.Controllers
{
    [Authorize]
    public class DeliveredResourceTableController : Controller
    {
        MaterialsSuplyContext context;
        IMemoryCache cache;

        public DeliveredResourceTableController(MaterialsSuplyContext context, IMemoryCache cache)
        {
            this.context = context;
            this.cache = cache;
            if (!cache.TryGetValue("CashDelivereResources", out List<DeliveredResource> deliveredResources))
            {
                ChangeCash(context.DeliveredResources.ToList());
            }
        }

        [HttpGet]
        public IActionResult DeliveredResourceGetPage()
        {
            cache.TryGetValue("CashDelivereResources", out List<DeliveredResource> delivered);
            var someObjects = PaginationSize(HttpContext.Session.Get<List<DeliveredResource>>("DeliveredResourceSesion") ?? delivered);
            return View("DeliveredResourcePageRead", someObjects);
        }

        [HttpGet]
        public IActionResult DeliveredResourcePageRoot(string sumbitButton, int CurentPage = 1)
        {
            cache.TryGetValue("CashDelivereResources", out List<DeliveredResource> delivered);
            delivered = PaginationSize(HttpContext.Session.Get<List<DeliveredResource>>("DeliveredResourceSesion") ?? delivered, 30, CurentPage);
            if (sumbitButton == "menu_button_0")//create
            {
                return View("DeliveredResourcePageCreate", delivered);
            }
            else if (sumbitButton == "menu_button_1")//read
            {
                return View("DeliveredResourcePageRead", delivered);
            }
            else if (sumbitButton == "menu_button_2")//update
            {
                return View("DeliveredResourcePageUpdate", delivered);
            }
            else if (sumbitButton == "menu_button_3") //delete
            {
                return View("DeliveredResourcePageDelete", delivered);
            }
            else
            {
                return RedirectToAction("BasePageView", "BasePage");
            }
        }

        [HttpPost]
        public IActionResult DeliveredResourcePageRead(int DeliveredResourcesId, int YearOfDelivery, int QuarterOfDelivery, 
            double SizeOfResourseUsed, int SupplyContractsId, string TypePage)
        {
            List<DeliveredResource> deliveredResource = new List<DeliveredResource>();

            //Создание списка с фильтрующими полями
            List<string> list = new List<string>
            {
                DeliveredResourcesId.ToString(),
                YearOfDelivery.ToString(),
                QuarterOfDelivery.ToString(),
                SizeOfResourseUsed.ToString(),
                SupplyContractsId.ToString()
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();

            //Получение отфильтрованных полей
            deliveredResource = filtre.DeliveredResourceFilter(context, list).ToList();
            HttpContext.Session.Set<List<DeliveredResource>>("DeliveredResourceSesion", deliveredResource);
            deliveredResource = PaginationSize(deliveredResource, 30, ViewBag.CurentPage ?? 1);

            if (TypePage == "CreatePage")
            {
                return View("DeliveredResourcePageCreate", deliveredResource);
            }
            else if (TypePage == "DeletePage")
            {
                return View("DeliveredResourcePageDelete", deliveredResource);
            }
            else if (TypePage == "ReadPage")
            {
                return View("DeliveredResourcePageRead", deliveredResource);
            }
            else if (TypePage == "UpdatePage")
            {
                return View("DeliveredResourcePageUpdate", deliveredResource);
            }

            return View("DeliveredResourcePageRead", list);
        }

        //Метод для создания новых материалов
        [HttpPost]
        public IActionResult DeliveredResourceCreate(int YearOfDelivery, int QuarterOfDelivery,
            double SizeOfResourseUsed, int SupplyContractsId)
        {
            var deliveredResource = new DeliveredResource();
            deliveredResource.YearOfDelivery = YearOfDelivery;
            deliveredResource.QuarterOfDelivery = QuarterOfDelivery;
            deliveredResource.SizeOfResourseUsed = SizeOfResourseUsed;
            deliveredResource.SupplyContractsId= SupplyContractsId;

            context.DeliveredResources.Update(deliveredResource);
            context.SaveChanges();
            ChangeCash(context.DeliveredResources.ToList());
            cache.TryGetValue("CashDelivereResources", out List<DeliveredResource> deliveredResources);
            var delivered = PaginationSize(HttpContext.Session.Get<List<DeliveredResource>>("DeliveredResourceSesion") ?? deliveredResources);

            return View("DeliveredResourcePageCreate", delivered);
        }

        [HttpPost]
        public IActionResult DeliveredResourcePageDelete(int DeliveredResourcesId, int YearOfDelivery, int QuarterOfDelivery,
            double SizeOfResourseUsed, int SupplyContractsId, string TypePage)
        {
            List<string> list = new List<string>
            {
                DeliveredResourcesId.ToString(),
                YearOfDelivery.ToString(),
                QuarterOfDelivery.ToString(),
                SizeOfResourseUsed.ToString(),
                SupplyContractsId.ToString()
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            List<DeliveredResource> deliveredResources = new List<DeliveredResource>();
            deliveredResources = filtre.DeliveredResourceFilter(context, list).ToList();

            if (deliveredResources.Count != 0)
            {
                context.DeliveredResources.RemoveRange(deliveredResources);
                context.SaveChanges();
            }

            ChangeCash(context.DeliveredResources.ToList());
            cache.TryGetValue("CashDelivereResources", out List<DeliveredResource> delivered);
            var deliver = PaginationSize(HttpContext.Session.Get<List<DeliveredResource>>("DeliveredResourceSesion") ?? delivered);

            return View("DeliveredResourcePageDelete", deliver);
        }

        [HttpPost]
        public IActionResult DeliveredResourcePageUpdate(int DeliveredResourcesId, int YearOfDelivery, int QuarterOfDelivery,
            double SizeOfResourseUsed, int SupplyContractsId)
        {
            var deliveredResource = context.DeliveredResources.Find(DeliveredResourcesId);
            if (deliveredResource != null)
            {
                deliveredResource.DeliveredResourcesId = DeliveredResourcesId;
                deliveredResource.YearOfDelivery = YearOfDelivery;
                deliveredResource.QuarterOfDelivery = QuarterOfDelivery;
                deliveredResource.SizeOfResourseUsed = SizeOfResourseUsed;
                deliveredResource.SupplyContractsId = SupplyContractsId;

                context.DeliveredResources.Update(deliveredResource);
                context.SaveChanges();

                ChangeCash(context.DeliveredResources.ToList());
                cache.TryGetValue("CashDelivereResources", out List<DeliveredResource> delivered);
                var deliver = PaginationSize(HttpContext.Session.Get<List<DeliveredResource>>("DeliveredResourceSesion") ?? delivered, 30 , ViewBag.CurentPage ?? 1);

                return View("DeliveredResourcePageUpdate", deliver);
            }
            return View("DeliveredResourcePageUpdate", HttpContext.Session.Get<List<DeliveredResource>>("DeliveredResourceSesion") ?? new List<DeliveredResource>());
        }

        private void ChangeCash(List<DeliveredResource> deliveredResources)
        {
            if (!cache.TryGetValue("CashDelivereResources", out var cachedData))
            {
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10) // Время хранения в кэше
                };

                cache.Set("CashDelivereResources", deliveredResources, cacheEntryOptions);
            }
        }

        private List<DeliveredResource> PaginationSize(List<DeliveredResource> someObjects, int pageSize = 30, int curentPage = 1)
        {
            var totalCount = someObjects.Count(); // Получаем общее количество пользователей
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize); // Вычисляем общее количество страниц

            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;

            var objects = someObjects
                .Skip((curentPage - 1) * pageSize)
                .Take(pageSize).ToList();

            return objects.ToList();
        }
    }
}
