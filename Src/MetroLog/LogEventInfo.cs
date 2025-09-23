// Decompiled with JetBrains decompiler
// Type: MetroLog.LogEventInfo
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Internal;
using System;
using System.Threading;

#nullable disable
namespace MetroLog
{
  public class LogEventInfo
  {
    private ExceptionWrapper _exceptionWrapper;
    private static long _globalSequenceId;

    public long SequenceID { get; set; }

    public LogLevel Level { get; set; }

    public string Logger { get; set; }

    public string Message { get; set; }

    public DateTimeOffset TimeStamp { get; set; }

    [JsonIgnore]
    public Exception Exception { get; set; }

    public LogEventInfo(LogLevel level, string logger, string message, Exception ex)
    {
      this.Level = level;
      this.Logger = logger;
      this.Message = message;
      this.Exception = ex;
      this.TimeStamp = LogManagerBase.GetDateTime();
      this.SequenceID = LogEventInfo.GetNextSequenceId();
    }

    internal static long GetNextSequenceId()
    {
      return Interlocked.Increment(ref LogEventInfo._globalSequenceId);
    }

    public string ToJson() => SimpleJson.SerializeObject((object) this);

    public ExceptionWrapper ExceptionWrapper
    {
      get
      {
        if (this._exceptionWrapper == null && this.Exception != null)
          this._exceptionWrapper = new ExceptionWrapper(this.Exception);
        return this._exceptionWrapper;
      }
      set => this._exceptionWrapper = value;
    }
  }
}
