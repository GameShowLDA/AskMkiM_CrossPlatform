using System;
using Ask.Shared.Data.Hotkeys;
using Ask.Shared.Interfaces.Console;
using Avalonia.Input;

namespace AskMain.Startup.Hotkeys;

/// <summary>
/// Обработчик горячей клавиши Developer Console.
/// </summary>
public sealed class DeveloperConsoleHotkey
{
  private readonly IDeveloperConsole _console;

  /// <summary>
  /// Создаёт обработчик горячей клавиши Developer Console.
  /// </summary>
  public DeveloperConsoleHotkey(IDeveloperConsole console)
  {
    _console = console;
  }

  /// <summary>
  /// Обрабатывает нажатие клавиши и переключает видимость консоли.
  /// </summary>
  public void OnKeyDown(object? sender, KeyEventArgs e)
  {
    Console.WriteLine($"Key: {e.Key}, Modifiers: {e.KeyModifiers}");

    if (!AppHotkeys.DeveloperConsoleToggle.Matches(e))
      return;

    _console.Toggle();
    e.Handled = true;
  }

  /// <summary>
  /// Проверяет, соответствует ли событие комбинации
  /// переключения Developer Console.
  /// </summary>
  private static bool IsToggleCombination(KeyEventArgs e)
  {
    if (e.Key != Key.Oem3)
      return false;

    if (OperatingSystem.IsMacOS())
      return e.KeyModifiers.HasFlag(KeyModifiers.Meta);

    return e.KeyModifiers.HasFlag(KeyModifiers.Control);
  }
}