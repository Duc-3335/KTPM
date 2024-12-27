using quan_ly_tai_nguyen_rung.Models.section4;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("COMMUNE")]
    public class Commune
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [Column("ID_DISTRICT")]
        public int DistrictId { get; set; }

        [ForeignKey("DistrictId")]
        public District District { get; set; }
        public ICollection<AnimalFacility> AnimalFacilities { get; set; } // Navigation Property
    }
}
