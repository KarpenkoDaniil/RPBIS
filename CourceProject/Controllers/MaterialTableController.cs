using DBModels;
using Filtration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CourceProject.Controllers
{
    [Authorize]
    public class MaterialTableController : Controller
    {
        MaterialsSuplyContext context;
        //Передача через конструктор контекста
        public MaterialTableController(MaterialsSuplyContext context)
        {
            this.context = context;
        }

        //Маршрутизация страниц
        [HttpGet]
        public IActionResult MaterialPageRoot(string sumbitButton)
        {
            if (sumbitButton == "menu_button_0")//create
            {
                return View("MaterialPageCreate", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
            }
            else if (sumbitButton == "menu_button_1")//read
            {
                return View("MaterialPageRead", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
            }
            else if (sumbitButton == "menu_button_2")//update
            {
                return View("MaterialPageUpdate", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
            }
            else if (sumbitButton == "menu_button_3") //delete
            {
                return View("MaterialPageDelete", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
            }
            else
            {
                return RedirectToAction("BasePageView", "BasePage");
            }
        }

        //Инициализация страницы 
        [HttpGet]
        public IActionResult MaterialGetPage()
        {
            List<Material> materials = new List<Material>();
            return View("MaterialPageRead", materials);
        }

        //Метод для получения с страницы фильтрующих полей,
        //если поле пустое то данное фильтруещее поле не будет использоваться в фильтрации
        //Так же это метод отвечает и за отображения данных
        [HttpPost]
        [ResponseCache(Duration = 240 + 2 * 13, Location = ResponseCacheLocation.Any)]
        public IActionResult MaterialPostPage(int MaterialID, string MaterialType, string NameOfStateStandart, string StateStandart, string MeasureOfMeasurement, string TypePage)
        {
            //Создание списка с фильтрующими полями
            List<string> list = new List<string>
            {
                MaterialID.ToString(),
                MaterialType,
                NameOfStateStandart,
                StateStandart,
                MeasureOfMeasurement
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            List<Material> materials = new List<Material>();

            //Получение отфильтрованных полей
            materials = filtre.MaterialsFilter(context, list).ToList();
            HttpContext.Session.Set<List<Material>>("MaterialSesion", materials);

            if (TypePage == "CreatePage")
            {
                return View("MaterialPageCreate", materials);
            }
            else if (TypePage == "DeletePage")
            {
                return View("MaterialPageDelete", materials);
            }
            else if (TypePage == "ReadPage")
            {
                return View("MaterialPageRead", materials);
            }
            else if (TypePage == "UpdatePage")
            {
                return View("MaterialPageUpdate", materials);
            }

            return View("MaterialPageRead", materials);
        }

        //Метод для создания новых материалов
        [HttpPost]
        public IActionResult MaterialPageCreate(string MaterialType,
            string NameOfStateStandart, string StateStandart, string MeasureOfMeasurement, string Characteristics)
        {
            Material material = new Material();
            material.StateStandart = StateStandart;
            material.MeasureOfMeasurement = MeasureOfMeasurement;
            material.MaterialType = MaterialType;
            material.NameOfStateStandart = NameOfStateStandart;
            material.Characteristics = Characteristics;

            context.Materials.Add(material);

            context.SaveChanges();

            return View("MaterialPageCreate", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
        }

        [HttpPost]
        public IActionResult MaterialPageDelete(int MaterialID, string MaterialType, 
            string NameOfStateStandart, string StateStandart, string MeasureOfMeasurement)
        {
            List<string> list = new List<string>
            {
                MaterialID.ToString(),
                MaterialType,
                NameOfStateStandart,
                StateStandart,
                MeasureOfMeasurement
            };

            FiltreRequestForDb filtre = new FiltreRequestForDb();
            List<Material> materials = new List<Material>();
            materials = filtre.MaterialsFilter(context, list).ToList();

            if(materials.Count != 0)
            {
                context.Materials.RemoveRange(materials);
                context.SaveChanges();  
            }

            return View("MaterialPageDelete", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
        }

        [HttpPost]
        public IActionResult MaterialPageUpdate(int MaterialID, string MaterialType,
            string NameOfStateStandart, string StateStandart, string MeasureOfMeasurement, string Characteristics)
        {
            var material = context.Materials.Find(MaterialID);
            if(material != null)
            {
                material.MaterialType = MaterialType;
                material.NameOfStateStandart= NameOfStateStandart;
                material.StateStandart= StateStandart;
                material.MeasureOfMeasurement= MeasureOfMeasurement;
                material.Characteristics= Characteristics;

                context.Materials.Update(material);
                context.SaveChanges();

                var str = HttpContext.Session.Get<List<Material>>("MaterialSesion");
            }
            return View("MaterialPageDelete", HttpContext.Session.Get<List<Material>>("MaterialSesion") ?? new List<Material>());
        }
    }
}
