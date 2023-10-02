namespace AvtomatService.Models;

/// <summary>
/// Модель кладовщика
/// </summary>
public class Storekeeper
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; } = string.Empty;
}