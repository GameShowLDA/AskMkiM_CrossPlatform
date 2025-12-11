namespace Ask.Shared.Interfaces.Devices.Base;

/// <summary>
/// Определяет контракт для объектов, которые имеют родительский элемент
/// в иерархической структуре.
/// </summary>
/// <typeparam name="TParent">
/// Тип родительского объекта, к которому принадлежит текущий элемент.
/// </typeparam>
public interface IHasParent<TParent>
{
  /// <summary>
  /// Родительский объект текущего элемента.
  /// </summary>
  TParent Parent { get; }
}
