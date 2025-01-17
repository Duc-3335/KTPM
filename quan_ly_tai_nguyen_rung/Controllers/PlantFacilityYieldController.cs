using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section1;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Repository;
using quan_ly_tai_nguyen_rung.ViewModel.section2;
using quan_ly_tai_nguyen_rung.ViewModels;

public class PlantFacilityYieldController : Controller
{
    private readonly IPlantFacilityYieldRepository _plantFacilityYieldRepository;
    private readonly ApplicationDbContext _context;
    private readonly IPlantRepository _plantRepository;

    public PlantFacilityYieldController(
        IPlantFacilityYieldRepository plantFacilityYieldRepository,
        IPlantRepository plantRepository,
        ApplicationDbContext context)
    {
        _plantFacilityYieldRepository = plantFacilityYieldRepository;
        _context = context;
        _plantRepository = plantRepository;
    }

    // Hiển thị danh sách sản lượng theo cơ sở
    public async Task<IActionResult> Index(int facilityId)
    {
        ViewBag.FacilityId = facilityId;
        var statistics = await _plantFacilityYieldRepository.GetStatisticsByFacilityAsync(facilityId);
        return View(statistics);
    }

    private async Task PopulatePlants(int facilityId)
    {
        var plants = await _plantRepository.GetAllOfFacility(facilityId);
        ViewBag.Plants = new SelectList(plants, "Id", "Name");
    }

    // Thêm mới: Hiển thị form
    public async Task<IActionResult> Create(int facilityId)
    {
        ViewBag.FacilityId = facilityId;
        await PopulatePlants(facilityId);
        return View();
    }

    // Thêm mới: Lưu vào DB
    [HttpPost]
    public async Task<IActionResult> Create(PlantFacilityYieldViewModel yieldViewModel, int facilityId)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.FacilityId = facilityId;
            return View(yieldViewModel);
        }
        var newYield = new PlantFacilityYield
        {
            Year = yieldViewModel.Year,
            Quarter = yieldViewModel.Quarter,
            Month = yieldViewModel.Month,
            Yield = yieldViewModel.Yield,
            PlantId = yieldViewModel.PlantId,
        };

        // Thêm đối tượng mới vào repository
        _plantFacilityYieldRepository.Add(newYield);

        // Lưu thay đổi vào cơ sở dữ liệu
        await _context.SaveChangesAsync();

        // Thêm thông báo thành công vào TempData để hiển thị cho người dùng
        TempData["SuccessMessage"] = "Dữ liệu đã được thêm thành công!";

        // Chuyển hướng về action Index với facilityId đã cho
        return RedirectToAction(nameof(Index), new { facilityId = facilityId });
    }


    // Hiển thị form chỉnh sửa
    // GET Edit
    public async Task<IActionResult> Edit(int year, int month, int facilityId, int plantId)
    {
        var yieldData = await _plantFacilityYieldRepository.GetYieldByYearMonthAndFacilityAsync(year, month, facilityId, plantId);
        ViewBag.FacilityId = facilityId;
        if (yieldData == null)
        {
            return NotFound("Dữ liệu không tồn tại.");
        }

        // Gọi PopulatePlants để có danh sách cây trồng
        await PopulatePlants(facilityId);

        var editViewModel = new PlantFacilityYieldViewModel
        {
            Year = yieldData.Year,
            Month = yieldData.Month,
            Quarter = yieldData.Quarter,
            PlantId = yieldData.PlantId,
            Yield = yieldData.Yield,
        };

        return View(editViewModel);
    }

    // POST Edit
    [HttpPost]
    public async Task<IActionResult> Edit(int id, PlantFacilityYieldViewModel model, int facilityId)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.FacilityId = facilityId;
            return View(model);
        }

        var yieldData = await _plantFacilityYieldRepository.GetByIdAsync(id);
        if (yieldData == null)
        {
            return NotFound("Dữ liệu không tồn tại.");
        }

        // Cập nhật các trường cần thiết
        yieldData.Year = model.Year;
        yieldData.Month = model.Month;
        yieldData.Quarter = model.Quarter;
        yieldData.PlantId = model.PlantId; // Cập nhật PlantId từ model
        yieldData.Yield = model.Yield;

        _plantFacilityYieldRepository.Update(yieldData);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = "Dữ liệu đã được cập nhật thành công!";
        return RedirectToAction(nameof(Index), new { facilityId = facilityId });
    }


    // Xóa: Hiển thị xác nhận
    // GET Delete
    public async Task<IActionResult> Delete(int id, int facilityId)
    {
        var record = await _plantFacilityYieldRepository.GetByIdAsync(id);
        ViewBag.FacilityId = facilityId;
        if (record == null)
        {
            return NotFound("Dữ liệu không tồn tại.");
        }
        return View(record);
    }

    // POST Delete
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id, int facilityId)
    {
        var record = await _plantFacilityYieldRepository.GetByIdAsync(id);
        ViewBag.FacilityId = facilityId;
        if (record != null)
        {
            _plantFacilityYieldRepository.Delete(record); // Xóa bản ghi đã tìm thấy
            TempData["SuccessMessage"] = "Dữ liệu đã được xóa thành công!";
        }
        else
        {
            TempData["ErrorMessage"] = "Dữ liệu không tồn tại hoặc không thuộc cơ sở này.";
        }
        return RedirectToAction(nameof(Index), new { facilityId = facilityId });
    }

}
