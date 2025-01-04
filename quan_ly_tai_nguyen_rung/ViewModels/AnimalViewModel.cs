using quan_ly_tai_nguyen_rung.Models.section4;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.DATA.@enum;

namespace quan_ly_tai_nguyen_rung.ViewModels
{
    public class AnimalViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DATA.@enum.generic Generic { get; set; } // Giữ nguyên kiểu enum

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateFound { get; set; } // Giữ nguyên kiểu DateTime

        [Required]
        public int PreviousQuantity { get; set; } // Giữ nguyên kiểu int

        [Required]
        public DATA.@enum.status Status { get; set; } // Giữ nguyên kiểu enum

        [Required]
        public bool Fluctuation { get; set; } // Giữ nguyên kiểu bool

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } // Giữ nguyên kiểu DateTime

        [Required]
        [StringLength(255)]
        public string Reason { get; set; }

        [Required]
        [StringLength(255)]
        public string Location { get; set; }

        [Required]
        public DATA.@enum.Is_active IsActive { get; set; } // Đổi từ bool sang kiểu enum

        [Required]
        public int CurrentQuantity { get; set; }

        // Các thuộc tính cho dropdown
        public IEnumerable<SelectListItem> GenericOptions { get; set; } // Lựa chọn cho Generic
        public IEnumerable<SelectListItem> StatusOptions { get; set; } // Lựa chọn cho Status
        public IEnumerable<SelectListItem> IsActiveOptions { get; set; } // Lựa chọn cho IsActive
    }
}
