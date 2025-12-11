using System;
using System.Runtime.CompilerServices;

namespace LogLib.Abstractions;

/// <summary>
/// Определяет высокоуровневый интерфейс сервиса логирования,
/// предоставляющий универсальные методы для записи сообщений
/// различного уровня и обработки исключений.
///
/// Интерфейс не привязан к конкретной технологии логирования
/// (NLog, Serilog и т. д.) и служит фасадом, упрощающим использование
/// логирования во всех частях приложения.
///
/// Реализация LogService отвечает за:
/// - формирование структуры LogEntry,
/// - определение категории (General или Device),
/// - передачу записи в ILogWriter.
///
/// Логика UI/Device/General управляется флагом isDeviceLog.
/// </summary>
public interface ILogService
{
  /// <summary>
  /// Логирует информационное сообщение.
  /// </summary>
  /// <param name="message">Текст сообщения.</param>
  /// <param name="isDeviceLog">Если true — сообщение относится к оборудованию; иначе — к общему логу.</param>
  /// <param name="callerFilePath">Путь к файлу, откуда был вызван метод.</param>
  /// <param name="callerLineNumber">Номер строки вызова.</param>
  void Info(
    string message,
    bool isDeviceLog = false,
    [CallerFilePath] string callerFilePath = "",
    [CallerLineNumber] int callerLineNumber = 0);

  /// <summary>
  /// Логирует предупреждение.
  /// </summary>
  void Warn(
    string message,
    bool isDeviceLog = false,
    [CallerFilePath] string callerFilePath = "",
    [CallerLineNumber] int callerLineNumber = 0);

  /// <summary>
  /// Логирует сообщение об ошибке.
  /// </summary>
  void Error(
    string message,
    bool isDeviceLog = false,
    [CallerFilePath] string callerFilePath = "",
    [CallerLineNumber] int callerLineNumber = 0);

  /// <summary>
  /// Логирует исключение с возможностью указать дополнительное сообщение.
  /// Исключение всегда попадает в общий лог и устройство-лог (если isDeviceLog = true).
  /// </summary>
  void Exception(
    Exception ex,
    string? message = null,
    bool isDeviceLog = false,
    [CallerFilePath] string callerFilePath = "",
    [CallerLineNumber] int callerLineNumber = 0);
}