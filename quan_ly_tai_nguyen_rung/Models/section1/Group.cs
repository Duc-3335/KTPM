using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("GROUPS")] 
    public class Group
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
        public bool Status { get; set; } // 0: OFF, 1: ON

        [Column("ID_ROLES_GROUP")]
        public int RolesGroupId { get; set; }

        [ForeignKey("RolesGroupId")]
        public RolesGroup RolesGroup { get; set; } 
    }
}

