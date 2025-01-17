using quan_ly_tai_nguyen_rung.Models.section2;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("PLANT_FACILITY_YIELD")]
    public class PlantFacilityYield
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("ID_PLANT")]
        public int PlantId { get; set; } // Mã cơ sở sản xuất

        [Required]
        [Column("YEAR")]
        public int Year { get; set; } // Năm 

        [Required]
        [Column("QUARTER")]
        public int Quarter { get; set; } //Quý 

        [Required]
        [Column("MONTH")]
        public int Month { get; set; } // Tháng

        [Required]
        [Column("PLANT")]
        public int Yield { get; set; } // Sản lượng CÂY BÁN RA 

        [ForeignKey("PlantId")]
        public Plant Plant { get; set; } // Quan hệ với bảng Plant
    }
}
