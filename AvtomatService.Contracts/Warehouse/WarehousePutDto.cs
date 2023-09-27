using System.ComponentModel.DataAnnotations;

namespace AvtomatService.Contracts.Warehouse;

/// <summary>
/// Модель данных для создания склада
/// </summary>
public class WarehousePutDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid? Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    [Required(ErrorMessage = "Не указано название склада!")]
    public string Title { get; set; } = string.Empty;
}