using quan_ly_tai_nguyen_rung.Models.section2;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IPlantRepository
    {
        Task<IEnumerable<Plant>> GetAllOfFacility(int facilityId);
        Task<Plant> GetIdByAsyncOfFacility(int id, int facilityId);
        Task<Plant> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId);
        Task<IEnumerable<Plant>> GetPlantByNameOfFacility(string name, int facilityId);
        bool Add(Plant plant);
        bool Update(Plant plant);
        bool Delete(Plant plant);
        bool Save();
    }
}
