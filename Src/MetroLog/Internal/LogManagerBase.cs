// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.LogManagerBase
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Targets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Internal
{
  public class LogManagerBase : ILogManager
  {
    private readonly Dictionary<string, Logger> _loggers;
    private readonly object _loggersLock = new object();
    internal const string DateTimeFormat = "o";

    public LoggingConfiguration DefaultConfiguration { get; private set; }

    public event EventHandler<LoggerEventArgs> LoggerCreated;

    public Task<Stream> GetCompressedLogs()
    {
      FileTargetBase fileTargetBase = this.DefaultConfiguration.GetTargets().OfType<FileTargetBase>().FirstOrDefault<FileTargetBase>();
      return fileTargetBase != null ? fileTargetBase.GetCompressedLogs() : Task.FromResult<Stream>((Stream) null);
    }

    public LogManagerBase(LoggingConfiguration configuration)
    {
      if (configuration == null)
        throw new ArgumentNullException(nameof (configuration));
      this._loggers = new Dictionary<string, Logger>((IEqualityComparer<string>) StringComparer.OrdinalIgnoreCase);
      this.DefaultConfiguration = configuration;
    }

    public ILogger GetLogger<T>(LoggingConfiguration config = null)
    {
      return this.GetLogger(typeof (T), config);
    }

    public ILogger GetLogger(Type type, LoggingConfiguration config = null)
    {
      if (type == null)
        throw new ArgumentNullException(nameof (type));
      return this.GetLogger(type.Name, config);
    }

    public ILogger GetLogger(string name, LoggingConfiguration config = null)
    {
      if (string.IsNullOrWhiteSpace(name))
        throw new ArgumentNullException(nameof (name));
      lock (this._loggersLock)
      {
        if (!this._loggers.ContainsKey(name))
        {
          Logger logger = new Logger(name, config ?? this.DefaultConfiguration);
          InternalLogger.Current.Info("Created Logger '{0}'", (object) name);
          this.OnLoggerCreatedSafe(new LoggerEventArgs((ILogger) logger));
          this._loggers[name] = logger;
        }
        return (ILogger) this._loggers[name];
      }
    }

    private void OnLoggerCreatedSafe(LoggerEventArgs args)
    {
      try
      {
        this.OnLoggerCreated(args);
      }
      catch (Exception ex)
      {
        InternalLogger.Current.Error("Failed to handle OnLoggerCreated event.", ex);
      }
    }

    private void OnLoggerCreated(LoggerEventArgs args)
    {
      EventHandler<LoggerEventArgs> loggerCreated = this.LoggerCreated;
      if (loggerCreated == null)
        return;
      loggerCreated((object) this, args);
    }

    internal static DateTimeOffset GetDateTime() => DateTimeOffset.UtcNow;
  }
}
