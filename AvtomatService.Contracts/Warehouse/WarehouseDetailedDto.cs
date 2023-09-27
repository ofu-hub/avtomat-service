using AvtomatService.Contracts.Product;

namespace AvtomatService.Contracts.Warehouse;

/// <summary>
/// Модель данных для отображения подробной информации о складе
/// </summary>
public class WarehouseDetailedDto : WarehouseBaseDto
{
    /// <summary>
    /// Список товаров
    /// </summary>
    public virtual List<ProductBaseDto> Products { get; set; } = new();
}