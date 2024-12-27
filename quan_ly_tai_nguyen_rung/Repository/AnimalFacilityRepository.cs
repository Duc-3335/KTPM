using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class AnimalFacilityRepository : IAnimalFacilityRepository
    {
        private readonly ApplicationDbContext _context;
        public AnimalFacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(AnimalFacility storage)
        {
            _context.Add(storage);
            return Save();
        }

        public bool Delete(AnimalFacility storage)
        {
            _context?.Remove(storage);
            return Save();
        }

        public async Task<IEnumerable<AnimalFacility>> GetAll()
        {
            return await _context.AnimalFacilities.ToListAsync();
        }

        public async Task<AnimalFacility> GetIdByAsync(int id)
        {
            return await _context.AnimalFacilities.Include(i => i.Commune).FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<AnimalFacility> GetByIdAsyncNoTracking(int id)
        {
            return await _context.AnimalFacilities
                .Include(i => i.Commune)
                .AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<IEnumerable<AnimalFacility>> GetStorageByName(string name)
        {
            name = string.IsNullOrWhiteSpace(name) ? "" : name.Replace(" ", "").ToLower();
            return await _context.AnimalFacilities.Include(i => i.Commune).Where(c => c.Name.Replace(" ", "").ToLower().Contains(name)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(AnimalFacility storage)
        {
            _context.Update(storage);
            return Save();
        }

    }
}
