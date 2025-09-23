// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.SyncTarget
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Layouts;
using System;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Targets
{
  public abstract class SyncTarget(Layout layout) : Target(layout)
  {
    protected override sealed Task<LogWriteOperation> WriteAsyncCore(
      LogWriteContext context,
      LogEventInfo entry)
    {
      try
      {
        this.Write(context, entry);
        return Task.FromResult<LogWriteOperation>(new LogWriteOperation((Target) this, entry, true));
      }
      catch (Exception ex)
      {
        InternalLogger.Current.Error(string.Format("Failed to write to target '{0}'.", (object) this), ex);
        return Task.FromResult<LogWriteOperation>(new LogWriteOperation((Target) this, entry, false));
      }
    }

    protected abstract void Write(LogWriteContext context, LogEventInfo entry);
  }
}
