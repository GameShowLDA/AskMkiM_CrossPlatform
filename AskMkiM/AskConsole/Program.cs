using LogLib;
using LogLib.Abstractions;
using Microsoft.Extensions.DependencyInjection;

// Создаём DI-контейнер
var services = new ServiceCollection();

// Подключаем LogLib
services.AddLogLib();

var provider = services.BuildServiceProvider();

// Получаем ILogService
var log = provider.GetRequiredService<ILogService>();

Console.WriteLine("Hello World from AskConsole!");

// Логируем обычные сообщения
log.Info("Приложение запущено.");
log.Warn("Это предупреждение.");
log.Error("Это ошибка.");

// Логируем исключение
try
{
  throw new InvalidOperationException("Тестовое исключение");
}
catch (Exception ex)
{
  log.Exception(ex, "Произошло исключение при тестировании LogLib");
}

// Тестируем device-лог
log.Info("Команда к устройству отправлена.", isDeviceLog: true);

Console.WriteLine("Логи записаны. Проверь папку ./logs/");

Console.ReadLine();