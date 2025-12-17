using AskDB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AskDB;

/// <summary>
/// Предоставляет фабрику контекста базы данных для инструментов разработки EF Core,
/// обеспечивая создание экземпляра <see cref="AppDbContext"/> во время генерации миграций.
/// </summary>
public class DbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
  public AppDbContext CreateDbContext(string[] args)
  {
    // Получаем корректный путь к БД из DataBaseConfig
    string dbPath = DataBaseConfig.DbFilePath;

    var options = new DbContextOptionsBuilder<AppDbContext>()
      .UseSqlite($"Data Source={dbPath}")
      .Options;

    return new AppDbContext(options);
  }
}