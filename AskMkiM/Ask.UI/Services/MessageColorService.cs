using Ask.Shared.Enums.Common;

namespace Ask.UI.Services;

/// <summary>
/// Сервис, отвечающий за преобразование логических типов сообщений
/// в ключи ресурсов цветов пользовательского интерфейса.
///
/// Он НЕ хранит реальные цвета —
/// только выдаёт ключи для привязки к теме.
/// </summary>
public static class MessageColorService
{
  /// <summary>
  /// Возвращает ключ ресурса цвета,
  /// связанного с типом сообщения.
  /// Тема UI самостоятельно предоставляет нужные цвета.
  /// </summary>
  /// <param name="type">Тип сообщения</param>
  /// <returns>Имя ресурса цвета</returns>
  public static string GetColorResourceKey(MessageType type)
  {
    return type switch
    {
      MessageType.Success      => "Color.Message.Success",
      MessageType.Error        => "Color.Message.Error",
      MessageType.Command      => "Color.Message.Command",
      MessageType.CommandBlock => "Color.Message.CommandBlock",
      _                        => "Color.Message.Default"
    };
  }

  /// <summary>
  /// Возвращает строковый префикс состояния,
  /// например [НОРМА], [БРАК].
  /// </summary>
  public static string GetPrefix(MessageType type)
  {
    return type switch
    {
      MessageType.Success => "[НОРМА]",
      MessageType.Error   => "[БРАК]",
      _ => string.Empty
    };
  }
}
