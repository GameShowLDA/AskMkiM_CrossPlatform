using Ask.Shared.Interfaces.Console;

namespace AskMain.Startup;

/// <summary>
/// Оркестратор запуска приложения.
/// 
/// Содержит централизованную точку инициализации стартовых компонентов.
/// Используется из App.OnFrameworkInitializationCompleted().
/// </summary>
public sealed class ApplicationStartup
{
  /// <summary>
  /// Экземпляр Developer Console, запущенный при старте приложения.
  /// Хранится для дальнейшего использования (горячие клавиши и т.п.).
  /// </summary>
  private IDeveloperConsole? _developerConsole;

  /// <summary>
  /// Выполняет стартовую инициализацию приложения.
  /// 
  /// Метод предназначен для вызова один раз при запуске приложения.
  /// Здесь последовательно запускаются все стартовые модули приложения.
  /// </summary>
  public void Run()
  {
    var consoleStartup = new DeveloperConsoleStartup();
    _developerConsole = consoleStartup.Run();
  }

  /// <summary>
  /// Возвращает интерфейс управления Developer Console,
  /// запущенной при старте приложения.
  /// </summary>
  public IDeveloperConsole? GetDeveloperConsole()
  {
    return _developerConsole;
  }
}