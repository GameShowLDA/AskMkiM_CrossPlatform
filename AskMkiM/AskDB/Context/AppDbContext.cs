using Microsoft.EntityFrameworkCore;

namespace AskDB.Context;

/// <summary>
/// Контекст базы данных для управления устройствами.
/// </summary>
public partial class AppDbContext : DbContext
{
  /// <summary>
  /// Конструктор, принимающий параметры конфигурации.
  /// </summary>
  public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

  /// <summary>
  /// Fallback-конфигурация — используется только если контекст
  /// создан без передачи DbContextOptions.
  /// В нормальной работе проекта этот блок вызываться не должен.
  /// </summary>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if (!optionsBuilder.IsConfigured)
    {
      // Берём корректный путь напрямую из DataBaseConfig
      string dbPath = DataBaseConfig.DbFilePath;

      optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
  }

  /// <summary>
  /// Настройка моделей БД.
  /// </summary>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    // modelBuilder.ApplyConfiguration(new MeasurementErrorEntityConfiguration());
    // modelBuilder.ApplyConfiguration(new MeasurementErrorRangeEntityConfiguration());
    // modelBuilder.ApplyConfiguration(new UserArchiveRootConfiguration());

    base.OnModelCreating(modelBuilder);
  }
}