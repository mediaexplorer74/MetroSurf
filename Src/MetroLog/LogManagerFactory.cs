// Decompiled with JetBrains decompiler
// Type: MetroLog.LogManagerFactory
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Internal;
using System;
using System.Threading;

#nullable disable
namespace MetroLog
{
  public static class LogManagerFactory
  {
    private static readonly ILogConfigurator _configurator = PlatformAdapter.Resolve<ILogConfigurator>();
    private static LoggingConfiguration _defaultConfig = LogManagerFactory._configurator.CreateDefaultSettings();
    private static readonly Lazy<ILogManager> _lazyLogManager = new Lazy<ILogManager>((Func<ILogManager>) (() => LogManagerFactory.CreateLogManager()), LazyThreadSafetyMode.ExecutionAndPublication);

    public static LoggingConfiguration CreateLibraryDefaultSettings()
    {
      return LogManagerFactory._configurator.CreateDefaultSettings();
    }

    public static ILogManager CreateLogManager(LoggingConfiguration config = null)
    {
      LoggingConfiguration configuration = config ?? LogManagerFactory.DefaultConfiguration;
      configuration.Freeze();
      ILogManagerCreator logManagerCreator = PlatformAdapter.Resolve<ILogManagerCreator>(false);
      ILogManager manager = logManagerCreator == null ? (ILogManager) new LogManagerBase(configuration) : logManagerCreator.Create(configuration);
      LogManagerFactory._configurator.OnLogManagerCreated(manager);
      return manager;
    }

    public static LoggingConfiguration DefaultConfiguration
    {
      get => LogManagerFactory._defaultConfig;
      set
      {
        if (value == null)
          throw new ArgumentNullException(nameof (value));
        if (LogManagerFactory._lazyLogManager.IsValueCreated)
          throw new InvalidOperationException("Must set DefaultConfiguration before any calls to DefaultLogManager");
        LogManagerFactory._defaultConfig = value;
      }
    }

    public static ILogManager DefaultLogManager => LogManagerFactory._lazyLogManager.Value;
  }
}
