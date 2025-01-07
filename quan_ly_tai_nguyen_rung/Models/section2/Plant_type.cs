using quan_ly_tai_nguyen_rung.DATA.@enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("PLANT")]
    public class Plant
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; } // Tên giống cây

        [Required]
        [Column("TYPE")]
        public DATA.@enum.type_plant Type { get; set; } // Loại giống cây

        [Required]
        [Column("PRICE")]
        public int Price { get; set; } // Giá bán (VNĐ)

        [Required]
        [Column("HEIGHT")]
        public int Height { get; set; } // Chiều cao cây giống (cm)

        [Required]
        [Column("ID_PLANT_FACILITY")]
        public int PlantFacilityId { get; set; } // ID của cơ sở sản xuất giống cây trồng

        [ForeignKey("PlantFacilityId")]
        public PlantFacility PlantFacility { get; set; } // Quan hệ với bảng PlantFacility
    }
}
