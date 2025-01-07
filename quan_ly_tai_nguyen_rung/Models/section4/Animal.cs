using quan_ly_tai_nguyen_rung.Models.section4;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section4
{
    [Table("ANIMAL")]
    public class Animal
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("GENERIC")]
        public DATA.@enum.generic Generic { get; set; } // CHỦNG LOẠI

        [Required]
        [Column("DATE_FOUND")]
        public DateTime DateFound { get; set; } // NGÀY TÌM THẤY 

        [Required]
        [Column("PREVIOUS_QUANTITY")]
        public int PreviousQuantity { get; set; } // SỐ LƯỢNG CÁ THỂ

        [Required]
        [Column("STATUS")]
        public DATA.@enum.status Status { get; set; } // TRẠNG THÁI BẢO TỒN

        [Required]
        [Column("HAS_FLUCTUATION")]
        public bool HasFluctuation { get; set; } // có biến động hay không

        [Required]
        [Column("CURRENT_QUANTITY")]
        public int CurrentQuantity { get; set; } // SỐ LƯỢNG HIỆN TẠI

        [Required]
        [Column("ID_ANIMAL_FACILITY")]
        public int AnimalFacilityId { get; set; }

        // Navigation property
        [ForeignKey("IdAnimalFacility")]
        public virtual AnimalFacility AnimalFacility { get; set; }
    }
}
