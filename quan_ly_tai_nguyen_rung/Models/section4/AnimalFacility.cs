using quan_ly_tai_nguyen_rung.Models.section1;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section4
{
    [Table("ANIMAL_FACILITY")]
    public class AnimalFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        [StringLength(255)]
        [Column("ADDRESS")]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CONTACT_FACE")]
        public string ContactFace { get; set; } // THÔNG TIN LIÊN HỆ

        [Required]
        [StringLength(100)]
        [Column("CONTACT_MAIL")]
        public string ContactMail { get; set; }

        [Required]
        [StringLength(100)]
        [Column("CONTACT_PHONE")]
        public string ContactPhone { get; set; }

        [Required]
        [Column("LABOR")]
        public int Labor { get; set; } // SỐ NHÂN CÔNG 

        [Required]
        [Column("ACREAGE")]
        public float Acreage { get; set; } // DIỆN TÍCH 

        [StringLength(200)]
        [Column("IMAGE_ANIMAL_STORAGE")]
        public string? ImageAnimalStorage { get; set; } // bản đồ cơ sở lưu trữ động vật

        [Required]
        [Column("ID_COMMUNE")]
        public int CommuneId { get; set; }

        // Navigation property
        public virtual Commune Commune { get; set; }
    }
}
