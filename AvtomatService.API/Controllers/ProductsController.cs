using AvtomatService.Contracts.Product;
using AvtomatService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AvtomatService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly DatabaseContext _context;
    private readonly ILogger<ProductsController> _logger;
    
    /// <summary>
    /// Конструктор класса <see cref="WarehousesController"/>
    /// </summary>
    /// <param name="context">Контекст базы данных</param>
    /// <param name="logger">Логгер</param>
    public ProductsController(DatabaseContext context, ILogger<ProductsController> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    /// <summary>
    /// Получение списка всех товаров
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var data = await _context.Products.ToListAsync();
        var products = data.Select(p => new ProductBaseDto()
        {
            Id = p.Id,
            Title = p.Title,
            Quantity = p.Quantity
        }).ToList();
        return Ok(products);
    }

    /// <summary>
    /// Получение информации о конкретном товаре
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [Route("{id:guid}"), HttpGet]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var products = await _context.Products.Where(p => p.Id == id).Select(p => new ProductBaseDto()
        {
            Id = p.Id,
            Title = p.Title,
            Quantity = p.Quantity
        }).FirstOrDefaultAsync();
        
        if (products is null)
            return NotFound();
        
        return Ok(products);
    }
    
    /// <summary>
    /// Изменить количество товара
    /// </summary>
    /// <param name="id">Идентификатор продукта</param>
    /// <param name="quantityChange">Количество</param>
    /// <returns></returns>
    [Route("{id:guid}/changequantity"), HttpPost]
    public async Task<IActionResult> Changequantity([FromRoute] Guid id, [FromBody] int quantityChange)
    {
        var product = await _context.Products.FindAsync(id);
        if (product is not null)
        {
            product.Quantity += quantityChange;
            await _context.SaveChangesAsync();
            return Ok();
        }
        return NotFound();
    }
    
    /// <summary>
    /// Вставить товар
    /// </summary>
    /// <param name="productPutDto">Товар</param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromForm] ProductPutDto productPutDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (productPutDto.Id is null || productPutDto.Id == Guid.Empty)
        {
            var newProduct = new Product()
            {
                Id = new Guid(),
                Title = productPutDto.Title,
                Quantity = productPutDto.Quantity ?? 0
            };
            
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
        
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productPutDto.Id);
        if (product is null)
            return NotFound(productPutDto.Id);

        product.Title = productPutDto.Title;
        product.Quantity = productPutDto.Quantity ?? product.Quantity;
        
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        
        return Ok();
    }
    
    /// <summary>
    /// Удалить товар
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns></returns>
    [Route("{id:guid}"), HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
        
        if (product is null)
            return NotFound();
        
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        
        return NoContent();
    }
}