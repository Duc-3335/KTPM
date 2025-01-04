using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using quan_ly_tai_nguyen_rung.Models.section4; // Namespace chứa lớp Animal
using quan_ly_tai_nguyen_rung.Models.section2; // Namespace chứa lớp AnimalFacility

namespace quan_ly_tai_nguyen_rung.Models.section4
{
    [Table("ANIMAL_ANINAL_FACILITY")]
    public class AnimalAnimalFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; } // Khóa chính

        [Required]
        [Column("ID_ANIMAL")]
        public int AnimalId { get; set; } // Khóa ngoại tới bảng ANIMAL

        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; } // Navigation property tới ANIMAL

        [Required]
        [Column("ID_ANIMAL_FACILITY")]
        public int AnimalFacilityId { get; set; } // Khóa ngoại tới bảng ANIMAL_FACILITY

        [ForeignKey("AnimalFacilityId")]
        public virtual AnimalFacility AnimalFacility { get; set; } // Navigation property tới ANIMAL_FACILITY

    }
}