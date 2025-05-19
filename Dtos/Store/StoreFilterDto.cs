using System.ComponentModel.DataAnnotations;
using dovandung0300467.Constants;

namespace dovandung0300467.Dtos.Store;

public class StoreFilterDto
{
	public int? Id { get; set; }
	public string Keyword { get; set; } = string.Empty;

	[Range(1, AppConstants.MaxPageSize, ErrorMessage = "Kích thước trang phải từ 1 đến {1}")]
	public int PageSize { get; set; } = AppConstants.DefaultPageSize;

	[Range(0, int.MaxValue, ErrorMessage = "Chỉ số trang phải lớn hơn hoặc bằng 0")]
	public int PageIndex { get; set; } = 0;
}