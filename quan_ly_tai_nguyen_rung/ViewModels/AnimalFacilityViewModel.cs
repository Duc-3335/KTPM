using Microsoft.AspNetCore.Mvc.Rendering;
using quan_ly_tai_nguyen_rung.Models.section1;
using System.ComponentModel.DataAnnotations;

namespace quan_ly_tai_nguyen_rung.ViewModels
{
    public class AnimalFacilityViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Address { get; set; }

        [Required]
        [StringLength(100)]
        public string ContactFace { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string ContactMail { get; set; }

        [Required]
        [StringLength(100)]
        [Phone]
        public string ContactPhone { get; set; }

        [Required]
        public int Labor { get; set; }

        [Required]
        public double Acreage { get; set; }

        public byte[]? ImageAnimalStorage { get; set; }

        [Required]
        public int CommuneId { get; set; } // Lưu giá trị commune được chọn từ dropdown
    }
    
}
