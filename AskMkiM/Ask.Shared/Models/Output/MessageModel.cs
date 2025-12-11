using Ask.Shared.Enums.Common;

namespace Ask.Shared.Models.Output;

/// <summary>
/// Модель сообщения, используемая исполнителем программ контроля,
/// логикой протокола и пользовательскими сервисами.
/// Не содержит UI-логики и не зависит от платформы.
/// </summary>
public class MessageModel
{
  /// <summary>
  /// Заголовок сообщения (например: "НОРМА", "БРАК", "ИНФО").
  /// </summary>
  public string? Header { get; set; }

  /// <summary>
  /// Основной текст сообщения.
  /// </summary>
  public string? Message { get; set; }

  /// <summary>
  /// Время формирования сообщения (строковый формат — UI решает, как выводить).
  /// </summary>
  public string? Time { get; set; }

  /// <summary>
  /// Тип сообщения (команда, блок, инфо, ошибка, успех).
  /// </summary>
  public MessageType? Type { get; set; }

  /// <summary>
  /// Признак ошибки выполнения.
  /// </summary>
  public bool ExecutionError { get; set; }

  /// <summary>
  /// Можно ли удалять сообщение (используется UI).
  /// </summary>
  public bool CanBeDeleted { get; set; }

  /// <summary>
  /// Признак того, что сообщение относится к устройству.
  /// </summary>
  public bool IsDeviceMessage { get; set; }

  /// <summary>
  /// Уровень отступа перед текстом.
  /// </summary>
  public int IndentLevel { get; set; }

  public MessageModel(
    string? header = null,
    string? message = null,
    MessageType? type = MessageType.Info)
  {
    Header = header;
    Message = message;
    Type = type;
    Time = DateTime.Now.ToString("HH:mm:ss");
  }

  public override string ToString()
    => (!string.IsNullOrEmpty(Header) && !string.IsNullOrEmpty(Message))
      ? $"{Header}: {Message}"
      : Header ?? Message ?? string.Empty;
}
