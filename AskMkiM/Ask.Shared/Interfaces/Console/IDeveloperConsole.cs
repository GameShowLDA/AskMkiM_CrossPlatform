namespace Ask.Shared.Interfaces.Console;

/// <summary>
/// Базовый контракт управления Developer Console.
/// Отвечает только за жизненный цикл и видимость консоли.
/// Не содержит UI- и платформо-зависимых деталей.
/// </summary>
public interface IDeveloperConsole
{
  /// <summary>
  /// Инициализирует и запускает Developer Console.
  /// Повторный вызов не должен приводить к повторной инициализации.
  /// </summary>
  void Start();

  /// <summary>
  /// Останавливает Developer Console и освобождает ресурсы.
  /// </summary>
  void Stop();

  /// <summary>
  /// Показывает окно Developer Console.
  /// </summary>
  void Show();

  /// <summary>
  /// Скрывает окно Developer Console.
  /// </summary>
  void Hide();

  /// <summary>
  /// Переключает видимость Developer Console.
  /// </summary>
  void Toggle();

  /// <summary>
  /// Признак того, что Developer Console инициализирована и запущена.
  /// </summary>
  bool IsRunning { get; }

  /// <summary>
  /// Признак того, что окно Developer Console в данный момент отображается.
  /// </summary>
  bool IsVisible { get; }
}
