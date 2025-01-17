using quan_ly_tai_nguyen_rung.Models.section2;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.ViewModel.section2
{

    public class PlantFacilityYieldStatisticsViewModel
    {
        public string FacilityName { get; set; }
        public int FacilityId { get; set; }
        public List<YearlyStatistics> YearlyData { get; set; }
    }

    public class YearlyStatistics
    {
        public int Year { get; set; }
        public List<QuarterlyStatistics> QuarterlyData { get; set; }
    }

    public class QuarterlyStatistics
    {
        public int Quarter { get; set; }
        public List<MonthlyStatistics> MonthlyData { get; set; }
    }

    public class MonthlyStatistics
    {
        public int Month { get; set; }
        public int TotalNumber { get; set; }
        public List<PlantStatistics> PlantData { get; set; }
    }

    public class PlantStatistics
    {
        public int id { get; set; }
        public int PlantId { get; set; }
        public string PlantName { get; set; }
        public int TotalSold { get; set; }
    }

}
