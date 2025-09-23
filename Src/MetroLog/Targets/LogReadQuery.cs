// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.LogReadQuery
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;

#nullable disable
namespace MetroLog.Targets
{
  public class LogReadQuery
  {
    public bool IsTraceEnabled { get; set; }

    public bool IsDebugEnabled { get; set; }

    public bool IsInfoEnabled { get; set; }

    public bool IsWarnEnabled { get; set; }

    public bool IsErrorEnabled { get; set; }

    public bool IsFatalEnabled { get; set; }

    public int Top { get; set; }

    public DateTime FromDateTimeUtc { get; set; }

    public LogReadQuery()
    {
      this.IsTraceEnabled = false;
      this.IsDebugEnabled = false;
      this.IsInfoEnabled = true;
      this.IsWarnEnabled = true;
      this.IsErrorEnabled = true;
      this.IsFatalEnabled = true;
      this.Top = 1000;
      this.FromDateTimeUtc = DateTime.UtcNow.AddDays(-7.0);
    }

    public void SetLevels(LogLevel from, LogLevel to)
    {
      this.IsTraceEnabled = LogLevel.Trace >= from && LogLevel.Trace <= to;
      this.IsDebugEnabled = LogLevel.Debug >= from && LogLevel.Debug <= to;
      this.IsInfoEnabled = LogLevel.Info >= from && LogLevel.Info <= to;
      this.IsWarnEnabled = LogLevel.Warn >= from && LogLevel.Warn <= to;
      this.IsErrorEnabled = LogLevel.Error >= from && LogLevel.Error <= to;
      this.IsFatalEnabled = LogLevel.Fatal >= from && LogLevel.Fatal <= to;
    }
  }
}
