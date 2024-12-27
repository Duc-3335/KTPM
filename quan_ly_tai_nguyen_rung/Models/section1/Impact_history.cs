using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("IMPACT_HISTORY")]
    public class ImpactHistory
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("TYPE")]
        [StringLength(100)]
        public DATA.@enum.type_impact Type { get; set; } // Loại tác động

        [Required]
        [Column("TIME")]
        public TimeSpan Time { get; set; }

        [Required]
        [Column("DAY")]
        public DateTime Day { get; set; }

        [Column("ID_USER")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}
