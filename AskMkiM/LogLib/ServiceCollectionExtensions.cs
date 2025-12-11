using LogLib.Abstractions;
using LogLib.Context;
using LogLib.NLogAdapter;
using LogLib.Services;
using Microsoft.Extensions.DependencyInjection;
using NLog;

namespace LogLib;

/// <summary>
/// Набор методов расширения для регистрации LogLib в DI-контейнере.
/// 
/// Позволяет подключить логирование одной строкой:
/// services.AddLogLib();
///
/// Регистрируются:
/// - ILogWriter                → NLogWriter
/// - ILogContextProvider       → CallerInfoContextProvider
/// - ILogService               → LogService
///
/// LogLib остаётся полностью независимой и может использоваться
/// в любом приложении — UI, консоль, сервисы, библиотеки.
/// </summary>
public static class ServiceCollectionExtensions
{
  /// <summary>
  /// Добавляет сервисы логирования LogLib в DI-контейнер.
  /// </summary>
  public static IServiceCollection AddLogLib(this IServiceCollection services)
  {
    LogManager.Setup().LoadConfigurationFromFile("NLog.config");
    
    services.AddSingleton<ILogWriter, NLogWriter>();
    services.AddSingleton<ILogContextProvider, CallerInfoContextProvider>();
    services.AddSingleton<ILogService, LogService>();

    return services;
  }
}