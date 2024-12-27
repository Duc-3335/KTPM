using quan_ly_tai_nguyen_rung.DATA.@enum;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.Models.section4
{
    [Table("ANIMAL")]
    public class Animal
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Column("NAME")]
        public string Name { get; set; }

        [Required]
        [Column("GENERIC")]
        public DATA.@enum.generic Generic { get; set; } // Đảm bảo Generic đã được định nghĩa trong DATA namespace

        [Required]
        [Column("DATE_FOUND")]
        [DataType(DataType.Date)]
        public DateTime DateFound { get; set; }

        [Required]
        [Column("PREVIOUS_QUANTITY")]
        public int PreviousQuantity { get; set; } // số lượng 

        [Required]
        [Column("STATUS")]
        public DATA.@enum.status Status { get; set; } // Đảm bảo Status đã được định nghĩa trong DATA namespace

        [Required]
        [Column("FLUCTUATION")]
        public bool Fluctuation { get; set; } // biến động
        [Required]
        [Column("DATE")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // Ngày biến động
        [Required]
        [Column("REASON", TypeName = "TEXT")]
        public string Reason { get; set; } // Lý do biến động
        [StringLength(255)]
        [Required]
        [Column("LOCATION")]
        public string Location { get; set; }
        [Required]
        [Column("IS_ACTIVE")]
        public bool is_Active { get; set; }
        [Required]
        [Column("CURRENT_QUANTITY")]
        public int CurrentQuantity { get; set; }
        public ICollection<AnimalAnimalFacility> animalAnimalFacilities { get; set; }

    }
}
