namespace AvtomatService.Models;

/// <summary>
/// Модель склада
/// </summary>
public class Warehouse
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
    /// Список товаров
    /// </summary>
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}