using quan_ly_tai_nguyen_rung.DATA.@enum;
using System;
using System.ComponentModel.DataAnnotations;

namespace quan_ly_tai_nguyen_rung.ViewModel.section4
{
    public class AnimalViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống.")]
        [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Chủng loại không được để trống.")]
        public DATA.@enum.generic Generic { get; set; } // Giữ nguyên kiểu enum

        [Required(ErrorMessage = "Ngày tìm thấy không được để trống.")]
        public DateTime DateFound { get; set; } // NGÀY TÌM THẤY 

        [Required(ErrorMessage = "Số lượng cá thể không được để trống.")]
        public int PreviousQuantity { get; set; } // SỐ LƯỢNG CÁ THỂ

        [Required(ErrorMessage = "Trạng thái không được để trống.")]
        public DATA.@enum.status Status { get; set; } // Giữ nguyên kiểu enum

        [Required(ErrorMessage = "Cần chỉ định xem có biến động hay không.")]
        public bool HasFluctuation { get; set; }  // có biến động hay không

        [Required(ErrorMessage = "Số lượng hiện tại không được để trống.")]
        public int CurrentQuantity { get; set; } // SỐ LƯỢNG HIỆN TẠI

        [Required(ErrorMessage = "ID cơ sở động vật không được để trống.")]
        public int AnimalFacilityId { get; set; } // ID cơ sở động vật
    }
}
