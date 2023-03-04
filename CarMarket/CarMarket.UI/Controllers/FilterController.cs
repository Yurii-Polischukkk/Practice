using CarMarket.Core;
using CarMarket.Repos;
using CarMarket.Repos.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace CarMarket.UI.Controllers
{
    public class FilterController : Controller
    {
        private readonly CarsRepository carsRepository;
        private readonly BrandRepository brandsRepository;
        private readonly ModelRepository modelsRepository;
        public FilterController(CarsRepository carsRepository, BrandRepository brandRepository, ModelRepository modelRepository)
        {
            this.carsRepository = carsRepository;
            brandsRepository = brandRepository;
            modelsRepository = modelRepository;
        }
        public IActionResult Index()
        {
            return View(carsRepository.GetCars());
        }
        public async Task<IActionResult> Filter(string brands, string models, string color, string? engine, double? minPrice, double? maxPrice)
        {
            var cars = carsRepository.GetCars();
            if (brands != null)
            {
                var brand = brandsRepository.GetBrandByName(brands);
                if (brand == null)
                {
                    brand = new Brand() { Name = brands };
                    brand = await brandsRepository.AddBrandAsync(brand);
                }
                cars = cars.Where(x => x.Brand.Name == brand.Name).ToList();
            }

            if (models != null)
            {
                var model = modelsRepository.GetModelByName(models);
                if (model == null)
                {
                    model = new Model() { Name = models };
                    model = await modelsRepository.AddModelAsync(model);
                }
                cars = cars.Where(x => x.Model.Name == model.Name).ToList();
            }
           
            if (color != null)
            {
                cars = cars.Where(x => x.Color == color).ToList();
            }
            if (engine != null)
            {
                cars = cars.Where(x => x.Engine == engine).ToList();
            }
            if (minPrice != null)
            {
                cars = cars.Where(x => x.Price >= minPrice).ToList();
            }
            if (maxPrice != null)
            {
                cars = cars.Where(x => x.Price <= maxPrice).ToList();
            }
            return View(cars);
        }
        
    }
}
