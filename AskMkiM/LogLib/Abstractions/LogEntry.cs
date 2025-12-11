
namespace LogLib.Abstractions;

/// <summary>
/// Структура данных, описывающая логируемое сообщение.
///
/// LogEntry создаётся в LogService и передаётся в ILogWriter,
/// который занимается физической записью данных.
///
/// Содержит:
/// - уровень логирования (Info, Warn, Error, Debug),
/// - текст сообщения,
/// - категорию (General или Device),
/// - путь к исходному файлу,
/// - номер строки вызова,
/// - объект исключения, если присутствует.
/// 
/// LogEntry не содержит никакой логики и служит исключительно
/// для транспортировки данных между слоями логирования.
/// Такой подход полностью соответствует принципам SRP и DIP.
/// </summary>
public class LogEntry
{
  /// <summary>
  /// Уровень логирования (Info, Warn, Error, Debug).
  /// </summary>
  public LogLevel Level { get; init; }

  /// <summary>
  /// Текст лог-сообщения.
  /// </summary>
  public string Message { get; init; } = string.Empty;

  /// <summary>
  /// Категория логирования:
  /// - "General" — общий лог,
  /// - "Device" — лог работы с оборудованием.
  /// </summary>
  public string Category { get; init; } = "General";

  /// <summary>
  /// Полный путь к файлу, откуда был вызван метод логирования.
  /// Заполняется на уровне LogService.
  /// </summary>
  public string File { get; init; } = string.Empty;

  /// <summary>
  /// Номер строки вызова в исходном файле.
  /// </summary>
  public int Line { get; init; }

  /// <summary>
  /// Исключение, если оно присутствует.
  /// Может быть null.
  /// </summary>
  public Exception? Exception { get; init; }
}