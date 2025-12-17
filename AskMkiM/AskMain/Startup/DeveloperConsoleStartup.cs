using Ask.DevConsole;
using Ask.Shared.Interfaces.Console;

namespace AskMain.Startup;

/// <summary>
/// Стартовый модуль Developer Console.
/// 
/// Отвечает за создание и запуск Developer Console при старте приложения.
/// Является единственной точкой, где основное приложение
/// напрямую взаимодействует с реализацией DevConsole.
/// </summary>
public sealed class DeveloperConsoleStartup
{
  /// <summary>
  /// Экземпляр Developer Console, доступный основному приложению
  /// через интерфейс IDeveloperConsole.
  /// </summary>
  private IDeveloperConsole? _developerConsole;

  /// <summary>
  /// Запускает Developer Console и возвращает интерфейс управления ею.
  /// 
  /// Метод предназначен для вызова из ApplicationStartup
  /// на этапе инициализации приложения.
  /// </summary>
  /// <returns>
  /// Интерфейс управления Developer Console.
  /// </returns>
  public IDeveloperConsole Run()
  {
    if (_developerConsole != null)
      return _developerConsole;

    var consoleHost = new ConsoleHost();
    consoleHost.Start();

    _developerConsole = consoleHost;

    return _developerConsole;
  }
}