using Avalonia.Input;

namespace Ask.Shared.Data.Hotkeys;

/// <summary>
/// Централизованное хранилище всех горячих клавиш приложения.
/// Содержит ТОЛЬКО описания сочетаний клавиш, без логики обработки.
/// </summary>
public static class AppHotkeys
{
  /// <summary>
  /// Горячая клавиша переключения Developer Console.
  /// </summary>
  public static KeyGesture DeveloperConsoleToggle { get; } =
    OperatingSystem.IsMacOS()
      ? new KeyGesture(Key.D, KeyModifiers.Meta | KeyModifiers.Shift)
      : new KeyGesture(Key.Oem3, KeyModifiers.Control);

  /// <summary>
  /// Горячая клавиша скрытия Developer Console.
  /// </summary>
  public static KeyGesture DeveloperConsoleHide { get; } =
    new KeyGesture(Key.Escape);

}