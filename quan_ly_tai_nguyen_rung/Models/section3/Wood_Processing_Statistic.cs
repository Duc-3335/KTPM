using quan_ly_tai_nguyen_rung.Models.section3;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("WOOD_PROCESSING_STATISTICS")]
    public class WoodProcessingStatistics
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("ID_WOOD_PROCESSING_FACILITY")]
        public int WoodProcessingFacilityId { get; set; }

        [Required]
        [Column("MONTH")]
        public int Month { get; set; } // Tháng

        [Required]
        [Column("YIELD")]
        public float Yield { get; set; } // Sản lượng

        [ForeignKey("WoodProcessingFacilityId")]
        public WoodProcessingFacility WoodProcessingFacility { get; set; }
    }
}
