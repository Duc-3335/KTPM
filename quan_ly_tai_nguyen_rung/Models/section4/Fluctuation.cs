using quan_ly_tai_nguyen_rung.Models.section4;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models
{
    [Table("FLUCTUATION")]
    public class Fluctuation
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("ID_ANIMAL")]
        public int AnimalId { get; set; }

        [Required]
        [Column("TYPE")]
        public bool Type { get; set; } // LOẠI BIẾN ĐỘNG (TĂNG 1, GIẢM 0)

        [Required]
        [Column("REASON", TypeName = "TEXT")]
        public string Reason { get; set; } // LÝ DO BIẾN ĐỘNG 

        [Required]
        [Column("QUANTITY_CHANGE")]
        public int QuantityChange { get; set; } // SỐ LƯỢNG BIẾN ĐỘNG

        [Required]
        [Column("YEAR")]
        public int Year { get; set; } // NĂM BIẾN ĐỘNG

        [Required]
        [Column("QUARTER")]
        public int Quarter { get; set; } // QUÝ BIẾN ĐỘNG

        [Required]
        [Column("MONTH")]
        public int Month { get; set; } // THÁNG BIẾN ĐỘNG
        // Navigation property
        [ForeignKey("AnimalId")]
        public virtual Animal Animal { get; set; }
    }
}
