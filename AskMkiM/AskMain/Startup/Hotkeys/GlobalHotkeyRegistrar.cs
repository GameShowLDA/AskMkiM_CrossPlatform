using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace AskMain.Startup.Hotkeys;

/// <summary>
/// Сервис регистрации горячих клавиш на уровне TopLevel.
///
/// Позволяет обрабатывать нажатия клавиш глобально для конкретного
/// TopLevel (главного окна или корневого визуального контейнера),
/// независимо от текущего фокуса внутри окна.
///
/// Использует только публичный API Avalonia 11.
/// </summary>
public sealed class GlobalHotkeyRegistrar : IDisposable
{
  /// <summary>
  /// TopLevel, к которому привязывается обработка горячих клавиш.
  /// Как правило, это главное окно приложения.
  /// </summary>
  private readonly TopLevel _topLevel;

  /// <summary>
  /// Обработчик события нажатия клавиши.
  /// </summary>
  private readonly EventHandler<KeyEventArgs> _handler;

  /// <summary>
  /// Создаёт и регистрирует глобальный обработчик горячих клавиш
  /// для указанного TopLevel.
  /// </summary>
  /// <param name="topLevel">
  /// TopLevel, в рамках которого будут перехватываться нажатия клавиш.
  /// </param>
  /// <param name="handler">
  /// Обработчик события KeyDown.
  /// </param>
  public GlobalHotkeyRegistrar(
    TopLevel topLevel,
    EventHandler<KeyEventArgs> handler)
  {
    _topLevel = topLevel;
    _handler = handler;

    _topLevel.AddHandler(
      InputElement.KeyDownEvent,
      _handler,
      RoutingStrategies.Tunnel);
  }

  /// <summary>
  /// Удаляет регистрацию глобального обработчика горячих клавиш
  /// и освобождает связанные ресурсы.
  /// </summary>
  public void Dispose()
  {
    _topLevel.RemoveHandler(
      InputElement.KeyDownEvent,
      _handler);
  }
}