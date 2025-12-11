namespace LogLib.Abstractions;

/// <summary>
/// Уровни логирования, независимые от конкретной библиотеки (NLog, Serilog и т.д.).
/// Используются в LogEntry и LogService.
/// 
/// Адаптер (например, NLogWriter) самостоятельно преобразует эти уровни
/// в соответствующие уровни конкретного логгера.
/// </summary>
public enum LogLevel
{
  Debug,
  Info,
  Warn,
  Error
}