using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
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
            _context.Add(animal);
            return Save();
        }

        public bool Delete(Animal animal)
        {
            _context.Remove(animal);
            return Save();
        }

        public async Task<IEnumerable<Animal>> GetAll()
        {
            return await _context.Animals.ToListAsync();
        }

        public async Task<IEnumerable<Animal>> GetAnimalByName(string name)
        {
            name = ConvertString(name);
            return await _context.Animals.Where(c => c.Name.Replace(" ", "").ToLower().Contains(name)).ToListAsync();
        }

        public async Task<Animal> GetIdByAsync(int id)
        {
            return await _context.Animals.FirstOrDefaultAsync(a => a.Id == id);
        }
        public async Task<Animal> GetIdBtAsyncNoTracking(int id)
        {
            return await _context.Animals.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Animal animal)
        {
            _context.Update(animal);
            return Save();
        }
        string ConvertString(string str)
        {
            return string.IsNullOrWhiteSpace(str) ? "" : str.Replace(" ", "").ToLower();
        }
    }
}
