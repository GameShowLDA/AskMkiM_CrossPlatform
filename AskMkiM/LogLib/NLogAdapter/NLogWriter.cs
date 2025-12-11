using LogLib.Abstractions;
using NLog;

namespace LogLib.NLogAdapter;

/// <summary>
/// Адаптер для передачи логов в NLog.
/// Получает структуру LogEntry и выполняет реальную запись сообщения.
/// 
/// Этот класс — единственное место, где используется NLog,
/// что соответствует принципу инверсии зависимостей:
/// верхний уровень зависит от абстракций, а не от реализации.
/// 
/// Категория лога определяет имя логгера:
/// - "General" → общий лог
/// - "Device"  → лог работы с устройствами
///
/// Уровень логирования (Info, Warn, Error, Debug)
/// мапится в соответствующий уровень NLog.
/// </summary>
public class NLogWriter : ILogWriter
{
  /// <summary>
  /// Записывает сообщение в NLog.
  /// </summary>
  /// <param name="entry">Данные лог-сообщения.</param>
  public void Write(LogEntry entry)
  {
    var logger = LogManager.GetLogger(entry.Category);

    var nlogLevel = MapLevel(entry.Level);

    // Добавляем источник (файл и строка) в начало сообщения
    var formattedMessage = $"[{entry.File}:{entry.Line}] {entry.Message}";

    if (entry.Exception != null)
    {
      logger.Log(nlogLevel, entry.Exception, formattedMessage);
    }
    else
    {
      logger.Log(nlogLevel, formattedMessage);
    }
  }

  /// <summary>
  /// Преобразование нашего LogLevel в NLog.LogLevel.
  /// </summary>
  private static NLog.LogLevel MapLevel(Abstractions.LogLevel level)
  {
    return level switch
    {
      Abstractions.LogLevel.Debug => NLog.LogLevel.Debug,
      Abstractions.LogLevel.Info  => NLog.LogLevel.Info,
      Abstractions.LogLevel.Warn  => NLog.LogLevel.Warn,
      Abstractions.LogLevel.Error => NLog.LogLevel.Error,
      _ => NLog.LogLevel.Info
    };
  }
}