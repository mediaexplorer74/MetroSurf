// Decompiled with JetBrains decompiler
// Type: MetroLog.ILoggerAsync
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog
{
  public interface ILoggerAsync
  {
    string Name { get; }

    bool IsTraceEnabled { get; }

    bool IsDebugEnabled { get; }

    bool IsInfoEnabled { get; }

    bool IsWarnEnabled { get; }

    bool IsErrorEnabled { get; }

    bool IsFatalEnabled { get; }

    Task<LogWriteOperation[]> TraceAsync(string message, Exception ex = null);

    Task<LogWriteOperation[]> TraceAsync(string message, params object[] ps);

    Task<LogWriteOperation[]> DebugAsync(string message, Exception ex = null);

    Task<LogWriteOperation[]> DebugAsync(string message, params object[] ps);

    Task<LogWriteOperation[]> InfoAsync(string message, Exception ex = null);

    Task<LogWriteOperation[]> InfoAsync(string message, params object[] ps);

    Task<LogWriteOperation[]> WarnAsync(string message, Exception ex = null);

    Task<LogWriteOperation[]> WarnAsync(string message, params object[] ps);

    Task<LogWriteOperation[]> ErrorAsync(string message, Exception ex = null);

    Task<LogWriteOperation[]> ErrorAsync(string message, params object[] ps);

    Task<LogWriteOperation[]> FatalAsync(string message, Exception ex = null);

    Task<LogWriteOperation[]> FatalAsync(string message, params object[] ps);

    Task<LogWriteOperation[]> LogAsync(LogLevel logLevel, string message, Exception ex);

    Task<LogWriteOperation[]> LogAsync(LogLevel logLevel, string message, params object[] ps);

    bool IsEnabled(LogLevel level);
  }
}
