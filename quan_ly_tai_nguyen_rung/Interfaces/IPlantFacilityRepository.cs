using quan_ly_tai_nguyen_rung.Models.section2;
using System.Runtime.CompilerServices;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IPlantFacilityRepository
    {
        Task<IEnumerable<PlantFacility>> GetAll();
        Task<PlantFacility> GetIdByAsync(int id);
        Task<PlantFacility> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<PlantFacility>> GetFacilityByName(string name);
        bool Add(PlantFacility facility);
        bool Update(PlantFacility facility);
        bool Delete(PlantFacility facility);
        bool Save();
    }
}
