using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using quan_ly_tai_nguyen_rung.DATA.@enum;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("PLANT_TYPE")]
    public class PlantType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("TYPE")]
        public DATA.@enum.type_plant Type { get; set; } // Loại giống cây

        [Required]
        [Column("PRICE")]
        public int Price { get; set; } // Giá

        [Required]
        [Column("HEIGHT")]
        public int Height { get; set; } // Chiều cao cây giống
    }
}
