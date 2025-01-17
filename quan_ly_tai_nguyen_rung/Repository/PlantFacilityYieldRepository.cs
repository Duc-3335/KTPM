using Microsoft.EntityFrameworkCore;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models.section2;
using quan_ly_tai_nguyen_rung.ViewModel;
using quan_ly_tai_nguyen_rung.ViewModel.section2;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class PlantFacilityYieldRepository : IPlantFacilityYieldRepository
    {
        private readonly ApplicationDbContext _context;

        public PlantFacilityYieldRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(PlantFacilityYield yield)
        {
            _context.PlantFacilityYields.Add(yield);
            return Save();
        }

        public bool Update(PlantFacilityYield yield)
        {
            _context.PlantFacilityYields.Update(yield);
            return Save();
        }

        public bool Delete(PlantFacilityYield yield)
        {
            _context.PlantFacilityYields.Remove(yield);
            return Save();
        }

        public bool Save()
        {
            return _context.SaveChanges() > 0;
        }
        public async Task<PlantFacilityYieldStatisticsViewModel> GetStatisticsByFacilityAsync(int facilityId)
        {
            var facility = await _context.PlantFacilities.FirstOrDefaultAsync(f => f.Id == facilityId);
            if (facility == null)
            {
                throw new ArgumentException("Facility not found.");
            }

            var plantIds = await _context.Plants
                .Where(p => p.PlantFacilityId == facilityId)
                .Select(p => p.Id)
                .ToListAsync();

            var yields = await _context.PlantFacilityYields
                .Where(y => plantIds.Contains(y.PlantId))
                .Include(y => y.Plant)
                .ToListAsync();

            var groupedByYear = yields
                .GroupBy(y => y.Year)
                .Select(n => new YearlyStatistics
                {
                    Year = n.Key,
                    QuarterlyData = n.GroupBy(y => y.Quarter)
                        .Select(q => new QuarterlyStatistics
                        {
                            Quarter = q.Key,
                            MonthlyData = q.GroupBy(m => m.Month)
                                .Select(t => new MonthlyStatistics
                                {
                                    Month = t.Key,
                                    TotalNumber = t.Sum(m => m.Yield),
                                    PlantData = t.GroupBy(p => p.Plant.Name)
                                        .Select(p => new PlantStatistics
                                        {
                                            id = p.First().Id,
                                            PlantId = p.First().PlantId,
                                            PlantName = p.Key,
                                            TotalSold = p.Sum(x => x.Yield)
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                }).ToList();

            return new PlantFacilityYieldStatisticsViewModel
            {
                FacilityName = facility.Name,
                FacilityId = facility.Id,
                YearlyData = groupedByYear
            };

        }
        public async Task<PlantFacilityYield> GetYieldByYearMonthAndFacilityAsync(int year, int month, int facilityId, int plantId)
        {
            return await _context.PlantFacilityYields
                .Include(y => y.Plant) // Kết hợp với bảng Plant
                    .ThenInclude(p => p.PlantFacility) // Kết hợp với bảng Facility
                .FirstOrDefaultAsync(y => y.Year == year &&
                                           y.Month == month &&
                                           y.Plant.PlantFacilityId== facilityId &&
                                           y.PlantId == plantId);
        }


        public async Task<PlantFacilityYield> GetByIdAsync(int id)
        {
            return await _context.PlantFacilityYields.FindAsync(id);
        }



    }
}
