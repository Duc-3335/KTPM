using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.ViewModel;
using quan_ly_tai_nguyen_rung.ViewModel.section4;

namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class FluctuationController : Controller
    {
        private readonly IFluctuationRepository _fluctuationRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ApplicationDbContext _context;

        public FluctuationController(
            IFluctuationRepository fluctuationRepository,
            IAnimalRepository animalRepository,
            ApplicationDbContext context)
        {
            _fluctuationRepository = fluctuationRepository;
            _animalRepository = animalRepository;
            _context = context;
        }

        // GET: Index
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.FacilityId = id;
           
            var fluctuation = await _fluctuationRepository.GetFluctuationByFacilityAsync(id);
            return View(fluctuation);
        }

        // Populate animals for a dropdown
        private async Task PopulateAnimals(int facilityId)
        {
            var animals = await _animalRepository.GetAllOfFacility(facilityId);
            ViewBag.Animal = new SelectList(animals, "Id", "Name");
        }

        // GET: Create
        public async Task<IActionResult> Create(int facilityId)
        {
            ViewBag.FacilityId = facilityId;
            await PopulateAnimals(facilityId);
            
            return View();
        }

        // POST: Create
        [HttpPost]
        public async Task<IActionResult> Create(FluctuationViewModel model, int facilityId)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAnimals(facilityId);
                ViewBag.FacilityId = facilityId;
                return View(model);
            }

            var fluctuation = new Fluctuation
            {
                Type = model.Type,
                Year = model.Year,
                Month = model.Month,
                Quarter = model.Quarter,
                AnimalId = model.AnimalId,
                QuantityChange = model.QuantityChange,
                Reason = model.Reason
            };

            _fluctuationRepository.Add(fluctuation);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Fluctuation đã cập nhật thành công!";
            return RedirectToAction(nameof(Index), new { id = facilityId });
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id, int facilityId)
        {
            var fluctuation = await _fluctuationRepository.GetIdByAsync(id);
            ViewBag.FacilityId = facilityId;
            if (fluctuation == null)
            {
                return NotFound("Fluctuation not found.");
            }

            await PopulateAnimals(facilityId);

            var model = new FluctuationViewModel
            {
                Id = fluctuation.Id,
                Year = fluctuation.Year,
                Month = fluctuation.Month,
                Type = fluctuation.Type,
                Quarter = fluctuation.Quarter,
                AnimalId = fluctuation.AnimalId,
                QuantityChange = fluctuation.QuantityChange,
                Reason = fluctuation.Reason
            };

            return View(model);
        }

        // POST: Edit
        [HttpPost]
        public async Task<IActionResult> Edit(int id, FluctuationViewModel model, int facilityId)
        {
            if (!ModelState.IsValid)
            {
                await PopulateAnimals(facilityId);
                ViewBag.FacilityId = facilityId;
                return View(model);
            }

            var fluctuation = await _fluctuationRepository.GetIdByAsync(id);
            if (fluctuation == null)
            {
                return NotFound("Fluctuation not found.");
            }

            fluctuation.Year = model.Year;
            fluctuation.Month = model.Month;
            fluctuation.Quarter = model.Quarter;
            fluctuation.Type = model.Type;
            fluctuation.AnimalId = model.AnimalId;
            fluctuation.QuantityChange = model.QuantityChange;
            fluctuation.Reason = model.Reason;

            _fluctuationRepository.Update(fluctuation);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Fluctuation data đã cập nahajt thành công!";
            return RedirectToAction(nameof(Index), new { id = facilityId });
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id, int facilityId)
        {
            var fluctuation = await _fluctuationRepository.GetIdByAsync(id);
            ViewBag.FacilityId = facilityId;
            if (fluctuation == null)
            {
                return NotFound("Fluctuation not found.");
            }

            return View(fluctuation);
        }

        // POST: Delete
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id, int facilityId)
        {
            var fluctuation = await _fluctuationRepository.GetIdByAsync(id);
            ViewBag.FacilityId = facilityId;
            if (fluctuation != null)
            {
                _fluctuationRepository.Delete(fluctuation);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Fluctuation data đã xóa thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Fluctuation đã xóa thành công.";
            }

            return RedirectToAction(nameof(Index), new { id = facilityId });
        }
    }
}
