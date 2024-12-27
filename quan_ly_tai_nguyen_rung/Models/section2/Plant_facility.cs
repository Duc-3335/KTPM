using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using quan_ly_tai_nguyen_rung.Models.section1;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("PLANT_FACILITY")]
    public class PlantFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("STATUS")]
        public bool Status { get; set; } // 0: Off, 1: On

        [Required]
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
        [Column("ACREAGE")]
        public float Acreage { get; set; }

        [Required]
        [Column("SEEDLINGS_YIELD")]
        public float SeedlingsYield { get; set; }

        [Required]
        [Column("LABOR")]
        public int Labor { get; set; }

        [Column("IMAGE_LANT_BREEDING_FACILITY")]
        public byte[] ImagePlantBreedingFacility { get; set; }

        [Required]
        [Column("ID_COMMUNE")]
        public int CommuneId { get; set; }

        [ForeignKey("CommuneId")]
        public Commune Commune { get; set; }
    }
}
