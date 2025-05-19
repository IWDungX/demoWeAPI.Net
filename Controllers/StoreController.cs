using Microsoft.AspNetCore.Mvc;
using dovandung0300467.Dtos.Store;
using dovandung0300467.Interfaces;

namespace dovandung0300467.Controllers;

[ApiController]
[Route("api/stores")]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto dto)
    {
        var result = await _storeService.CreateStoreAsync(dto);
        return CreatedAtAction(nameof(GetStore), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStore(int id, [FromBody] UpdateStoreDto dto)
    {
        var result = await _storeService.UpdateStoreAsync(id, dto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStore(int id)
    {
        await _storeService.DeleteStoreAsync(id);
        return NoContent();
    }

    // API GetStore dùng cho CreatedAtAction
    [HttpGet("{id}")]
    public async Task<IActionResult> GetStore(int id)
    {
        var store = await _storeService.GetStoresAsync(new StoreFilterDto { Id = id });
        if (!store.Items.Any())
            return NotFound();
        return Ok(store.Items.First());
    }

    [HttpGet]
    public async Task<IActionResult> GetStores([FromQuery] StoreFilterDto filter)
    {
        var result = await _storeService.GetStoresAsync(filter);
        return Ok(result);
    }

    [HttpGet("{storeId}/top-suppliers")]
    public async Task<IActionResult> GetTopSuppliers(int storeId)
    {
        var result = await _storeService.GetTopSuppliersAsync(storeId);
        return Ok(result);
    }

    [HttpGet("statistics")]
    public async Task<IActionResult> GetStatistics()
    {
        var result = await _storeService.GetStoreStatisticsAsync();
        return Ok(result);
    }
}