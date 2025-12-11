using System.Runtime.CompilerServices;
using Ask.Shared.Models.Output;

namespace Ask.Shared.Interfaces.Execution.Output;

/// <summary>
/// Определяет функциональность сервиса вывода сообщений пользователю,
/// включая отображение форматированного текста, работу с пошаговым режимом,
/// управление позиционированием курсора и вспомогательные операции вывода.
/// </summary>
public interface IMessageOutputService
{
    /// <summary>
    /// Асинхронно отображает сообщение пользователю с учётом форматирования,
    /// режима выполнения и параметров пошагового вывода.
    /// </summary>
    /// <param name="model">
    /// Модель данных выводимого сообщения, содержащая текст и параметры оформления.
    /// </param>
    /// <param name="isBlockStart">
    /// Определяет, является ли сообщение началом логического блока 
    /// (используется для визуального форматирования).
    /// </param>
    /// <param name="skipStepModeCheck">
    /// Если true — пропускает ожидание пользовательского подтверждения
    /// в пошаговом режиме выполнения алгоритма.
    /// </param>
    /// <param name="skipPause">
    /// Если true — отключает встроенную автоматическую паузу перед выводом.
    /// </param>
    /// <param name="callerName">Имя вызывающего метода (устанавливается автоматически).</param>
    /// <param name="callerFile">Путь к файлу вызова метода (устанавливается автоматически).</param>
    /// <param name="callerLine">Номер строки вызова (устанавливается автоматически).</param>
    /// <returns>
    /// Задача, представляющая операцию отображения сообщения.
    /// </returns>
    Task ShowMessageAsync(
        MessageModel model,
        bool isBlockStart = false,
        bool skipStepModeCheck = false,
        bool skipPause = false,
        [CallerMemberName] string callerName = "",
        [CallerFilePath] string callerFile = "",
        [CallerLineNumber] int callerLine = 0);

    /// <summary>
    /// Асинхронно добавляет пустую строку с возможным уровнем отступа.
    /// </summary>
    /// <param name="indentLevel">
    /// Количество отступов перед пустой строкой.
    /// </param>
    Task AppendEmptyLineAsync(int indentLevel = 0);

    /// <summary>
    /// Заголовок текущего сообщения. Может использоваться интерфейсом
    /// для отображения секций или структурирования текста.
    /// </summary>
    string Header { get; set; }

    /// <summary>
    /// Возвращает номер последней строки вывода,
    /// что позволяет сервису или вызывающему коду управлять позицией курсора.
    /// </summary>
    /// <returns>Последний номер строки в текущем выводе.</returns>
    int GetLastLineNumber();

    /// <summary>
    /// Асинхронно перемещает курсор или область вывода к указанному номеру строки.
    /// Используется при обновлении текста или навигации внутри протокола.
    /// </summary>
    /// <param name="lineNumber">Номер строки, к которой необходимо перейти.</param>
    Task MoveToLineAsync(int lineNumber);
}
