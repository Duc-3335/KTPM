using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.DATA.@enum;

namespace quan_ly_tai_nguyen_rung.Models.ViewModels
{
    public class PlantViewModel
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Tên cây là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên cây không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Loại cây là bắt buộc.")]
        public DATA.@enum.type_plant type { get; set; }

        [Required(ErrorMessage = "Giá là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Giá phải là số dương.")]
        public int Price { get; set; } // Giá

        [Required(ErrorMessage = "Chiều cao là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Chiều cao phải là số dương.")]
        public int Height { get; set; } // Chiều cao cây giống
    }
}
