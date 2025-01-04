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
            var plants = await _plantRepository.GetAllOfFacility(facilityId);
            return View(plants);
        }

        // GET: Plant/Details/5
        public async Task<IActionResult> Details(int id, int facilityId)
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
        public IActionResult Create()
        {
            PopulatePlantTypeOptions();
            return View();
        }

        // POST: Plant/Create
        [HttpPost]
        public async Task<IActionResult> Creat(PlantViewModel plantViewModel, int facilityId)
        {
            if (!ModelState.IsValid)
            {
                return View(plantViewModel);
            }

            var newPlant = new PlantType
            {
                Name = plantViewModel.Name,
                Type = plantViewModel.type,
                Price = plantViewModel.Price,
                Height = plantViewModel.Height,
            };

            _plantRepository.Add(newPlant);
            await _context.SaveChangesAsync(); // Lưu cây mới vào cơ sở dữ liệu

            // Thêm mối quan hệ vào bảng trung gian
            var plantFacilityRelation = new PlantTypePlantFacility
            {
                PlantTypeID = newPlant.Id, // ID của cây mới vừa thêm
                PlantFacilityID = facilityId
            };

            _context.plantPlantFacilities.Add(plantFacilityRelation);
            await _context.SaveChangesAsync(); // Lưu mối quan hệ vào cơ sở dữ liệu

            return RedirectToAction(nameof(Index), new { facilityId = facilityId });
        }


        // GET: Plant/Edit/5
        public async Task<IActionResult> Edit(int id, int facilityId)
        {
            var plant = await _plantRepository.GetIdByAsyncOfFacility(id, facilityId);
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
            return View(plant);
        }

        // POST: Plant/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PlantViewModel plantVM, int facilityId)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit!");
                return View(plantVM);
            }
            PopulatePlantTypeOptions(); 
            // Lấy thông tin cây hiện tại từ repository
            var existingPlant = await _plantRepository.GetIdByAsyncNoTrackingOfFacility(id, facilityId);
            if (existingPlant == null)
            {
                return View("Error"); // Nếu không tìm thấy cây, trả về trang lỗi
            }

            // Cập nhật thông tin cây từ ViewModel
            existingPlant.Name = plantVM.Name;
            existingPlant.Type = plantVM.type;
            existingPlant.Price = plantVM.Price;
            existingPlant.Height = plantVM.Height;

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
                IEnumerable<PlantType> plants = await _plantRepository.GetAllOfFacility(facilityId);
                return View("Index", plants);
            }
            IEnumerable<PlantType> searchResults = await _plantRepository.GetPlantByNameOfFacility(name, facilityId);
            return View("Index", searchResults);
        }
        // GET: Plant/Delete/5
        public async Task<IActionResult> Delete(int id, int facilityId)
        {
            var plant = await _plantRepository.GetIdByAsyncOfFacility(id, facilityId);
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
            if (plant != null)
            {
                _plantRepository.Delete(plant);
                var ppf = await _context.plantPlantFacilities
                    .FirstOrDefaultAsync(ppf => ppf.PlantTypeID == id && ppf.PlantFacilityID == facilityId); 
                if (ppf != null)
                {
                    _context.plantPlantFacilities.Remove(ppf);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index), new { facilityId = facilityId }); // Chuyển hướng về danh sách cây
        }
    }
}
