using quan_ly_tai_nguyen_rung.Models.section2;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IPlantRepository
    {
        Task<IEnumerable<PlantType>> GetAllOfFacility(int facilityId);
        Task<PlantType> GetIdByAsyncOfFacility(int id, int facilityId);
        Task<PlantType> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId);
        Task<IEnumerable<PlantType>> GetPlantByNameOfFacility(string name, int facilityId);
        bool Add(PlantType plant);
        bool Update(PlantType plant);
        bool Delete(PlantType plant);
        bool Save();
    }
}
