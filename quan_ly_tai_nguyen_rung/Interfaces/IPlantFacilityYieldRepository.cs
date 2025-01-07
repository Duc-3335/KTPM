using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.ViewModel.section2;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IPlantFacilityYieldRepository
    {
        //Task<IEnumerable<PlantFacilityYield>> GetAllOfFacility(int facilityId);
        //Task<PlantFacilityYield> GetIdByAsyncOfFacility(int id, int facilityId);
        //Task<PlantFacilityYield> GetIdByAsyncNoTrackingOfFacility(int id, int facilityId);
        //Task<IEnumerable<PlantFacilityYield>> GetPlantByNameOfFacility(string name, int facilityId);
        bool Add(PlantFacilityYield yield);
        bool Update(PlantFacilityYield yield);
        bool Delete(PlantFacilityYield yield);
        bool Save();
        Task<PlantFacilityYield> GetYieldByYearMonthAndFacilityAsync(int year, int month, int facilityId, int plantId);
        Task<PlantFacilityYield> GetByIdAsync(int id);
        Task<PlantFacilityYieldStatisticsViewModel> GetStatisticsByFacilityAsync(int facilityId);
    }
}
