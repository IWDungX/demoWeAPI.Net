using System.ComponentModel.DataAnnotations;

namespace dovandung0300467.Dtos.Store
{
    public class UpdateStoreDto
    {
        private string _name = string.Empty;
        private string _address = string.Empty;

        [Required(ErrorMessage = "Tên cửa hàng là bắt buộc")]
        [StringLength(100, ErrorMessage = "Tên cửa hàng không được vượt quá 100 ký tự")]
        public string Name
        {
            get => _name;
            set => _name = value?.Trim() ?? string.Empty;
        }

        [Required(ErrorMessage = "Địa chỉ là bắt buộc")]
        [StringLength(200, ErrorMessage = "Địa chỉ không được vượt quá 200 ký tự")]
        public string Address
        {
            get => _address;
            set => _address = value?.Trim() ?? string.Empty;
        }

        [Required(ErrorMessage = "Giờ mở cửa là bắt buộc")]
        public TimeSpan OpeningHour { get; set; }

        [Required(ErrorMessage = "Giờ đóng cửa là bắt buộc")]
        public TimeSpan ClosingHour { get; set; }
    }
}
