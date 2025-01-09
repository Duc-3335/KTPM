using quan_ly_tai_nguyen_rung.Models;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.ViewModel;
using quan_ly_tai_nguyen_rung.ViewModel.section4;

namespace quan_ly_tai_nguyen_rung.Interfaces
{
    public interface IFluctuationRepository
    {
        bool Add(Fluctuation fluctuation);
        bool Update(Fluctuation fluctuation);
        bool Delete(Fluctuation fluctuation);
        bool Save ();
        Task<IEnumerable<Fluctuation>> GetAllOfFacility(int facilityId); 
        Task<Fluctuation> GetIdByAsync(int id); 
        Task<FluctuationStatisticViewModel> GetFluctuationByFacilityAsync(int facilityId); 
    }
}
