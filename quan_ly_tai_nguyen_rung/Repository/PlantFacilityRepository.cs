using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class PlantFacilityRepository : IPlantFacilityRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantFacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(PlantFacility facility)
        {
            _context.Add(facility);
            return Save();
        }

        public bool Delete(PlantFacility facility)
        {
            _context.Remove(facility);
            return Save();
        }

        public async Task<IEnumerable<PlantFacility>> GetAll()
        {
            return await _context.PlantFacilities.ToListAsync();
        }

        public async Task<PlantFacility> GetIdByAsync(int id)
        {
            return await _context.PlantFacilities
                    .Include(i => i.Commune)
                    .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<PlantFacility> GetByIdAsyncNoTracking(int id)
        {
            return await _context.PlantFacilities
                    .Include(i => i.Commune)
                    .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<PlantFacility>> GetFacilityByName(string name)
        {
            name = string.IsNullOrWhiteSpace(name) ? "" : name.Replace(" ","").ToLower();
            return await _context.PlantFacilities
                    .Include(i => i.Commune)
                    .Where(c => c.Name.Replace(" ","")
                    .ToLower().Contains(name)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(PlantFacility facility)
        {
            _context.Update(facility);
            return Save();
        }
    }
}
