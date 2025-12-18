using Ask.Shared.Interfaces.Console;
using AskMain.Startup.Hotkeys;
using Avalonia.Controls;

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

  private GlobalHotkeyRegistrar? _hotkeyRegistrar;
  
  /// <summary>
  /// Выполняет стартовую инициализацию приложения.
  /// 
  /// Метод предназначен для вызова один раз при запуске приложения.
  /// Здесь последовательно запускаются все стартовые модули приложения.
  /// </summary>
  public void Run(TopLevel topLevel)
  {
    var consoleStartup = new DeveloperConsoleStartup();
    _developerConsole = consoleStartup.Run();
    
    var hotkey = new DeveloperConsoleHotkey(_developerConsole);
    _hotkeyRegistrar = new GlobalHotkeyRegistrar(
      topLevel,
      hotkey.OnKeyDown);
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