using DBModels;
using Filtration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourceProject.Controllers
{
    [Authorize]
    public class SuplyContractTableController : Controller
    {
        MaterialsSuplyContext context;

        public SuplyContractTableController(MaterialsSuplyContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult SuplyContractGetPage()
        {
            return View("SuplyContractPageRead", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
        }

        [HttpGet]
        public IActionResult SuplyContractPageRoot(string sumbitButton)
        {
            if (sumbitButton == "menu_button_0")//create
            {
                return View("SuplyContractPageCreate", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
            }
            else if (sumbitButton == "menu_button_1")//read
            {
                return View("SuplyContractPageRead", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
            }
            else if (sumbitButton == "menu_button_2")//update
            {
                return View("SuplyContractPageUpdate", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
            }
            else if (sumbitButton == "menu_button_3") //delete
            {
                return View("SuplyContractPageDelete", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
            }
            else
            {
                return RedirectToAction("BasePageView", "BasePage");
            }
        }

        [HttpPost]
        public IActionResult SuplyContractPageRead(int SupplyContractsId, DateTime DateOfConclusion, DateTime DateOfDiliver, string Supplyer, 
            double DiliverySize, int MaterialId, int EmployeeID, string TypePage)
        {
            List<SupplyContract> supplyContract = new List<SupplyContract>();

            //Создание списка с фильтрующими полями
            List<string> list = new List<string>
            {
                SupplyContractsId.ToString(),
                DateOfConclusion.ToString(),
                DateOfDiliver.ToString(),
                Supplyer,
                DiliverySize.ToString(),
                MaterialId.ToString(),
                EmployeeID.ToString()

            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();

            //Получение отфильтрованных полей
            supplyContract = filtre.SupplyContractFilter(context, list).ToList();
            HttpContext.Session.Set<List<SupplyContract>>("SuplyContractSesion", supplyContract);

            if (TypePage == "CreatePage")
            {
                return View("SuplyContractPageCreate", supplyContract);
            }
            else if (TypePage == "DeletePage")
            {
                return View("SuplyContractPageDelete", supplyContract);
            }
            else if (TypePage == "ReadPage")
            {
                return View("SuplyContractPageRead", supplyContract);
            }
            else if (TypePage == "UpdatePage")
            {
                return View("SuplyContractPageUpdate", supplyContract);
            }

            return View("SuplyContractPageRead", list);
        }

        //Метод для создания новых материалов
        [HttpPost]
        public IActionResult SuplyContractPageCreate( DateTime DateOfConclusion, DateTime DateOfDiliver, string Supplyer,
            double DiliverySize, int MaterialId, int EmployeeID)
        {
            var supplyContract = new SupplyContract();
            supplyContract.DateOfConclusion = DateOfConclusion;
            supplyContract.DateOfDiliver = DateOfDiliver;
            supplyContract.Supplyer = Supplyer;
            supplyContract.DiliverySize = DiliverySize;
            supplyContract.MaterialId = MaterialId;
            supplyContract.EmployeeId = EmployeeID;

            context.SupplyContracts.Update(supplyContract);
            context.SaveChanges();

            return View("SuplyContractPageCreate", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
        }

        [HttpPost]
        public IActionResult SuplyContractPageDelete(int SupplyContractsId, DateTime DateOfConclusion, DateTime DateOfDiliver, string Supplyer,
            double DiliverySize, int MaterialId, int EmployeeID)
        {
            List<string> list = new List<string>
            {
                SupplyContractsId.ToString(),
                DateOfConclusion.ToString(),
                DateOfDiliver.ToString(),
                Supplyer,
                DiliverySize.ToString(),
                MaterialId.ToString(),
                EmployeeID.ToString()

            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            List<SupplyContract> suplyContract = new List<SupplyContract>();
            suplyContract = filtre.SupplyContractFilter(context, list).ToList();

            if (suplyContract.Count != 0)
            {
                context.SupplyContracts.RemoveRange(suplyContract);
                context.SaveChanges();
            }

            return View("SuplyContractPageDelete", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
        }

        [HttpPost]
        public IActionResult SuplyContractPageUpdate(int SupplyContractsId, DateTime DateOfConclusion, DateTime DateOfDiliver, string Supplyer,
            double DiliverySize, int MaterialId, int EmployeeID)
        {
            var supplyContract = context.SupplyContracts.Find(SupplyContractsId);
            if (supplyContract != null)
            {
                supplyContract.DateOfConclusion = DateOfConclusion;
                supplyContract.DateOfDiliver = DateOfDiliver;
                supplyContract.Supplyer = Supplyer;
                supplyContract.DiliverySize = DiliverySize;
                supplyContract.MaterialId = MaterialId;
                supplyContract.EmployeeId = EmployeeID;

                context.SupplyContracts.Update(supplyContract);
                context.SaveChanges();
            }
            return View("SuplyContractPageUpdate", HttpContext.Session.Get<List<SupplyContract>>("SuplyContractSesion") ?? new List<SupplyContract>());
        }
    }
}

