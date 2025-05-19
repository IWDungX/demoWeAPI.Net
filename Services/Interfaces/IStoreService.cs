using dovandung0300467.Dtos;
using dovandung0300467.Dtos.Store;
using dovandung0300467.Dtos.Supplier;

namespace dovandung0300467.Interfaces
{
	public interface IStoreService
	{
		Task<StoreDto> CreateStoreAsync(CreateStoreDto dto);
		Task<StoreDto> UpdateStoreAsync(int id, UpdateStoreDto dto);
		Task DeleteStoreAsync(int id);
		Task<PagedResultDto<StoreDto>> GetStoresAsync(StoreFilterDto filter);
		Task<List<SupplierDto>> GetTopSuppliersAsync(int storeId);
		Task<List<StoreStatisticsDto>> GetStoreStatisticsAsync();
	}
}
