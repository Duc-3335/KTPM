using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.Models.section4;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IAnimalRepository
    {
        Task<IEnumerable<Animal>> GetAllOfFacility(int facilityId);
        Task<Animal> GetIdByAsyncOfFacility(int id, int facilityId);
        Task<Animal> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId);
        Task<IEnumerable<Animal>> GetAnimalByNameOfFacility(string name, int facilityId);
        bool Add(Animal animal);
        bool Update(Animal animal);
        bool Delete(Animal animal);
        bool Save();
    }
}
