// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.Logger
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Internal
{
  internal class Logger : ILogger, ILoggerAsync, ILoggerQuery
  {
    private readonly LoggingConfiguration _configuration;
    private static readonly Task<LogWriteOperation[]> EmptyOperations = Task.FromResult<LogWriteOperation[]>(new LogWriteOperation[0]);

    public string Name { get; private set; }

    public Logger(string name, LoggingConfiguration config)
    {
      this.Name = name;
      this._configuration = config;
    }

    internal Task<LogWriteOperation[]> TraceAsync(string message, Exception ex = null)
    {
      return this.LogAsync(LogLevel.Trace, message, ex);
    }

    internal Task<LogWriteOperation[]> TraceAsync(string message, params object[] ps)
    {
      return this.LogAsync(LogLevel.Trace, message, ps);
    }

    internal Task<LogWriteOperation[]> DebugAsync(string message, Exception ex = null)
    {
      return this.LogAsync(LogLevel.Debug, message, ex);
    }

    internal Task<LogWriteOperation[]> DebugAsync(string message, params object[] ps)
    {
      return this.LogAsync(LogLevel.Debug, message, ps);
    }

    internal Task<LogWriteOperation[]> InfoAsync(string message, Exception ex = null)
    {
      return this.LogAsync(LogLevel.Info, message, ex);
    }

    internal Task<LogWriteOperation[]> InfoAsync(string message, params object[] ps)
    {
      return this.LogAsync(LogLevel.Info, message, ps);
    }

    internal Task<LogWriteOperation[]> WarnAsync(string message, Exception ex = null)
    {
      return this.LogAsync(LogLevel.Warn, message, ex);
    }

    internal Task<LogWriteOperation[]> WarnAsync(string message, params object[] ps)
    {
      return this.LogAsync(LogLevel.Warn, message, ps);
    }

    internal Task<LogWriteOperation[]> ErrorAsync(string message, Exception ex = null)
    {
      return this.LogAsync(LogLevel.Error, message, ex);
    }

    internal Task<LogWriteOperation[]> ErrorAsync(string message, params object[] ps)
    {
      return this.LogAsync(LogLevel.Error, message, ps);
    }

    internal Task<LogWriteOperation[]> FatalAsync(string message, Exception ex = null)
    {
      return this.LogAsync(LogLevel.Fatal, message, ex);
    }

    internal Task<LogWriteOperation[]> FatalAsync(string message, params object[] ps)
    {
      return this.LogAsync(LogLevel.Fatal, message, ps);
    }

    internal Task<LogWriteOperation[]> LogAsync(LogLevel logLevel, string message, Exception ex)
    {
      return this.LogInternal(logLevel, message, (object[]) null, ex, false);
    }

    internal Task<LogWriteOperation[]> LogAsync(
      LogLevel logLevel,
      string message,
      params object[] ps)
    {
      return this.LogInternal(logLevel, message, ps, (Exception) null, true);
    }

    private Task<LogWriteOperation[]> LogInternal(
      LogLevel level,
      string message,
      object[] ps,
      Exception ex,
      bool doFormat)
    {
      try
      {
        if (!this._configuration.IsEnabled)
          return Logger.EmptyOperations;
        IEnumerable<Target> targets = this._configuration.GetTargets(level);
        if (!targets.Any<Target>())
          return Logger.EmptyOperations;
        if (doFormat)
          message = string.Format(message, ps);
        LogEventInfo entry = new LogEventInfo(level, this.Name, message, ex);
        LogWriteContext context = new LogWriteContext();
        return Task.WhenAll<LogWriteOperation>(targets.Select<Target, Task<LogWriteOperation>>((Func<Target, Task<LogWriteOperation>>) (target => target.WriteAsync(context, entry))));
      }
      catch (Exception ex1)
      {
        InternalLogger.Current.Error("Logging operation failed.", ex1);
        return Logger.EmptyOperations;
      }
    }

    public bool IsTraceEnabled => this.IsEnabled(LogLevel.Trace);

    public bool IsDebugEnabled => this.IsEnabled(LogLevel.Debug);

    public bool IsInfoEnabled => this.IsEnabled(LogLevel.Info);

    public bool IsWarnEnabled => this.IsEnabled(LogLevel.Warn);

    public bool IsErrorEnabled => this.IsEnabled(LogLevel.Error);

    public bool IsFatalEnabled => this.IsEnabled(LogLevel.Fatal);

    public void Trace(string message, Exception ex = null) => this.TraceAsync(message, ex);

    public void Trace(string message, params object[] ps) => this.TraceAsync(message, ps);

    public void Debug(string message, Exception ex = null) => this.DebugAsync(message, ex);

    public void Debug(string message, params object[] ps) => this.DebugAsync(message, ps);

    public void Info(string message, Exception ex = null) => this.InfoAsync(message, ex);

    public void Info(string message, params object[] ps) => this.InfoAsync(message, ps);

    public void Warn(string message, Exception ex = null) => this.WarnAsync(message, ex);

    public void Warn(string message, params object[] ps) => this.WarnAsync(message, ps);

    public void Error(string message, Exception ex = null) => this.ErrorAsync(message, ex);

    public void Error(string message, params object[] ps) => this.ErrorAsync(message, ps);

    public void Fatal(string message, Exception ex = null) => this.FatalAsync(message, ex);

    public void Fatal(string message, params object[] ps) => this.FatalAsync(message, ps);

    public void Log(LogLevel logLevel, string message, Exception ex)
    {
      this.LogAsync(logLevel, message, ex);
    }

    public void Log(LogLevel logLevel, string message, params object[] ps)
    {
      this.LogAsync(logLevel, message, ps);
    }

    public bool IsEnabled(LogLevel level) => this._configuration.GetTargets(level).Any<Target>();

    Task<LogWriteOperation[]> ILoggerAsync.TraceAsync(string message, Exception ex)
    {
      return this.TraceAsync(message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.TraceAsync(string message, params object[] ps)
    {
      return this.TraceAsync(message, ps);
    }

    Task<LogWriteOperation[]> ILoggerAsync.DebugAsync(string message, Exception ex)
    {
      return this.DebugAsync(message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.DebugAsync(string message, params object[] ps)
    {
      return this.DebugAsync(message, ps);
    }

    Task<LogWriteOperation[]> ILoggerAsync.InfoAsync(string message, Exception ex)
    {
      return this.InfoAsync(message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.InfoAsync(string message, params object[] ps)
    {
      return this.InfoAsync(message, ps);
    }

    Task<LogWriteOperation[]> ILoggerAsync.WarnAsync(string message, Exception ex)
    {
      return this.WarnAsync(message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.WarnAsync(string message, params object[] ps)
    {
      return this.WarnAsync(message, ps);
    }

    Task<LogWriteOperation[]> ILoggerAsync.ErrorAsync(string message, Exception ex)
    {
      return this.ErrorAsync(message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.ErrorAsync(string message, params object[] ps)
    {
      return this.ErrorAsync(message, ps);
    }

    Task<LogWriteOperation[]> ILoggerAsync.FatalAsync(string message, Exception ex)
    {
      return this.FatalAsync(message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.FatalAsync(string message, params object[] ps)
    {
      return this.FatalAsync(message, ps);
    }

    Task<LogWriteOperation[]> ILoggerAsync.LogAsync(
      LogLevel logLevel,
      string message,
      Exception ex)
    {
      return this.LogAsync(logLevel, message, ex);
    }

    Task<LogWriteOperation[]> ILoggerAsync.LogAsync(
      LogLevel logLevel,
      string message,
      params object[] ps)
    {
      return this.LogAsync(logLevel, message, ps);
    }

    public IEnumerable<Target> GetTargets() => this._configuration.GetTargets();
  }
}
