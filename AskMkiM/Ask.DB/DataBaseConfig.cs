using AskDB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LogLib;
using LogLib.Abstractions;

namespace AskDB
{
  /// <summary>
  /// Конфигурация локальной SQLite-базы данных, включая путь к файлу,
  /// создание контекста и применение миграций при запуске приложения.
  /// </summary>
  public static class DataBaseConfig
  {
    /// <summary>
    /// Полный путь к файлу базы данных (_config.db), лежащему в папке Resources
    /// рядом с исполняемым файлом. Папка создаётся автоматически.
    /// </summary>
    public static string DbFilePath
    {
      get
      {
        string baseDir = AppContext.BaseDirectory;
        string resourcesDir = Path.Combine(baseDir, "Resources");

        Directory.CreateDirectory(resourcesDir);
        return Path.Combine(resourcesDir, "_config.db");
      }
    }

    /// <summary>
    /// Возвращает новые параметры конфигурации EF Core, привязанные к SQLite-файлу.
    /// </summary>
    private static DbContextOptions<AppDbContext> CreateOptions() =>
      new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite($"Data Source={DbFilePath}")
        .Options;

    /// <summary>
    /// Создаёт новый экземпляр DbContext.
    /// </summary>
    public static AppDbContext CreateContext() =>
      new AppDbContext(CreateOptions());

    /// <summary>
    /// Инициализирует БД: создаёт файл при отсутствии и применяет миграции.
    /// </summary>
    public static async Task InitializeAsync()
    {
      // Поднимаем DI только для логирования
      var services = new ServiceCollection()
        .AddLogLib()
        .BuildServiceProvider();

      var log = services.GetRequiredService<ILogService>();

      try
      {
        using var context = CreateContext();

        // ❗ EF Core сам создаст файл БД, если его нет.
        // Не использовать EnsureCreated! Только MigrateAsync.
        await context.Database.MigrateAsync();

        log.Info("База данных успешно инициализирована и миграции применены.");
      }
      catch (Exception ex)
      {
        log.Exception(ex, "Ошибка инициализации базы данных.");
      }
    }
  }
}