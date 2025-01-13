using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace quan_ly_tai_nguyen_rung.ViewModel
{
    public class FluctuationViewModel
    {
        public int Id { get; set; } // Để chỉnh sửa chức năng

        [Required(ErrorMessage = "Năm là bắt buộc.")]
        [Range(1900, 2100, ErrorMessage = "Năm phải nằm trong khoảng từ 1900 đến 2100.")]
        public int Year { get; set; }
        [Required]
        public bool Type { get; set; } 
        [Required(ErrorMessage = "Tháng là bắt buộc.")]
        [Range(1, 12, ErrorMessage = "Tháng phải nằm trong khoảng từ 1 đến 12.")]
        public int Month { get; set; }

        [Required(ErrorMessage = "Quý là bắt buộc.")]
        [Range(1, 4, ErrorMessage = "Quý phải nằm trong khoảng từ 1 đến 4.")]
        public int Quarter { get; set; }

        [Required(ErrorMessage = "Động vật là bắt buộc.")]
        public int AnimalId { get; set; }

        [Required(ErrorMessage = "Sự thay đổi số lượng là bắt buộc.")]
        [Range(0, int.MaxValue, ErrorMessage = "Sự thay đổi số lượng phải là số không âm.")]
        public int QuantityChange { get; set; }

        [StringLength(500, ErrorMessage = "Lý do phải dài tối đa 500 ký tự.")]
        public string Reason { get; set; }
    }
}
