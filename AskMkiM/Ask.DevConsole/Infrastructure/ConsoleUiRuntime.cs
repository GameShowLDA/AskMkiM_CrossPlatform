using System.Threading;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Ask.DevConsole.Infrastructure;

/// <summary>
/// Инфраструктурный компонент, отвечающий за создание и управление
/// окном Developer Console внутри уже запущенного Avalonia-приложения.
///
/// Класс не инициализирует Avalonia и не создаёт Application.
/// Использует существующий UI Dispatcher основного приложения.
///
/// Предназначен для использования исключительно через ConsoleHost
/// и не предоставляет прямого доступа к UI элементам.
/// </summary>
internal sealed class ConsoleUiRuntime
{
  /// <summary>
  /// Окно Developer Console.
  /// Создаётся и используется исключительно в UI-диспетчере Avalonia.
  /// </summary>
  private Window? _window;

  /// <summary>
  /// Диспетчер UI-потока, в котором работает окно Developer Console.
  /// Используется для потокобезопасного взаимодействия с UI.
  /// </summary>
  private Dispatcher? _dispatcher;

  /// <summary>
  /// Синхронизатор, сигнализирующий о завершении инициализации UI.
  /// Гарантирует, что операции отображения и скрытия окна
  /// не будут выполнены до создания Dispatcher и окна.
  /// </summary>
  private readonly ManualResetEventSlim _initialized = new(false);

  /// <summary>
  /// Признак того, что UI-логика Developer Console запущена
  /// и готова к приёму команд.
  /// </summary>
  public bool IsRunning { get; private set; }

  /// <summary>
  /// Признак того, что окно Developer Console в данный момент
  /// отображается пользователю.
  /// </summary>
  public bool IsVisible { get; private set; }

  /// <summary>
  /// Запускает UI-логику Developer Console.
  ///
  /// Метод вызывается из фонового потока и инициализирует
  /// окно консоли в существующем UI-диспетчере Avalonia.
  /// </summary>
  public void Run()
  {
    IsRunning = true;

    Dispatcher.UIThread.Post(InitializeUi);

    _initialized.Wait();
  }

  /// <summary>
  /// Инициализирует UI-компоненты Developer Console.
  ///
  /// Метод выполняется строго в UI-диспетчере Avalonia.
  /// Здесь создаётся окно консоли и настраиваются его параметры.
  /// </summary>
  private void InitializeUi()
  {
    _dispatcher = Dispatcher.UIThread;

    _window = new ConsoleWindow
    {
      Title = "Developer Console",
      Width = 1200,
      Height = 800,
      ShowInTaskbar = false
    };

    _window.Closed += (_, _) => IsVisible = false;

    _initialized.Set();
  }

  /// <summary>
  /// Отображает окно Developer Console пользователю.
  ///
  /// Метод потокобезопасен и может вызываться из любого потока.
  /// Если окно уже отображено, повторный вызов не имеет эффекта.
  /// </summary>
  public void Show()
  {
    _initialized.Wait();

    _dispatcher!.Post(() =>
    {
      if (_window == null)
        return;

      if (!_window.IsVisible)
      {
        _window.Show();
        _window.Activate();
      }

      IsVisible = true;
    });
  }

  /// <summary>
  /// Скрывает окно Developer Console, не уничтожая UI-логику.
  ///
  /// Метод потокобезопасен и может вызываться из любого потока.
  /// UI-диспетчер и окно остаются активными.
  /// </summary>
  public void Hide()
  {
    _dispatcher?.Post(() =>
    {
      if (_window == null)
        return;

      _window.Hide();
      IsVisible = false;
    });
  }

  /// <summary>
  /// Переключает видимость окна Developer Console.
  ///
  /// Если окно отображается — оно будет скрыто.
  /// Если окно скрыто — будет показано.
  /// </summary>
  public void Toggle()
  {
    if (IsVisible)
      Hide();
    else
      Show();
  }

  /// <summary>
  /// Запрашивает корректное завершение работы Developer Console.
  ///
  /// Закрывает окно консоли и переводит компонент
  /// в неактивное состояние без остановки Avalonia runtime.
  /// </summary>
  public void RequestStop()
  {
    _dispatcher?.Post(() =>
    {
      _window?.Close();
      IsRunning = false;
    });
  }
}
