using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using quan_ly_tai_nguyen_rung.Models.section1;

namespace quan_ly_tai_nguyen_rung.Models.section4
{
    [Table("ANIMAL_FACILITY")]
    public class AnimalFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("ADDRESS")]
        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [Column("CONTACT_FACE")]
        [StringLength(100)]
        public string ContactFace { get; set; }

        [Required]
        [Column("CONTACT_MAIL")]
        [StringLength(100)]
        public string ContactMail { get; set; }

        [Required]
        [Column("CONTACT_PHONE")]
        [StringLength(100)]
        public string ContactPhone { get; set; }

        [Required]
        [Column("LABOR")]
        public int Labor { get; set; }

        [Required]
        [Column("ACREAGE")]
        public double Acreage { get; set; }
        
        [Column("IMAGE_ANIMAL_STORAGE")]
        public byte[]? ImageAnimalStorage { get; set; }

        [Required]
        [Column("ID_COMMUNE")]
        public int CommuneId { get; set; }

        [ForeignKey("CommuneId")]
        public Commune Commune { get; set; }
        public ICollection<AnimalAnimalFacility> animalAnimalFacilities { get; set; }
    }
}
