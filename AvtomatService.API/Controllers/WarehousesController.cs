using AvtomatService.Contracts.Product;
using AvtomatService.Contracts.Warehouse;
using AvtomatService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtomatService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehousesController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly ILogger<WarehousesController> _logger;
    
    /// <summary>
    /// Конструктор класса <see cref="WarehousesController"/>
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    /// <param name="logger">Логгер</param>
    public WarehousesController(DatabaseContext context, ILogger<WarehousesController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    /// <summary>
    /// Получение списка всех складов
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data =  await _context.Warehouses
            .Include(w => w.Products)
            .ToListAsync();
        var warehouses = data.Select(w => new WarehouseDetailedDto()
        {
            Id = w.Id,
            Title = w.Title,
            Products = w.Products.Select(p => new ProductBaseDto()
            {
                Id = p.Id,
                Title = p.Title,
                Quantity = p.Quantity
            }).ToList()
        }).ToList();
        return Ok(warehouses);
    }
    
    /// <summary>
    /// Получение информации о конкретном складе
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [Route("{id:guid}"), HttpGet]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var warehouse = await _context.Warehouses.Where(w => w.Id == id).Select(w => new WarehouseDetailedDto()
        {
            Id = w.Id,
            Title = w.Title,
            Products = w.Products.Select(p => new ProductBaseDto()
            {
                Id = p.Id,
                Title = p.Title,
                Quantity = p.Quantity
            }).ToList()
        }).FirstOrDefaultAsync();
        
        if (warehouse is null)
            return NotFound();
        
        return Ok(warehouse);
    }

    /// <summary>
    /// Вставить склад
    /// </summary>
    /// <param name="warehousePutDto">Склад</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromForm] WarehousePutDto warehousePutDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (warehousePutDto.Id is null || warehousePutDto.Id == Guid.Empty)
        {
            var newWarehouse = new Warehouse()
            {
                Id = new Guid(),
                Title = warehousePutDto.Title,
                Products = new List<Product>()
            };
            
            await _context.Warehouses.AddAsync(newWarehouse);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        var warehouse = await _context.Warehouses.Include(warehouse => warehouse.Products).FirstOrDefaultAsync(x => x.Id == warehousePutDto.Id);
        if (warehouse is null)
            return NotFound(warehousePutDto.Id);

        warehouse.Title = warehousePutDto.Title;
        
        _context.Warehouses.Update(warehouse);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
    /// <summary>
    /// Удаление склада
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [Route("{id:guid}"), HttpDelete]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        var warehouse = await _context.Warehouses.FirstOrDefaultAsync(x => x.Id == id);
        
        if (warehouse is null)
            return NotFound();
        
        _context.Warehouses.Remove(warehouse);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}