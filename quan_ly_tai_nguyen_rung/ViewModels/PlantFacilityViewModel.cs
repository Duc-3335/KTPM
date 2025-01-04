using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.Models.section1;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.ViewModels
{
    public class PlantFacilityViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên cơ sở là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên cơ sở không được vượt quá 100 ký tự.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Trạng thái là bắt buộc.")]
        public bool Status { get; set; }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc.")]
        [StringLength(255, ErrorMessage = "Địa chỉ không được vượt quá 255 ký tự.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Người liên hệ là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên người liên hệ không được vượt quá 100 ký tự.")]
        public string ContactFace { get; set; }

        [Required(ErrorMessage = "Email liên hệ là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Email không được vượt quá 100 ký tự.")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không hợp lệ.")]
        public string ContactMail { get; set; }

        [Required(ErrorMessage = "Số điện thoại là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Số điện thoại không được vượt quá 100 ký tự.")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ.")]
        public string ContactPhone { get; set; }

        [Required(ErrorMessage = "Số lao động là bắt buộc.")]
        public int Labor { get; set; } 

        [Required(ErrorMessage = "Diện tích là bắt buộc.")]
        public float Acreage { get; set; } 

        [Required(ErrorMessage = "Năng suất cây giống là bắt buộc.")]
        public float SeedlingsYield { get; set; } 

        public byte[]? ImagePlantBreedingFacility { get; set; }

        [Required]
        public int CommuneId { get; set; } // Lưu giá trị commune được chọn từ dropdown
    }
}
