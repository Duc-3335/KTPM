using quan_ly_tai_nguyen_rung.Models.section2;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section2
{
    [Table("PLANT_TYPE_PLANT_FACILITY")]
    public class PlantTypePlantFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("PLANT_FACILITY_ID")]
        public int PlantFacilityID { get; set; }

        [ForeignKey("PlantFacilityId")]
        public virtual PlantFacility PlantFacility { get; set; } // Mối quan hệ với PlantFacility

        [Required]
        [Column("PLANT_TYPE_ID")]
        public int PlantTypeID { get; set; }

        [ForeignKey("PlantTypeId")]
        public virtual PlantType Plant { get; set; } // Mối quan hệ với PlantType
    }
}
