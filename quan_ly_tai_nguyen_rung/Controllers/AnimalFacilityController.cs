using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.Repository;
using quan_ly_tai_nguyen_rung.ViewModels;

namespace quan_ly_tai_nguyen_rung.Controllers
{
    public class AnimalFacilityController : Controller
    {
        private readonly IAnimalFacilityRepository _animalFacilityRepository;
        private readonly ApplicationDbContext _context;
        public AnimalFacilityController(IAnimalFacilityRepository animalFacilityRepository, ApplicationDbContext context)
        {
            
            _animalFacilityRepository = animalFacilityRepository;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<AnimalFacility> lt = await _animalFacilityRepository.GetAll();
            return View(lt);
        }
        public async Task<IActionResult> Detail(int id) {
            AnimalFacility storage = await _animalFacilityRepository.GetIdByAsync(id);
            return View(storage);
        }
        public IActionResult Create()
        {
            var communes = _context.Communes.ToList();
            ViewBag.Communes = new SelectList(communes, "Id", "Name"); // Tạo SelectList cho dropdown
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> Create (AnimalFacilityViewModel facilityVM)
        {
            if (!ModelState.IsValid)
            {
                
                return View(facilityVM);
            }
            var communes = _context.Communes.ToList();
            ViewBag.Communes = new SelectList(communes, "Id", "Name"); // Tạo SelectList cho dropdown
            var facility = new AnimalFacility
            {
                Name = facilityVM.Name,
                Address = facilityVM.Address,
                ContactFace = facilityVM.ContactFace,
                ContactMail = facilityVM.ContactMail,
                ContactPhone = facilityVM.ContactPhone,
                Labor = facilityVM.Labor,
                Acreage = facilityVM.Acreage,
                ImageAnimalStorage = facilityVM.ImageAnimalStorage,
                CommuneId = facilityVM.CommuneId
            };
            _animalFacilityRepository.Add(facility);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit (int id)
        {
            var facility = await _animalFacilityRepository.GetIdByAsync(id);
            if (facility == null) return View("Error");
            var communes = _context.Communes.ToList();
            ViewBag.Communes = new SelectList(communes, "Id", "Name"); // Tạo SelectList cho dropdown
            var facilityVM = new AnimalFacilityViewModel
            {
                Name = facility.Name,
                Address = facility.Address,
                ContactFace = facility.ContactFace,
                ContactMail = facility.ContactMail,
                ContactPhone = facility.ContactPhone,
                Labor = facility.Labor,
                Acreage = facility.Acreage,
                ImageAnimalStorage = facility.ImageAnimalStorage,
                CommuneId = facility.CommuneId
            };
            return View(facilityVM);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, AnimalFacilityViewModel facilityVM)
        {
            // Lấy danh sách Communes cho dropdown
            var communes = await _context.Communes.AsNoTracking().ToListAsync();
            ViewBag.Communes = new SelectList(communes, "Id", "Name");

            // Kiểm tra tính hợp lệ của model
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit!");
                return View(facilityVM);
            }

            // Lấy thực thể từ repository
            var facility = await _animalFacilityRepository.GetByIdAsyncNoTracking(id);
            if (facility == null)
            {
                return View("Error"); // Trả về trang lỗi nếu không tìm thấy
            }

            // Cập nhật dữ liệu từ ViewModel
            facility.Name = facilityVM.Name;
            facility.Address = facilityVM.Address;
            facility.ContactFace = facilityVM.ContactFace;
            facility.ContactMail = facilityVM.ContactMail;
            facility.ContactPhone = facilityVM.ContactPhone;
            facility.Labor = facilityVM.Labor;
            facility.Acreage = facilityVM.Acreage;
            facility.ImageAnimalStorage = facilityVM.ImageAnimalStorage;
            facility.CommuneId = facilityVM.CommuneId;

            // Gọi phương thức cập nhật trong repository
            _animalFacilityRepository.Update(facility);

            // Chuyển hướng đến trang Index sau khi chỉnh sửa thành công
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SearchByName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                IEnumerable<AnimalFacility> facilities = await _animalFacilityRepository.GetAll();
                return View("Index",facilities);
            }
            IEnumerable<AnimalFacility> searchResults = await _animalFacilityRepository.GetStorageByName(name);
            return View("Index", searchResults);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var facilityDetail = await _animalFacilityRepository.GetIdByAsync(id);
            if (facilityDetail == null) return View("Error");
            return View(facilityDetail);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteFacility(int id)
        {
            var facilityDetail = await _animalFacilityRepository.GetIdByAsync(id);
            if (facilityDetail == null) return View("error");
            //var relatedRecords = _context.animalAnimalFacilities
            //                     .Where(aaf => aaf.AnimalFacilityId == id)
            //                     .ToList();
            //if (relatedRecords.Any())
            //{
            //    _context.animalAnimalFacilities.RemoveRange(relatedRecords);
            //    await _context.SaveChangesAsync(); // Lưu thay đổi để đảm bảo xóa thành công
            //}

            _animalFacilityRepository.Delete(facilityDetail);
            return RedirectToAction("Index");
        }

    }
}
