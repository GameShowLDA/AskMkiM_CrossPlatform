using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Ask.Shared.Interfaces.Console;
using AskMain.Startup;
using Avalonia.Markup.Xaml;
using AskMain.ViewModels;
using AskMain.Views;

namespace AskMain;

public partial class App : Application
{
  /// <summary>
  /// Инициализирует XAML-ресурсы приложения.
  /// </summary>
  public override void Initialize()
  {
    AvaloniaXamlLoader.Load(this);
  }

  /// <summary>
  /// Вызывается после завершения инициализации Avalonia Framework.
  /// Здесь выполняется запуск стартовой последовательности приложения
  /// и инициализация всех инфраструктурных компонентов.
  /// </summary>
  public override void OnFrameworkInitializationCompleted()
  {
    if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
    {
      var mainWindow = new MainWindow();
      desktop.MainWindow = mainWindow;

      var startup = new ApplicationStartup();
      startup.Run(mainWindow);
    }

    base.OnFrameworkInitializationCompleted();
  }

  private void DisableAvaloniaDataAnnotationValidation()
  {
    // Get an array of plugins to remove
    var dataValidationPluginsToRemove =
      BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

    // remove each entry found
    foreach (var plugin in dataValidationPluginsToRemove)
    {
      BindingPlugins.DataValidators.Remove(plugin);
    }
  }
}