using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section1;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.ViewModels;
//hello
namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class AnimalController : Controller
    {
        private readonly IAnimalRepository _animalRepository;
        public AnimalController(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Animal> dv = await _animalRepository.GetAll();
            return View(dv);
        }
        public async Task<IActionResult> Detail(int id) 
        {
            Animal dv = await _animalRepository.GetIdByAsync(id);
            return View(dv);
        }
        public async Task<IActionResult> Create() {
            //var facilities = await _animalFacility.GetAll();
            //ViewBag.AnimalFacilities = new SelectList(facilities, "Id", "Name"); // Truyền 
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AnimalViewModel animal)
        {
            if (!ModelState.IsValid)
            {
                
                return View(animal);
            }
            //var facilities = await _animalFacility.GetAll();
            //ViewBag.AnimalFacilities = new SelectList(facilities, "Id", "Name"); // Truyền

            var _animal = new Animal
            {
                Name = animal.Name,
                Generic = animal.Generic,
                DateFound = animal.DateFound,
                PreviousQuantity = animal.PreviousQuantity,
                Status = animal.Status,
                Fluctuation = animal.Fluctuation,
                Date = animal.Date,
                Reason = animal.Reason,
                Location = animal.Location,
                is_Active = animal.IsActive,
                CurrentQuantity = animal.CurrentQuantity,
            };
 
            _animalRepository.Add(_animal);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit (int id)
        {
            var animal = await _animalRepository.GetIdByAsync(id);
            if (animal == null) return View("Error");
            var animalVm = new AnimalViewModel
            {
                Name= animal.Name,
                Generic= animal.Generic,
                DateFound = animal.DateFound,
                PreviousQuantity= animal.PreviousQuantity,
                Status = animal.Status,
                Fluctuation= animal.Fluctuation,
                Date = animal.Date,
                Reason = animal.Reason,
                Location = animal.Location,
                IsActive = animal.is_Active,
                CurrentQuantity = animal.CurrentQuantity,

            };
            return View(animalVm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnimalViewModel animalVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit!");
                return View("Edit", animalVM);
            }
            var dv = _animalRepository.GetIdBtAsyncNoTracking(id);
            if(dv == null) return View("Error");
            var animal = new Animal
            {
                Id = id,
                Name = animalVM.Name,
                Generic = animalVM.Generic,
                DateFound = animalVM.DateFound,
                PreviousQuantity = animalVM.PreviousQuantity,
                Status = animalVM.Status,
                Fluctuation = animalVM.Fluctuation,
                Date = animalVM.Date,
                Reason = animalVM.Reason,
                Location = animalVM.Location,
                is_Active = animalVM.IsActive,
                CurrentQuantity = animalVM.CurrentQuantity,
            };
            _animalRepository.Update(animal);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SearchByName(string name)
        {

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                // Nếu không có tên được nhập, trả về danh sách đầy đủ
                IEnumerable<Animal> animals = await _animalRepository.GetAll();
                return View("Index", animals);
            }
            IEnumerable<Animal> searchResults = await _animalRepository.GetAnimalByName(name);
            return View("Index", searchResults);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var animalDetail = await _animalRepository.GetIdByAsync(id);
            if (animalDetail == null) return View("Error");
            return View(animalDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAnimal(int id)
        {
            var animalDetail = await _animalRepository.GetIdByAsync(id);
            if (animalDetail == null) return View("Error");
            _animalRepository.Delete(animalDetail);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult GetAnimalStatistics()
        {
            // Lấy dữ liệu và nhóm theo tháng/năm
            var data = _animalRepository.GetAll()
                .Result
                .GroupBy(a => new { Year = a.Date.Year, Month = a.Date.Month })
                .Select(g => new
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    TotalQuantity = g.Sum(a => a.CurrentQuantity)
                })
                .OrderBy(g => g.Year)
                .ThenBy(g => g.Month)
                .ToList();

            // Chuyển đổi dữ liệu thành JSON
            var jsonData = JsonConvert.SerializeObject(data);
            return Content(jsonData, "application/json");
        }
        public IActionResult Statistics()
        {
            return View();
        }


    }
}
