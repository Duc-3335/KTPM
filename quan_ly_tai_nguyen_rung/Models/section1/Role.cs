using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("ROLES")]
    public class Role
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("ROLES")]
        public bool IsAdmin { get; set; } // True: Admin, False: User
    }
}
