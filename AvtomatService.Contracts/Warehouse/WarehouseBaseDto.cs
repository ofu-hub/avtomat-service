namespace AvtomatService.Contracts.Warehouse;

/// <summary>
/// Модель данных для отображения склада
/// </summary>
public class WarehouseBaseDto
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