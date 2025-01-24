using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.Repository;
using quan_ly_tai_nguyen_rung.ViewModels;

namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class AnimalFacilityController : Controller
    {
        private readonly IAnimalFacilityRepository _animalFacilityRepository;
        private readonly IAnimalRepository _animalRepository;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnimalFacilityController(IAnimalFacilityRepository animalFacilityRepository,
            IAnimalRepository animalRepository,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _animalFacilityRepository = animalFacilityRepository;
            _animalRepository = animalRepository;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var facilities = await _animalFacilityRepository.GetAll();
            return View(facilities);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var facility = await _animalFacilityRepository.GetIdByAsync(id);
            if (facility == null)
            {
                ViewData["ErrorMessage"] = "ID không hợp lệ. Vui lòng nhập lại.";
                return View("Detail");
            }
            return View(facility);
        }
        private async Task PopulateCommunes()
        {
            var communes = await _context.Communes.ToListAsync();
            ViewBag.Communes = new SelectList(communes, "Id", "Name");
        }
        public async Task<IActionResult> Create()
        {
            await PopulateCommunes();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AnimalFacilityViewModel facilityVM)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCommunes();
                return View(facilityVM);
            }

            var facility = new AnimalFacility
            {
                Name = facilityVM.Name,
                Address = facilityVM.Address,
                ContactFace = facilityVM.ContactFace,
                ContactMail = facilityVM.ContactMail,
                ContactPhone = facilityVM.ContactPhone,
                Labor = facilityVM.Labor,
                Acreage = facilityVM.Acreage,
                CommuneId = facilityVM.CommuneId,
                ImageAnimalStorage = UploadFile(facilityVM)
            };

            _animalFacilityRepository.Add(facility);
            return RedirectToAction("Index");
        }
        private string UploadFile(AnimalFacilityViewModel facilityVM)
        {
            string filename = null;
            if (facilityVM.ImageAnimalStorage != null)
            {
                string UploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "Images");
                filename = Guid.NewGuid().ToString() + "-" + facilityVM.ImageAnimalStorage.FileName;
                string filePath = Path.Combine(UploadDir, filename);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    facilityVM.ImageAnimalStorage.CopyTo(fileStream);
                }
            }
            return filename;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var facility = await _animalFacilityRepository.GetIdByAsync(id);
            if (facility == null) return View("Error");
            await PopulateCommunes();

            var facilityVM = new AnimalFacilityViewModel
            {
                Name = facility.Name,
                Address = facility.Address,
                ContactFace = facility.ContactFace,
                ContactMail = facility.ContactMail,
                ContactPhone = facility.ContactPhone,
                Labor = facility.Labor,
                Acreage = facility.Acreage,
                CommuneId = facility.CommuneId,
                URL = facility.ImageAnimalStorage
            };

            return View(facilityVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnimalFacilityViewModel facilityVM)
        {
            if (!ModelState.IsValid)
            {
                await PopulateCommunes();
                return View(facilityVM);
            }

            var facility = await _animalFacilityRepository.GetByIdAsyncNoTracking(id);
            if (facility == null) return View("Error");

            facility.Name = facilityVM.Name;
            facility.Address = facilityVM.Address;
            facility.ContactFace = facilityVM.ContactFace;
            facility.ContactMail = facilityVM.ContactMail;
            facility.ContactPhone = facilityVM.ContactPhone;
            facility.Labor = facilityVM.Labor;
            facility.Acreage = facilityVM.Acreage;
            facility.CommuneId = facilityVM.CommuneId;
            // Xóa ảnh nếu người dùng chọn "RemoveImage"
            if (facilityVM.RemoveImage && !string.IsNullOrEmpty(facility.ImageAnimalStorage))
            {
                var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", facility.ImageAnimalStorage);
                if (System.IO.File.Exists(imagePath))
                {
                    System.IO.File.Delete(imagePath); // Xóa file ảnh vật lý
                }
                facility.ImageAnimalStorage = null; // Xóa thông tin ảnh trong cơ sở dữ liệu
            }
            else if (facilityVM.ImageAnimalStorage != null)
            {
                // Upload ảnh mới
                facility.ImageAnimalStorage = UploadFile(facilityVM);
            }
            else
            {
                // Sử dụng ảnh cũ nếu không xóa và không tải ảnh mới
                facility.ImageAnimalStorage = facilityVM.URL;
            }

            _animalFacilityRepository.Update(facility);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SearchByName(string name)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Dữ liệu không hợp lệ.");
                IEnumerable<AnimalFacility> facilities = await _animalFacilityRepository.GetAll();
                return View("Index", facilities); // Trả về danh sách tất cả nếu dữ liệu không hợp lệ
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                var facilities = await _animalFacilityRepository.GetAll();
                return View("Index", facilities);
            }

            var searchResults = await _animalFacilityRepository.GetStorageByName(name);
            return View("Index", searchResults);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var facility = await _animalFacilityRepository.GetIdByAsync(id);
            if (facility == null) return View("Error");
            return View(facility);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var facility = await _animalFacilityRepository.GetIdByAsync(id);
            if (facility == null) 
            {
                return View("Error");
            }
            _animalFacilityRepository.Delete(facility);
            return RedirectToAction("Index");
        }
        
    }
}
