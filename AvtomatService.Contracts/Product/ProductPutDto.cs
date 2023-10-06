using System.ComponentModel.DataAnnotations;

namespace AvtomatService.Contracts.Product;

/// <summary>
/// Модель данных для создания товара
/// </summary>
public class ProductPutDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Required(ErrorMessage = "Не указано название товара!")]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор склада, к которому принадлежит товар
    /// </summary>
    [Required(ErrorMessage = "Не выбран склад, где будет храниться товар!")]
    public Guid WarehouseId { get; set; }
    
    /// <summary>
    /// Количество товара
    /// </summary>
    public double? Quantity { get; set; }
}