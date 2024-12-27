using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("USER")]
    public class User
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [Column("NAME")]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("STATUS")]
        public bool? Status { get; set; }

        [Column("EMAIL")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [Column("PHONE")]
        [StringLength(100)]
        public string Phone { get; set; }

        [Column("DISPLAY_NAME")]
        [StringLength(100)]
        public string DisplayName { get; set; }

        [Required]
        [Column("USER_NAME")]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        [Column("PASSWORD")]
        [StringLength(100)]
        public string Password { get; set; }

        [Required]
        [Column("ID_COMMUNE")]
        public int CommuneId { get; set; }

        [ForeignKey("CommuneId")]
        public Commune Commune { get; set; }

        [Required]
        [Column("ID_ROLES")]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
