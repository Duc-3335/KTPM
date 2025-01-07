using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client.Extensions.Msal;
using quan_ly_tai_nguyen_rung.DATA;
using quan_ly_tai_nguyen_rung.Interfaces;
using quan_ly_tai_nguyen_rung.Models;
using quan_ly_tai_nguyen_rung.Models.section4;
using quan_ly_tai_nguyen_rung.ViewModel;
using quan_ly_tai_nguyen_rung.ViewModel.section4;

namespace quan_ly_tai_nguyen_rung.Repository
{
    public class FluctuationRepository : IFluctuationRepository
    {
        private readonly ApplicationDbContext _context;
        public FluctuationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Fluctuation f)
        {
            _context.Fluctuations.Add(f);
            return Save();
        }

        public bool Delete(Fluctuation f)
        {
            _context.Fluctuations.Remove(f);
            return Save();
        }

        public async Task<IEnumerable<Fluctuation>> GetAllOfFacility(int facilityId)
        {
            return await _context.Fluctuations
                .Include(i => i.Animal)
                .Where(p => p.Animal.AnimalFacilityId == facilityId)
                .ToListAsync();
        }

        public async Task<Fluctuation> GetIdByAsync(int id)
        {
            return await _context.Fluctuations.Include(i => i.Animal).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<FluctuationStatisticViewModel> GetFluctuationByFacilityAsync(int facilityId)
        {
            var facility = await _context.AnimalFacilities.FirstOrDefaultAsync(f => f.Id == facilityId);
            if (facility == null)
            {
                throw new ArgumentException("Facility not found.");
            }

            var animalIds = await _context.Animals
                .Where(a => a.AnimalFacilityId == facilityId)
                .Select(a => a.Id)
                .ToListAsync();

            var fluctuations = await _context.Fluctuations
                .Where(f => animalIds.Contains(f.AnimalId))
                .Include(f => f.Animal)
                .ToListAsync();

            var groupedByYear = fluctuations
                .GroupBy(f => f.Year)
                .Select(g => new YearlyFluctuationStatistics
                {
                    Year = g.Key,
                    QuarterlyData = g.GroupBy(f => f.Quarter)
                        .Select(q => new QuarterlyFluctuationStatistics
                        {
                            Quarter = q.Key,
                            MonthlyData = q.GroupBy(f => f.Month)
                                .Select(m => new MonthlyFluctuationStatistics
                                {
                                    Month = m.Key,
                                    TotalQuantityChange = m.Sum(f => f.QuantityChange),
                                    FluctuationDetails = m.GroupBy(f => f.Animal.Name)
                                        .Select(a => new FluctuationDetails
                                        {
                                            AnimalName = a.Key,
                                            QuantityChange = a.Sum(x => x.QuantityChange),
                                            Reasons = a.Select(x => x.Reason).Distinct().ToList()
                                        }).ToList()
                                }).ToList()
                        }).ToList()
                }).ToList();

            return new FluctuationStatisticViewModel
            {
                FacilityName = facility.Name,
                FacilityId = facility.Id,
                YearlyData = groupedByYear
            };
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0;
        }

        public bool Update(Fluctuation f)
        {
            _context.Update(f);
            return Save();
        }
    }
}
