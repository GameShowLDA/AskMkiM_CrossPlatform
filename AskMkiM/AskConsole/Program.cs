using AskDB;
using LogLib;
using LogLib.Abstractions;
using Microsoft.Extensions.DependencyInjection;

// Создаём DI-контейнер
var services = new ServiceCollection();

// Подключаем LogLib
services.AddLogLib();

var provider = services.BuildServiceProvider();
var log = provider.GetRequiredService<ILogService>();

Console.WriteLine("Hello World from AskConsole!");

// ============================
//   ИНИЦИАЛИЗАЦИЯ БАЗЫ ДАННЫХ
// ============================

try
{
  log.Info("Инициализация базы данных начата...");
  await DataBaseConfig.InitializeAsync();
  log.Info("Инициализация базы данных завершена успешно.");
}
catch (Exception ex)
{
  log.Exception(ex, "Ошибка при инициализации базы данных");
}

Console.WriteLine("Готово. Нажмите Enter для выхода.");
Console.ReadLine();