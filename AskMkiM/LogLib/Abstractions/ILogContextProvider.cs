namespace LogLib.Abstractions;

/// <summary>
/// Интерфейс, предоставляющий информацию о месте вызова лог-сообщения.
/// Используется для получения полного пути к файлу и номера строки,
/// откуда был вызван метод логирования.
///
/// Реализация (например, CallerInfoContextProvider) использует
/// атрибуты компилятора [CallerFilePath] и [CallerLineNumber].
/// </summary>
public interface ILogContextProvider
{
  /// <summary>
  /// Возвращает путь к файлу и номер строки вызова.
  /// </summary>
  /// <param name="file">Путь к файлу, автоматически подставляется компилятором.</param>
  /// <param name="line">Номер строки, автоматически подставляется компилятором.</param>
  (string file, int line) GetCallerInfo(
    string file = "",
    int line = 0);
}