﻿using AvtomatService.Contracts.Product;

namespace AvtomatService.Contracts.Warehouse;

/// <summary>
/// Модель данных для отображения подробной информации о складе
/// </summary>
public sealed class WarehouseDetailedDto : WarehouseBaseDto
{
    /// <summary>
    /// Список товаров
    /// </summary>
    public List<ProductBaseDto> Products { get; set; } = null!;
}