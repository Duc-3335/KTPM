using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section1;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Models.ViewModels;
using quan_ly_tai_nguyen_rung.ViewModels;

namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class PlantController : Controller
    {
        private readonly IPlantRepository _plantRepository;
        private readonly ApplicationDbContext _context;
        public PlantController(IPlantRepository plantRepository, ApplicationDbContext context)
        {
            _plantRepository = plantRepository;
            _context = context;
        }

        // GET: Plant
        public async Task<IActionResult> Index(int facilityId)
        {
            if (facilityId <= 0)
            {
                return BadRequest("Facility ID không hợp lệ.");
            }

            var plants = await _plantRepository.GetAllOfFacility(facilityId);
            ViewBag.FacilityId = facilityId; // Lưu FacilityId để sử dụng trong View
            return View(plants);
        }

        // GET: Plant/Details/5
        [Route("Plant/Detail/{id}/{facilityId}")]
        public async Task<IActionResult> Detail(int id, int facilityId)
        {
            var plant = await _plantRepository.GetIdByAsyncNoTrackingOfFacility(id, facilityId);
            if (plant == null)
            {
                // Nếu không tìm thấy cây, hiển thị thông báo lỗi
                TempData["ErrorMessage"] = "Cây không tồn tại hoặc không thuộc cơ sở này.";
                return RedirectToAction(nameof(Index), new { facilityId = facilityId });
            }
            return View(plant);
        }

        private void PopulatePlantTypeOptions()
        {
            var plantTypeOptions = Enum.GetValues(typeof(DATA.@enum.type_plant))
                .Cast<DATA.@enum.type_plant>()
                .Select(t => new SelectListItem
                {
                    Value = t.ToString(),
                    Text = t.ToString() // Bạn có thể chuyển đổi để có định dạng dễ đọc hơn nếu cần
                }).ToList();

            ViewBag.PlantTypeOptions = plantTypeOptions;
        }

        // GET: Plant/Create
        // GET: Plant/Create
        public IActionResult Create(int facilityId)
        {
            if (facilityId <= 0)
            {
                return BadRequest("Facility ID không hợp lệ.");
            }

            ViewBag.FacilityId = facilityId; // Lưu facilityId để sử dụng trong View
            PopulatePlantTypeOptions();
            return View();
        }

        // POST: Plant/Create
        [HttpPost]
        public async Task<IActionResult> Create(PlantViewModel plantViewModel, int facilityId)
        {
            PopulatePlantTypeOptions();
            if (!ModelState.IsValid)
            {
                ViewBag.FacilityId = facilityId; // Đảm bảo facilityId có mặt trong View khi render lại
                return View(plantViewModel);
            }

            var newPlant = new Plant
            {
                Name = plantViewModel.Name,
                Type = plantViewModel.type,
                Price = plantViewModel.Price,
                Height = plantViewModel.Height,
                PlantFacilityId = facilityId,
            };

            _plantRepository.Add(newPlant);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Cây đã được thêm thành công!";
            return RedirectToAction(nameof(Index), new { facilityId });
        }


        // GET: Plant/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id, int facilityId)
        {
            var plant = await _plantRepository.GetIdByAsyncOfFacility(id, facilityId);
            ViewBag.FacilityId = facilityId;
            if (plant == null)
            {
                return NotFound();
            }
            var plantVM = new PlantViewModel
            {
                Name = plant.Name,
                type = plant.Type,
                Price = plant.Price,
                Height = plant.Height,
            };
            PopulatePlantTypeOptions(); // Đảm bảo danh sách loại cây có sẵn
            return View(plantVM);
        }

        // POST: Plant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlantViewModel plantVM, int facilityId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit!");
                ViewBag.FacilityId = facilityId;
                return View(plantVM);
            }
            PopulatePlantTypeOptions(); 
            // Lấy thông tin cây hiện tại từ repository
            var existingPlant = await _plantRepository.GetIdByAsyncOfFacility(id, facilityId);
            if (existingPlant == null)
            {
                return View("Error"); // Nếu không tìm thấy cây, trả về trang lỗi
            }

            // Cập nhật thông tin cây từ ViewModel
            existingPlant.Name = plantVM.Name;
            existingPlant.Type = plantVM.type;
            existingPlant.Price = plantVM.Price;
            existingPlant.Height = plantVM.Height;
            existingPlant.PlantFacilityId = facilityId;
            // Cập nhật cây trong repository
            _plantRepository.Update(existingPlant);

            // Chuyển hướng về danh sách cây
            return RedirectToAction(nameof(Index), new { facilityId = facilityId });
        }

        public async Task<IActionResult> SearchByName(string name,int facilityId)
        {

            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                // Nếu không có tên được nhập, trả về danh sách đầy đủ
                IEnumerable<Plant> plants = await _plantRepository.GetAllOfFacility(facilityId);
                return View("Index", plants);
            }
            IEnumerable<Plant> searchResults = await _plantRepository.GetPlantByNameOfFacility(name, facilityId);
            return View("Index", searchResults);
        }
        // GET: Plant/Delete/5
        public async Task<IActionResult> Delete(int id, int facilityId)
        {
            var plant = await _plantRepository.GetIdByAsyncOfFacility(id, facilityId);
            ViewBag.FacilityId = facilityId;
            if (plant == null)
            {
                return View("Error");
            }
            return View(plant);
        }

        // POST: Plant/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePlant(int id, int facilityId)
        {
            var plant = await _plantRepository.GetIdByAsyncOfFacility(id, facilityId);
            ViewBag.FacilityId = facilityId;

            if (plant != null)
            {
                _plantRepository.Delete(plant);
                TempData["SuccessMessage"] = "Cây đã được xóa thành công!";
            }
            else
            {
                TempData["ErrorMessage"] = "Cây không tồn tại hoặc không thuộc cơ sở này.";
            }
            return RedirectToAction(nameof(Index), new { facilityId = facilityId }); // Chuyển hướng về danh sách cây
        }

    }
}
