using Ask.Shared.Enums.Errors;

namespace Ask.Shared.Models.Errors
{
  /// <summary>
  /// Представляет информацию об ошибке, возникшей при выполнении
  /// программы контроля или обработке протокола. Содержит данные
  /// о строке, команде, описании ошибки и сопутствующих измерениях.
  /// </summary>
  public class ErrorItem
  {
    /// <summary>
    /// Номер строки исходного скрипта или протокола,
    /// в которой возникла ошибка.
    /// </summary>
    public int SourceLineNumber { get; set; }

    /// <summary>
    /// Номер строки после форматирования или вывода,
    /// используемый для отображения пользователю.
    /// </summary>
    public int FormattedLineNumber { get; set; }

    /// <summary>
    /// Команда, при выполнении которой произошла ошибка.
    /// </summary>
    public string Command { get; set; } = string.Empty;

    /// <summary>
    /// Текстовое описание ошибки, понятное пользователю или оператору.
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Результат измерения или сопутствующее значение,
    /// которое могло привести к ошибке.
    /// </summary>
    public string MeasureResult { get; set; } = string.Empty;

    /// <summary>
    /// Код ошибки, если она соответствует известному типу или категории.
    /// </summary>
    public ErrorCode? Code { get; set; }
  }
}