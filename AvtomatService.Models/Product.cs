namespace AvtomatService.Models;

/// <summary>
/// Модель товара
/// </summary>
public class Product
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Идентификатор склада, к которому принадлежит товар
    /// </summary>
    public Guid WarehouseId { get; set; }

    /// <summary>
    /// Склад
    /// </summary>
    public virtual Warehouse Warehouse { get; set; } = null!;
    
    /// <summary>
    /// Количество
    /// </summary>
    public double Quantity { get; set; }
}