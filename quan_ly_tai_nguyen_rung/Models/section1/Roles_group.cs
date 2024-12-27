using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("ROLES_GROUP")] 
    public class RolesGroup
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("ROLES")]
        public bool Role { get; set; } // 0: USER, 1: ADMIN
    }
}

