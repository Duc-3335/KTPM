using quan_ly_tai_nguyen_rung.Models.section4;
using System.Runtime.CompilerServices;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IAnimalFacilityRepository
    {
        Task<IEnumerable<AnimalFacility>> GetAll();
        Task<AnimalFacility> GetIdByAsync(int id);
        Task<AnimalFacility> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<AnimalFacility>> GetStorageByName(string name);
        bool Add(AnimalFacility storage);
        bool Update(AnimalFacility storage);
        bool Delete(AnimalFacility storage);
        bool Save();
    }
}
