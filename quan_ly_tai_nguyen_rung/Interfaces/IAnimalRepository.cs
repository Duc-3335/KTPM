using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAll();
        Task<Animal> GetIdByAsync(int id);
        public Task<Animal> GetIdBtAsyncNoTracking(int id);
        bool Add(Animal animal);
        Task<IEnumerable<Animal>> GetAnimalByName(string name);
        bool Update(Animal animal);
        bool Delete(Animal animal);
        bool Save();
    }
}
