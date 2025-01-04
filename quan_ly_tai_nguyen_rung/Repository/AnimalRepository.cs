using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class AnimalRepository : IAnimalRepository
    {
        private readonly ApplicationDbContext _context;

        public AnimalRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Animal animal)
        {
            _context.Animals.Add(animal);
            return Save();
        }

        public bool Update(Animal animal)
        {
            _context.Animals.Update(animal);
            return Save();
        }

        public bool Delete(Animal animal)
        {
            _context.Animals.Remove(animal);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }

        public async Task<IEnumerable<Animal>> GetAllOfFacility(int facilityId)
        {
            return await _context.animalAnimalFacilities
                .Where(aaf => aaf.AnimalFacilityId == facilityId)
                .Select(aaf => aaf.Animal)
                .ToListAsync();
        }

        public async Task<Animal> GetIdByAsyncOfFacility(int id, int facilityId)
        {
            return await _context.animalAnimalFacilities
                .Where(aaf => aaf.AnimalFacilityId == facilityId && aaf.AnimalId == id)
                .Select(aaf => aaf.Animal)
                .FirstOrDefaultAsync();
        }

        public async Task<Animal> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId)
        {
            return await _context.animalAnimalFacilities
                .Where(aaf => aaf.AnimalFacilityId == facilityId && aaf.AnimalId == id)
                .Select(aaf => aaf.Animal)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalByNameOfFacility(string name, int facilityId)
        {
            name = ConvertString(name);
            return await _context.animalAnimalFacilities
                .Where(aaf => aaf.AnimalFacilityId == facilityId && aaf.Animal.Name.Replace(" ", "").ToLower().Contains(name))
                .Select(aaf => aaf.Animal)
                .ToListAsync();
        }

        private string ConvertString(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? "" : str.Replace(" ", "").ToLower();
        }
    }
}
