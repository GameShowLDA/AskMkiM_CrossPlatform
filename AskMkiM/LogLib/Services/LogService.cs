using LogLib.Abstractions;
using System;
using System.Runtime.CompilerServices;

namespace LogLib.Services;

/// <summary>
/// Реализация высокоуровневого сервиса логирования.
/// Отвечает за формирование структуры LogEntry и передачу её
/// в механизм записи (ILogWriter).
///
/// LogService не знает ничего о конкретной библиотеке логирования
/// (NLog, Serilog и т.д.), соблюдая принципы инверсии зависимостей.
///
/// Категория определяется через флаг isDeviceLog:
/// - false → "General"
/// - true → "Device"
///
/// Полный путь к исходному файлу и номер строки автоматически
/// подставляются компилятором.
/// </summary>
public class LogService : ILogService
{
    private readonly ILogWriter _writer;
    private readonly ILogContextProvider _context;

    /// <summary>
    /// Создаёт экземпляр сервиса логирования.
    /// </summary>
    public LogService(ILogWriter writer, ILogContextProvider context)
    {
        _writer = writer;
        _context = context;
    }

    /// <summary>
    /// Логирует информационное сообщение.
    /// </summary>
    public void Info(
        string message,
        bool isDeviceLog = false,
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Write(
            message,
            LogLevel.Info,
            isDeviceLog,
            callerFilePath,
            callerLineNumber);
    }

    /// <summary>
    /// Логирует предупреждение.
    /// </summary>
    public void Warn(
        string message,
        bool isDeviceLog = false,
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Write(
            message,
            LogLevel.Warn,
            isDeviceLog,
            callerFilePath,
            callerLineNumber);
    }

    /// <summary>
    /// Логирует ошибку.
    /// </summary>
    public void Error(
        string message,
        bool isDeviceLog = false,
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Write(
            message,
            LogLevel.Error,
            isDeviceLog,
            callerFilePath,
            callerLineNumber);
    }

    /// <summary>
    /// Логирует исключение с дополнительным сообщением или без него.
    /// Исключение всегда будет записано в общий лог,
    /// а также в Device-лог, если указан флаг isDeviceLog.
    /// </summary>
    public void Exception(
        Exception ex,
        string? message = null,
        bool isDeviceLog = false,
        [CallerFilePath] string callerFilePath = "",
        [CallerLineNumber] int callerLineNumber = 0)
    {
        Write(
            message ?? ex.Message,
            LogLevel.Error,
            isDeviceLog,
            callerFilePath,
            callerLineNumber,
            ex);
    }

    /// <summary>
    /// Создаёт объект LogEntry и передаёт его writer'у.
    /// </summary>
    private void Write(
        string message,
        LogLevel level,
        bool isDeviceLog,
        string file,
        int line,
        Exception? exception = null)
    {
        var entry = new LogEntry
        {
            Level = level,
            Message = message,
            Category = isDeviceLog ? "Device" : "UI",
            File = file,
            Line = line,
            Exception = exception
        };

        _writer.Write(entry);
    }
}
