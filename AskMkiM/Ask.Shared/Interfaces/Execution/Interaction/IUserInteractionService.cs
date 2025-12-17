using Ask.Shared.Enums.Common;
using Ask.Shared.Interfaces.Execution.Output;
using Ask.Shared.Models.Errors;

namespace Ask.Shared.Interfaces.Execution.Interaction;

/// <summary>
/// Предоставляет контракт для сервисов взаимодействия с пользователем,
/// включая отображение сообщений, ожидание действий и обработку ошибок.
/// </summary>
public interface IUserInteractionService : IMessageOutputService
{
  /// <summary>
  /// Сервис для управления доступными пользователю кнопками и действиями.
  /// </summary>
  IButtonService ButtonService { get; set; }

  /// <summary>
  /// Ожидает подтверждение действия пользователем 
  /// (например, нажатием кнопки администратора или аппаратного ключа).
  /// </summary>
  /// <returns>
  /// True, если пользователь подтвердил действие; иначе — false.
  /// </returns>
  Task<bool> WaitAdminButtonAsync();

  /// <summary>
  /// Ожидает выбора пользователем действия после сообщения
  /// (например: повторить, продолжить, завершить).
  /// </summary>
  /// <param name="loop">
  /// Если true — продолжает ожидать выбора до получения допустимого варианта.
  /// </param>
  /// <returns>
  /// Действие, выбранное пользователем.
  /// </returns>
  Task<UserAction> WaitUserActionAsync(bool loop = false);

  /// <summary>
  /// Возвращает токен отмены, связанный с текущим процессом взаимодействия.
  /// </summary>
  CancellationToken GetCancellationToken();

  /// <summary>
  /// Добавляет ошибку в историю или накопитель ошибок.
  /// Рекомендуется использовать для отображения и дальнейшей обработки.
  /// </summary>
  /// <param name="errorItem">Информация об ошибке.</param>
  void AddError(ErrorItem errorItem);
}