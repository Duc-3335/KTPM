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

        public bool Add(Plant plant)
        {
            _context.Plants.Add(plant);
            return Save();
        }

        public bool Update(Plant plant)
        {
            _context.Plants.Update(plant);
            return Save();
        }

        public bool Delete(Plant plant)
        {
            _context.Plants.Remove(plant);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0; // Trả về true nếu lưu thành công
        }

        public async Task<IEnumerable<Plant>> GetAllOfFacility(int facilityId)
        {
            return await _context.Plants
                .Where(p => p.PlantFacilityId == facilityId) 
                .ToListAsync();
        }

        public async Task<Plant> GetIdByAsyncOfFacility(int id, int facilityId)
        {
            return await _context.Plants
                .Where(p => p.PlantFacilityId == facilityId && p.Id == id) // Lọc theo FacilityId và PlantId
                .FirstOrDefaultAsync();
        }

        public async Task<Plant> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId)
        {
            return await _context.Plants
                .Where(p => p.PlantFacilityId == facilityId && p.Id == id) // Lọc theo FacilityId và PlantId
                .AsNoTracking() // Trả về dữ liệu không theo dõi
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Plant>> GetPlantByNameOfFacility(string name, int facilityId)
        {
            name = ConvertString(name);
            return await _context.Plants
                .Where(p => p.PlantFacilityId == facilityId && p.Name.Replace(" ", "").ToLower().Contains(name)) // Tìm kiếm theo tên trong facility
                .ToListAsync();
        }


        private string ConvertString(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? "" : str.Replace(" ", "").ToLower();
        }
    }
}
