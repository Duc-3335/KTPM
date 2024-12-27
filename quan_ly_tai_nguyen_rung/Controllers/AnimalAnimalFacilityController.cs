using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Migrations;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.Repository;
using quan_ly_tai_nguyen_rung.ViewModels;

namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class AnimalAnimalFacilityController : Controller
    {
        private readonly IAnimalAnimalFacilityRepository _animalAnimalFacilityRepository;
        private readonly IAnimalFacilityRepository animalFacility;
        private readonly IAnimalRepository _animalRepository;
        public AnimalAnimalFacilityController(IAnimalAnimalFacilityRepository animalAnimalFacilityRepository, IAnimalRepository animalRepository, IAnimalFacilityRepository animalFacilityRepository)
        {
            _animalAnimalFacilityRepository = animalAnimalFacilityRepository;
            animalFacility = animalFacilityRepository;
            _animalRepository = animalRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<AnimalAnimalFacility> lk = await _animalAnimalFacilityRepository.GetAll();

            return View(lk);
        }
        //public async Task<IActionResult> Detail(int id)
        //{
        //    AnimalAnimalFacility lk = await _animalAnimalFacilityRepository.GetIdByAsync(id);
        //    return View(lk);
        //}
        public async Task<IActionResult> Create() {
            var animal = await _animalRepository.GetAll();
            ViewBag.Animals = new SelectList(animal, "Id", "Name");
            var facility = await animalFacility.GetAll();
            ViewBag.AnimalFacilities = new SelectList(facility, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(AnimalAnimalFacilityViewModel animalAnimalFacilityVM)
        {
            var animal = await _animalRepository.GetAll();
            ViewBag.Animals = new SelectList(animal, "Id", "Name");
            var facility = await animalFacility.GetAll();
            ViewBag.AnimalFacilities = new SelectList(facility, "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View(animalAnimalFacilityVM);
            }
                

            
            var animalAnimalFacility = new AnimalAnimalFacility
            {
                AnimalId = animalAnimalFacilityVM.AnimalId,
                AnimalFacilityId = animalAnimalFacilityVM.AnimalFacilityId
            };
            _animalAnimalFacilityRepository.Add(animalAnimalFacility);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            var aaf = await _animalAnimalFacilityRepository.GetIdByAsync(id);
            if (aaf == null) return View("Error");
            return View(aaf);
        }
        [HttpPost, ActionName("Delete")] 
        public async Task<IActionResult> DeleteA(int id)
        {
            var aaf = await _animalAnimalFacilityRepository.GetIdByAsync(id);
            if (aaf == null) return View("error");
            _animalAnimalFacilityRepository.Delete(aaf);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                IEnumerable<AnimalAnimalFacility> aaf = await _animalAnimalFacilityRepository.GetAll();
                return View("Index", aaf);
            }
            IEnumerable<AnimalAnimalFacility> searchResults = await _animalAnimalFacilityRepository.GetAnimalOrFacility(name);
            return View("Index", searchResults);
        }
    }
}

