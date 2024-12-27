using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class AnimalAnimalFacilityRepository : IAnimalAnimalFacilityRepository
    {
        private readonly ApplicationDbContext _context;
        public AnimalAnimalFacilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(AnimalAnimalFacility af)
        {
            _context.Add(af);
            return Save();
        }

        public bool Delete(AnimalAnimalFacility af)
        {
            _context.Remove(af);
            return Save();
        }

        public async Task<IEnumerable<AnimalAnimalFacility>> GetAll()
        {
            return await _context.animalAnimalFacilities
                .Include(a => a.Animal)
                .Include(b => b.AnimalFacility)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnimalAnimalFacility>> GetAnimalByFacility(string name)
        {
            return await _context.animalAnimalFacilities
                .Include(i => i.AnimalFacility)
                .Where(c => c.AnimalFacility.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<AnimalAnimalFacility>> GetAnimalByFacility(int id)
        {
            return await _context.animalAnimalFacilities
                .Include(i => i.AnimalFacility)
                .Where(c => c.AnimalFacility.Id == id)
                .ToListAsync();
        }

        public async Task<IEnumerable<AnimalAnimalFacility>> GetFacilityByAnimal(string name)
        {
            return await _context.animalAnimalFacilities
                .Include(i => i.Animal)
                .Where(c => c.Animal.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<AnimalAnimalFacility>> GetFacilityByAnimal(int id)
        {
            return await _context.animalAnimalFacilities
                .Include(i => i.Animal)
                .Where(c => c.Animal.Id == id)
                .ToListAsync();
        }
        public async Task<IEnumerable<AnimalAnimalFacility>> GetAnimalOrFacility(string name)
        {
            name = string
                .IsNullOrWhiteSpace(name) ? "" : name
                .Replace(" ", "")
                .ToLower();
            return await _context.animalAnimalFacilities
                .Include(a => a.Animal)
                .Include(f => f.AnimalFacility)
                .Where(af => af.Animal.Name.Replace(" ", "").ToLower().Contains(name) || af.AnimalFacility.Name.Replace(" ", "").ToLower().Contains(name))
                .ToListAsync();
        }

        public async Task<AnimalAnimalFacility> GetIdByAsync(int id)
        {
            return await _context.animalAnimalFacilities
                .Include(i => i.AnimalFacility)
                .Include(y => y.Animal)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(AnimalAnimalFacility af)
        {
            _context.Update(af);
            return Save();
        }

    }
}
