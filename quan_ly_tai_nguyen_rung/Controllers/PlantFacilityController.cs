using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Repository;
using quan_ly_tai_nguyen_rung.ViewModels;

namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class PlantFacilityController : Controller
    {
        private readonly IPlantFacilityRepository _plantFacilityRepository;
        private readonly IPlantRepository _plantRepository;
        private readonly ApplicationDbContext _context;
        public PlantFacilityController(IPlantFacilityRepository plantFacilityRepository, IPlantRepository plantRepository, ApplicationDbContext context)
        {
            _plantFacilityRepository = plantFacilityRepository;
            _plantRepository = plantRepository;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var facilities = await _plantFacilityRepository.GetAll();
            return View(facilities);
        }
        public async Task<IActionResult> Detail(int id)
        {
            var facility = await _plantFacilityRepository.GetIdByAsync(id);
            if (facility == null)
            {
                ViewData["ErrorMessage"] = "ID không hợp lệ. Vui lòng nhập lại.";
                return View("Detail"); // Trả về lại cùng View Detail.
            }
            return View (facility);
        }
        private async Task PopulateCommunes()
        {
            var communes = await _context.Communes.ToListAsync();
            ViewBag.Communes = new SelectList(communes, "Id", "Name");
        }

        public async Task<IActionResult> Create()
        {
            await PopulateCommunes(); // Gọi phương thức lấy danh sách xã
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PlantFacilityViewModel facilityVM)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCommunes(); // Gọi lại để đảm bảo danh sách xã có sẵn khi có lỗi
                return View(facilityVM);
            }

            var facility = new PlantFacility
            {
                Name = facilityVM.Name,
                Status = facilityVM.Status,
                Address = facilityVM.Address,
                ContactFace = facilityVM.ContactFace,
                ContactMail = facilityVM.ContactMail,
                ContactPhone = facilityVM.ContactPhone,
                Acreage = facilityVM.Acreage,
                SeedlingsYield = facilityVM.SeedlingsYield,
                Labor = facilityVM.Labor,
                ImagePlantBreedingFacility = facilityVM.ImagePlantBreedingFacility,
                CommuneId = facilityVM.CommuneId,
            };

            _plantFacilityRepository.Add(facility);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var facility = await _plantFacilityRepository.GetIdByAsync(id);
            if (facility == null) return View("Error");
            await PopulateCommunes();
            var facilityVM = new PlantFacilityViewModel
            {
                Name = facility.Name,
                Status = facility.Status,
                Address = facility.Address,
                ContactFace = facility.ContactFace,
                ContactMail = facility.ContactMail,
                ContactPhone = facility.ContactPhone,
                Labor = facility.Labor,
                Acreage = facility.Acreage,
                SeedlingsYield = facility.SeedlingsYield,
                ImagePlantBreedingFacility = facility.ImagePlantBreedingFacility,
                CommuneId = facility.CommuneId
            };
            return View(facilityVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, PlantFacilityViewModel facilityVM)
        {
            await PopulateCommunes();
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit !");
                return View(facilityVM);
            }
            var facility = await _plantFacilityRepository.GetByIdAsyncNoTracking(id);
            if (facility == null)
            {
                return View("Error");
            }
            facility.Name = facilityVM.Name;
            facility.Address = facilityVM.Address;
            facility.Status = facilityVM.Status;
            facility.ContactFace = facilityVM.ContactFace;
            facility.ContactMail = facilityVM.ContactMail;
            facility.ContactPhone = facilityVM.ContactPhone;
            facility.Labor = facilityVM.Labor;
            facility.Acreage = facilityVM.Acreage;
            facility.SeedlingsYield = facilityVM.SeedlingsYield;
            facility.ImagePlantBreedingFacility = facilityVM.ImagePlantBreedingFacility;
            facility.CommuneId = facilityVM.CommuneId;
            _plantFacilityRepository.Update(facility);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SearchByName(string name)
        {
            // Kiểm tra ModelState trước
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
                IEnumerable<PlantFacility> facilities = await _plantFacilityRepository.GetAll();
                return View("Index", facilities); // Trả về danh sách tất cả nếu dữ liệu không hợp lệ
            }

            // Kiểm tra nếu tên rỗng hoặc toàn khoảng trắng
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                IEnumerable<PlantFacility> facilities = await _plantFacilityRepository.GetAll();
                return View("Index", facilities);
            }

            // Tìm kiếm theo tên
            IEnumerable<PlantFacility> searchResults = await _plantFacilityRepository.GetFacilityByName(name);
            return View("Index", searchResults);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var facilityDetail = await _plantFacilityRepository.GetIdByAsync(id);
            if (facilityDetail == null) return View("Errol");
            return View(facilityDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _plantFacilityRepository.GetIdByAsync(id);
            if (facility == null)
            {
                return View("Error"); 
            }

            // Xóa mối quan hệ trong bảng trung gian
            var relatedRecords = _context.plantPlantFacilities
                                          .Where(pf => pf.PlantFacilityID == id)
                                          .ToList();
            if (relatedRecords.Any())
            {
                _context.plantPlantFacilities.RemoveRange(relatedRecords);
                await _context.SaveChangesAsync(); // Lưu thay đổi
            }
            _plantFacilityRepository.Delete(facility);
            return RedirectToAction("Index");
        }

    }
}
