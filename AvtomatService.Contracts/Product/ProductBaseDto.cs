namespace AvtomatService.Contracts.Product;

/// <summary>
/// Модель данных для отображения товара
/// </summary>
public class ProductBaseDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Title { get; set; } = string.Empty;
}