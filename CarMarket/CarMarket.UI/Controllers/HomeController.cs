using CarMarket.Repos;
using CarMarket.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;

namespace CarMarket.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BrandRepository brandsRepository;
        private readonly ModelRepository modelsRepository;
        public HomeController(ILogger<HomeController> logger, BrandRepository brandRepository, ModelRepository modelRepository)
        {
            _logger = logger;
            brandsRepository = brandRepository;
            modelsRepository = modelRepository;
        }
       

        public IActionResult Index()
        {
            
            var brands = brandsRepository.GetBrands();
            var brandsSelectList = new List<SelectListItem>();
            foreach (var brand in brands)
            {
                brandsSelectList.Add(new SelectListItem
                {
                    Text = brand.Name,
                    Value = brand.Name
                });
            }
            var models = modelsRepository.GetModels();
            var modelsSelectList = new List<SelectListItem>();
            foreach (var model in models)
            {               
                modelsSelectList.Add(new SelectListItem
                {
                    Text = model.Name,
                    Value = model.Name
                });
            }
            ViewBag.Brands = brandsSelectList;
            ViewBag.Models = modelsSelectList;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}