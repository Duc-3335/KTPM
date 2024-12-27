using quan_ly_tai_nguyen_rung.Models.section1;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section3
{
    [Table("WOOD_PROCESSING_FACILITY")]
    public class WoodProcessingFacility
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("STATUS")]
        public bool Status { get; set; } // 0 OFF, 1 ON 

        [Required]
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
        public double Acreage { get; set; } // DIỆN TÍCH HECTA

        [Column("Yield")]
        public double? Yield { get; set; } // SẢN LƯỢNG TẤN / NĂM

        [StringLength(500)]
        [Column("WOOD_SPECIES_PROVIDED")]
        public string? WoodSpeciesProvided { get; set; } // LOẠI GỖ ĐẦU VÀO 

        [Required]
        [StringLength(300)]
        [Column("PRODUCT")]
        public DATA.@enum.product Product { get; set; } // SẢN PHẨM 

        [Required]
        [StringLength(300)]
        [Column("PRODUCTION_TYPE")]
        public DATA.@enum.production_type ProductionType { get; set; } // loại hình sản xuất

        [Required]
        [StringLength(300)]
        [Column("ACTIVITY_FORM")]
        public DATA.@enum.activity_form ActivityForm { get; set; } // hình thức hoạt động

        [Column("IMAGE_WOOD_PROCESSING_FACILITY")]
        public byte[] ImageWoodProcessingFacility { get; set; } // bản đồ cơ sở CHẾ BIẾN GỖ 

        [Required]
        [Column("ID_COMMUNE")]
        public int IdCommune { get; set; }

        [ForeignKey("IdCommune")]
        public Commune Commune { get; set; }
    }
}
