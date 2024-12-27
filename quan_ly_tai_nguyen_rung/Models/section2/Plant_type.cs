using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("PLANT_TYPE")] // Mapping đến bảng PLANT_VARIETY_TYPE trong cơ sở dữ liệu
    public class PlantType
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string NAME { get; set; }

        [Required]
        public DATA.@enum.type_plant TYPE { get; set; } // Loại giống cây

        [Required]
        public int PRICE { get; set; } // Giá

        [Required]
        public int HEIGHT { get; set; } // Chiều cao cây giống

        [ForeignKey("PlantFacilityId")] // Chỉ định rằng ID_PLANT_BREEDING_FACILITY là khóa ngoại đến bảng PLANT_BREEDING_FACILITY
        public int PlantFacilityId { get; set; }

        // Navigation property
        public virtual PlantFacility PlantFacility { get; set; } // Tham chiếu đến bảng PLANT_BREEDING_FACILITY
    }
}
