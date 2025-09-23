// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.BufferedTarget
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Layouts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Targets
{
  public abstract class BufferedTarget : AsyncTarget, ILazyFlushable
  {
    private object _lock = new object();

    private List<LogEventInfo> Buffer { get; set; }

    private int Threshold { get; set; }

    public BufferedTarget(Layout layout, int threshold)
      : base(layout)
    {
      this.Threshold = threshold >= 1 ? threshold : throw new ArgumentOutOfRangeException(nameof (threshold));
      this.Buffer = new List<LogEventInfo>();
    }

    protected override sealed Task<LogWriteOperation> WriteAsyncCore(
      LogWriteContext context,
      LogEventInfo entry)
    {
      try
      {
        List<LogEventInfo> toFlush = (List<LogEventInfo>) null;
        lock (this._lock)
        {
          this.Buffer.Add(entry);
          if (this.Buffer.Count >= this.Threshold)
          {
            toFlush = new List<LogEventInfo>((IEnumerable<LogEventInfo>) this.Buffer);
            this.Buffer = new List<LogEventInfo>();
          }
        }
        return toFlush != null ? this.FlushAsync(context, (IEnumerable<LogEventInfo>) toFlush) : Task.FromResult<LogWriteOperation>(new LogWriteOperation((Target) this, entry, true));
      }
      catch (Exception ex)
      {
        InternalLogger.Current.Error(string.Format("Failed to write to target '{0}'.", (object) this), ex);
        return Task.FromResult<LogWriteOperation>(new LogWriteOperation((Target) this, entry, false));
      }
    }

    private async Task<LogWriteOperation> FlushAsync(
      LogWriteContext context,
      IEnumerable<LogEventInfo> toFlush)
    {
      try
      {
        await this.DoFlushAsync(context, toFlush);
        return new LogWriteOperation((Target) this, toFlush, true);
      }
      catch (Exception ex)
      {
        InternalLogger.Current.Error(string.Format("Failed to flush for target '{0}'.", (object) this), ex);
        return new LogWriteOperation((Target) this, toFlush, false);
      }
    }

    protected abstract Task DoFlushAsync(LogWriteContext context, IEnumerable<LogEventInfo> toFlush);

    async Task ILazyFlushable.LazyFlushAsync(LogWriteContext context)
    {
      List<LogEventInfo> logEventInfoList = (List<LogEventInfo>) null;
      lock (this._lock)
      {
        logEventInfoList = new List<LogEventInfo>((IEnumerable<LogEventInfo>) this.Buffer);
        this.Buffer = new List<LogEventInfo>();
      }
      if (!logEventInfoList.Any<LogEventInfo>())
        return;
      await this.DoFlushAsync(context, (IEnumerable<LogEventInfo>) logEventInfoList);
    }
  }
}
