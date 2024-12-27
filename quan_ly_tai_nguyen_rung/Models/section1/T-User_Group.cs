using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section1
{
    [Table("USER_GROUP")] 
    public class UserGroup
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [ForeignKey("User")] // Chỉ định rằng ID_USER là khóa ngoại đến bảng USER
        public int IdUser { get; set; }

        [ForeignKey("Group")] // Chỉ định rằng ID_GROUP là khóa ngoại đến bảng GROUPS
        public int IdGroup { get; set; }

        // Navigation properties
        public virtual User User { get; set; }
        public virtual Group Group { get; set; }
    }
}

