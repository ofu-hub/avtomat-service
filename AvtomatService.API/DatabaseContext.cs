using AvtomatService.Models;
using Microsoft.EntityFrameworkCore;

namespace AvtomatService.API;

/// <summary>
/// Контекст базы данных
/// </summary>
public sealed class DatabaseContext : DbContext
{
    /// <summary>
    /// Таблица складов
    /// </summary>
    public DbSet<Warehouse> Warehouses { get; set; } = null!;
    
    /// <summary>
    /// Таблица товаров
    /// </summary>
    public DbSet<Product> Products { get; set; } = null!;
    
    /// <summary>
    /// Конструктор по умолчанию
    /// </summary>
    public DatabaseContext() { }

    /// <summary>
    /// Конструктор с авто-миграцией
    /// </summary>
    /// <param name="options">Опции базы данных</param>
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        if (Database.GetPendingMigrations().Any())
            Database.Migrate();
    }

    /// <summary>
    /// Создание сущностей в базе данных на основе моделей
    /// </summary>
    /// <param name="modelBuilder">Конструктор сущностей</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Warehouse>(entity =>
        {
            entity.HasKey(w => w.Id);
            entity.Property(w => w.Title).IsRequired();
            entity.HasMany(w => w.Products).WithOne(p => p.Warehouse).HasForeignKey(p => p.WarehouseId);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Title).IsRequired();
            entity.Property(p => p.Quantity).IsRequired();
            entity.HasOne(p => p.Warehouse).WithMany(w => w.Products).HasForeignKey(p => p.WarehouseId);
        });
    }
}