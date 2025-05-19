using Microsoft.EntityFrameworkCore;
using dovandung0300467.DbContexts;
using dovandung0300467.Dtos.Store;
using dovandung0300467.Dtos.Supplier;
using dovandung0300467.Entities;
using dovandung0300467.Exceptions;
using dovandung0300467.Interfaces;
using dovandung0300467.Dtos;

namespace dovandung0300467.Services;

public class StoreService : IStoreService
{
    private readonly AppDbContext _context;

    public StoreService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<StoreDto> CreateStoreAsync(CreateStoreDto dto)
    {
        if (await _context.Stores.AnyAsync(s => s.Name == dto.Name))
            throw new UserFriendlyException("Tên cửa hàng đã tồn tại.");

        var store = new Store
        {
            Name = dto.Name,
            Address = dto.Address,
            OpeningHour = dto.OpeningHour,
            ClosingHour = dto.ClosingHour
        };

        _context.Stores.Add(store);
        await _context.SaveChangesAsync();

        return new StoreDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address,
            OpeningHour = store.OpeningHour,
            ClosingHour = store.ClosingHour
        };
    }

    public async Task<StoreDto> UpdateStoreAsync(int id, UpdateStoreDto dto)
    {
        var store = await _context.Stores.FindAsync(id)
            ?? throw new UserFriendlyException("Cửa hàng không tồn tại.");

        if (await _context.Stores.AnyAsync(s => s.Name == dto.Name && s.Id != id))
            throw new UserFriendlyException("Tên cửa hàng đã tồn tại.");

        store.Name = dto.Name;
        store.Address = dto.Address;
        store.OpeningHour = dto.OpeningHour;
        store.ClosingHour = dto.ClosingHour;

        await _context.SaveChangesAsync();

        return new StoreDto
        {
            Id = store.Id,
            Name = store.Name,
            Address = store.Address,
            OpeningHour = store.OpeningHour,
            ClosingHour = store.ClosingHour
        };
    }

    public async Task DeleteStoreAsync(int id)
    {
        var store = await _context.Stores.FindAsync(id)
            ?? throw new UserFriendlyException("Cửa hàng không tồn tại.");

        _context.Stores.Remove(store);
        await _context.SaveChangesAsync();
    }

    public async Task<PagedResultDto<StoreDto>> GetStoresAsync(StoreFilterDto filter)
    {
        var query = _context.Stores.AsQueryable();

        if (filter.Id.HasValue)
            query = query.Where(s => s.Id == filter.Id);

        if (!string.IsNullOrWhiteSpace(filter.Keyword))
            query = query.Where(s => s.Name.Contains(filter.Keyword) || s.Address.Contains(filter.Keyword));

        var totalCount = await query.CountAsync();

        var stores = await query
            .Skip(filter.PageIndex * filter.PageSize)
            .Take(filter.PageSize)
            .Select(s => new StoreDto
            {
                Id = s.Id,
                Name = s.Name,
                Address = s.Address,
                OpeningHour = s.OpeningHour,
                ClosingHour = s.ClosingHour
            })
            .ToListAsync();

        return new PagedResultDto<StoreDto>
        {
            Items = stores,
            TotalCount = totalCount,
            PageSize = filter.PageSize,
            PageIndex = filter.PageIndex
        };
    }

    public async Task<List<SupplierDto>> GetTopSuppliersAsync(int storeId)
    {
        var storeExists = await _context.Stores.AnyAsync(s => s.Id == storeId);
        if (!storeExists)
            throw new UserFriendlyException("Cửa hàng không tồn tại.");

        var maxFriendship = await _context.StoreSuppliers
            .Where(ss => ss.StoreId == storeId)
            .MaxAsync(ss => (float?)ss.FriendshipLevel) ?? 0;

        var suppliers = await _context.StoreSuppliers
            .Where(ss => ss.StoreId == storeId && ss.FriendshipLevel == maxFriendship)
            .Select(ss => new SupplierDto
            {
                Name = ss.Supplier.Name,
                PhoneNumber = ss.Supplier.PhoneNumber
            })
            .ToListAsync();

        return suppliers;
    }

    public async Task<List<StoreStatisticsDto>> GetStoreStatisticsAsync()
    {
        var result = await _context.Stores
            .Select(store => new StoreStatisticsDto
            {
                StoreName = store.Name,
                SupplierCount = store.StoreSuppliers.Count,
                AverageFriendshipLevel = store.StoreSuppliers.Any()
                    ? store.StoreSuppliers.Average(ss => ss.FriendshipLevel)
                    : 0
            })
            .ToListAsync();

        return result;
    }
}

