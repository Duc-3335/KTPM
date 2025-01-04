using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section2;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class PlantRepository : IPlantRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(PlantType plant)
        {
            _context.PlantTypes.Add(plant);
            return Save();
        }

        public bool Update(PlantType plant)
        {
            _context.PlantTypes.Update(plant);
            return Save();
        }

        public bool Delete(PlantType plant)
        {
            _context.PlantTypes.Remove(plant);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0; // Trả về true nếu lưu thành công
        }

        public async Task<IEnumerable<PlantType>> GetAllOfFacility(int facilityId)
        {
            return await _context.plantPlantFacilities
                .Where(ppf => ppf.PlantFacilityID == facilityId) // Lọc theo FacilityId
                .Select(ppf => ppf.Plant) // Lấy cây từ bảng trung gian
                .ToListAsync();
        }

        public async Task<PlantType> GetIdByAsyncOfFacility(int id, int facilityId)
        {
            return await _context.plantPlantFacilities
                .Where(ppf => ppf.PlantFacilityID == facilityId && ppf.PlantTypeID == id) // Lọc theo FacilityId và PlantId
                .Select(ppf => ppf.Plant)
                .FirstOrDefaultAsync();
        }

        public async Task<PlantType> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId)
        {
            return await _context.plantPlantFacilities
                .Where(ppf => ppf.PlantFacilityID == facilityId && ppf.PlantTypeID == id) // Lọc theo FacilityId và PlantId
                .Select(ppf => ppf.Plant)
                .AsNoTracking() // Trả về dữ liệu không theo dõi
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PlantType>> GetPlantByNameOfFacility(string name, int facilityId)
        {
            name = ConvertString(name);
            return await _context.plantPlantFacilities
                .Where(ppf => ppf.PlantFacilityID == facilityId && ppf.Plant.Name.Replace(" ", "").ToLower().Contains(name)) // Tìm kiếm theo tên trong facility
                .Select(ppf => ppf.Plant)
                .ToListAsync();
        }

        private string ConvertString(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? "" : str.Replace(" ", "").ToLower();
        }
    }
}
