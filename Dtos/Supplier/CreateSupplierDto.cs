using System.ComponentModel.DataAnnotations;

namespace dovandung0300467.Dtos.Supplier;

public class CreateSupplierDto
{
    private string _name = string.Empty;
    private string _address = string.Empty;
    private string _phoneNumber = string.Empty;

    [Required(ErrorMessage = "Tên nhà cung cấp là bắt buộc")]
    [StringLength(100, ErrorMessage = "Tên không được vượt quá 100 ký tự")]
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

    [Required(ErrorMessage = "Số điện thoại là bắt buộc")]
    [StringLength(15, ErrorMessage = "Số điện thoại không được vượt quá 15 ký tự")]
    public string PhoneNumber
    {
        get => _phoneNumber;
        set => _phoneNumber = value?.Trim() ?? string.Empty;
    }
}