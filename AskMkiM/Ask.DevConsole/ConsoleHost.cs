using System;
using System.Threading;
using Ask.DevConsole.Infrastructure;
using Ask.Shared.Interfaces.Console;

namespace Ask.DevConsole;

/// <summary>
/// Управляющий компонент Developer Console.
/// 
/// ConsoleHost является единственной публичной точкой входа для основного приложения.
/// Он управляет жизненным циклом консоли, создаёт отдельный UI-поток и предоставляет
/// API для управления видимостью окна.
///
/// Важно: ConsoleHost не раскрывает наружу типы Avalonia (Window/Dispatcher и т.п.).
/// Основное приложение взаимодействует только через интерфейс IDeveloperConsole.
/// </summary>
public sealed class ConsoleHost : IDeveloperConsole, IDisposable
{
  /// <summary>
  /// Объект синхронизации для защиты жизненного цикла (Start/Stop) от гонок.
  /// </summary>
  private readonly object _sync = new();

  /// <summary>
  /// Реализация UI-рантайма консоли, содержащая Avalonia-зависимости.
  /// Создаётся при запуске и уничтожается при остановке.
  /// </summary>
  private ConsoleUiRuntime? _runtime;

  /// <summary>
  /// UI-поток Developer Console.
  /// Создаётся при Start() и завершается при Stop().
  /// </summary>
  private Thread? _uiThread;

  /// <summary>
  /// Признак того, что ConsoleHost находится в состоянии Dispose.
  /// Используется для защиты от вызовов после освобождения ресурсов.
  /// </summary>
  private bool _isDisposed;

  /// <summary>
  /// Признак того, что Developer Console запущена (UI-поток жив и рантайм активен).
  /// </summary>
  public bool IsRunning
  {
    get
    {
      lock (_sync)
        return _runtime?.IsRunning ?? false;
    }
  }

  /// <summary>
  /// Признак того, что окно Developer Console в данный момент отображается.
  /// </summary>
  public bool IsVisible
  {
    get
    {
      lock (_sync)
        return _runtime?.IsVisible ?? false;
    }
  }

  /// <summary>
  /// Инициализирует и запускает Developer Console.
  /// Повторный вызов не приводит к повторной инициализации.
  /// </summary>
  public void Start()
  {
    lock (_sync)
    {
      ThrowIfDisposed();

      if (_runtime is { IsRunning: true })
        return;

      _runtime = new ConsoleUiRuntime();

      _uiThread = new Thread(_runtime.Run)
      {
        IsBackground = true,
        Name = "DeveloperConsole.UI"
      };

      _uiThread.Start();
    }
  }

  /// <summary>
  /// Останавливает Developer Console и освобождает ресурсы.
  /// Метод блокирует вызывающий поток до завершения UI-потока консоли.
  /// </summary>
  public void Stop()
  {
    Thread? threadToJoin;

    lock (_sync)
    {
      if (_runtime is null)
        return;

      /// <summary>
      /// Запрашиваем завершение UI-рантайма.
      /// </summary>
      _runtime.RequestStop();

      /// <summary>
      /// Запоминаем ссылку на поток, чтобы выполнить Join вне lock.
      /// </summary>
      threadToJoin = _uiThread;
    }

    /// <summary>
    /// Дожидаемся завершения UI-потока без удержания блокировки.
    /// </summary>
    threadToJoin?.Join();

    lock (_sync)
    {
      _runtime = null;
      _uiThread = null;
    }
  }

  /// <summary>
  /// Показывает окно Developer Console.
  /// Метод безопасен для вызова из любого потока.
  /// </summary>
  public void Show()
  {
    lock (_sync)
    {
      ThrowIfDisposed();
      _runtime?.Show();
    }
  }

  /// <summary>
  /// Скрывает окно Developer Console.
  /// Метод безопасен для вызова из любого потока.
  /// </summary>
  public void Hide()
  {
    lock (_sync)
    {
      ThrowIfDisposed();
      _runtime?.Hide();
    }
  }

  /// <summary>
  /// Переключает видимость окна Developer Console.
  /// </summary>
  public void Toggle()
  {
    lock (_sync)
    {
      ThrowIfDisposed();
      _runtime?.Toggle();
    }
  }

  /// <summary>
  /// Освобождает ресурсы ConsoleHost.
  /// Автоматически останавливает консоль, если она запущена.
  /// </summary>
  public void Dispose()
  {
    lock (_sync)
    {
      if (_isDisposed)
        return;

      _isDisposed = true;
    }

    Stop();
  }

  /// <summary>
  /// Проверяет состояние Dispose и выбрасывает исключение,
  /// если объект уже освобождён.
  /// </summary>
  private void ThrowIfDisposed()
  {
    if (_isDisposed)
      throw new ObjectDisposedException(nameof(ConsoleHost));
  }
}
