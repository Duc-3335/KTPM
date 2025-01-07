using quan_ly_tai_nguyen_rung.Models.section2;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.ViewModel.section2
{

    public class PlantFacilityYieldViewModel
    {
        public int Year { get; set; }
        public int Quarter { get; set; }
        public int Month { get; set; }
        public int PlantId { get; set; } 
        public int Yield { get; set; } 
    }

}
