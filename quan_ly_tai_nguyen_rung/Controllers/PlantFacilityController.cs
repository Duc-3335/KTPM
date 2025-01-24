using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PlantFacilityController(IPlantFacilityRepository plantFacilityRepository,
            IPlantRepository plantRepository,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _plantFacilityRepository = plantFacilityRepository;
            _plantRepository = plantRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                Labor = facilityVM.Labor,
                CommuneId = facilityVM.CommuneId,
                ImagePlantBreedingFacility = UploadFile(facilityVM),
            };

            _plantFacilityRepository.Add(facility);
            return RedirectToAction("Index");
        }
        private string UploadFile(PlantFacilityViewModel facilityVM)
        {
            string filename = null;
            if (facilityVM.ImagePlantBreedingFacility != null)
            {
                string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "-" + facilityVM.ImagePlantBreedingFacility.FileName;
                string filePath = Path.Combine(UploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    facilityVM.ImagePlantBreedingFacility.CopyTo(fileStream);
                }
            }
            return filename;
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
                CommuneId = facility.CommuneId,
                URL = facility.ImagePlantBreedingFacility
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
            if (facilityVM.RemoveImage && !string.IsNullOrEmpty(facility.ImagePlantBreedingFacility))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", facility.ImagePlantBreedingFacility);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath); // Xóa file ảnh vật lý
                }
                facility.ImagePlantBreedingFacility = null; // Xóa thông tin ảnh trong cơ sở dữ liệu
            }
            else if (facilityVM.ImagePlantBreedingFacility != null)
            {
                // Upload ảnh mới
                facility.ImagePlantBreedingFacility = UploadFile(facilityVM);
            }
            else
            {
                // Sử dụng ảnh cũ nếu không xóa và không tải ảnh mới
                facility.ImagePlantBreedingFacility = facilityVM.URL;
            }

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
            _plantFacilityRepository.Delete(facility);
            return RedirectToAction("Index");
        }
        
    }
}
