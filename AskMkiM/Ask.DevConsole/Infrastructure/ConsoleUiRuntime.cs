using System;
using System.Threading;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Threading;

namespace Ask.DevConsole.Infrastructure;

/// <summary>
/// Инфраструктурный компонент, инкапсулирующий запуск Avalonia Developer Console
/// в отдельном UI-потоке с собственным Dispatcher.
///
/// Класс полностью изолирует UI-логику консоли от основного приложения
/// и не допускает прямого доступа к элементам Avalonia извне.
/// </summary>
internal sealed class ConsoleUiRuntime
{
  /// <summary>
  /// Главное окно Developer Console.
  /// Создаётся и используется исключительно внутри UI-потока консоли.
  /// </summary>
  private Window? _window;

  /// <summary>
  /// Диспетчер UI-потока Developer Console.
  /// Используется для безопасного выполнения операций с UI
  /// из внешних потоков.
  /// </summary>
  private Dispatcher? _dispatcher;

  /// <summary>
  /// Синхронизатор, сигнализирующий о завершении инициализации UI.
  /// Гарантирует, что операции Show/Hide не будут выполнены
  /// до создания окна и Dispatcher.
  /// </summary>
  private readonly ManualResetEventSlim _initialized = new(false);

  /// <summary>
  /// Признак того, что UI-поток Developer Console запущен
  /// и Avalonia Application находится в состоянии выполнения.
  /// </summary>
  public bool IsRunning { get; private set; }

  /// <summary>
  /// Признак того, что окно Developer Console в данный момент отображается пользователю.
  /// </summary>
  public bool IsVisible { get; private set; }

  /// <summary>
  /// Точка входа UI-потока Developer Console.
  /// Метод должен вызываться исключительно из отдельного фонового потока.
  ///
  /// Инициализирует Avalonia Application и запускает собственный
  /// Dispatcher UI-потока.
  /// </summary>
  public void Run()
  {
    IsRunning = true;

    BuildAvaloniaApp()
      .Start(AppMain, null);

    IsRunning = false;
  }

  /// <summary>
  /// Основная функция Avalonia Application,
  /// выполняемая внутри UI-потока Developer Console.
  ///
  /// Здесь создаётся Dispatcher, инициализируется окно консоли
  /// и запускается цикл обработки сообщений UI.
  /// </summary>
  /// <param name="app">Экземпляр Avalonia Application.</param>
  /// <param name="args">Аргументы командной строки (не используются).</param>
  private void AppMain(Application app, string[]? args)
  {
    _dispatcher = Dispatcher.UIThread;

    _window = new ConsoleWindow
    {
      Title = "Developer Console",
      Width = 1200,
      Height = 800,
      ShowInTaskbar = false
    };

    _window.Closed += (_, _) => { IsVisible = false; };

    _initialized.Set();
    app.Run(_window);
  }

  /// <summary>
  /// Делает окно Developer Console видимым для пользователя.
  /// Метод потокобезопасен и может вызываться из любого потока.
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
  /// Скрывает окно Developer Console, не уничтожая UI-поток.
  /// Метод потокобезопасен и может вызываться из любого потока.
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
  /// Если окно отображается — оно будет скрыто,
  /// если скрыто — показано.
  /// </summary>
  public void Toggle()
  {
    if (IsVisible)
      Hide();
    else
      Show();
  }

  /// <summary>
  /// Запрашивает корректное завершение UI-потока Developer Console.
  /// Закрывает окно консоли, что приводит к завершению
  /// цикла обработки сообщений Avalonia.
  /// </summary>
  public void RequestStop()
  {
    _dispatcher?.Post(() =>
    {
      _window?.Close();
    });
  }

  /// <summary>
  /// Создаёт и настраивает AppBuilder для Avalonia Application,
  /// используемой Developer Console.
  /// </summary>
  /// <returns>Сконфигурированный экземпляр AppBuilder.</returns>
  private static AppBuilder BuildAvaloniaApp()
  {
    return AppBuilder
      .Configure<App>()
      .UsePlatformDetect()
      .LogToTrace();
  }
}