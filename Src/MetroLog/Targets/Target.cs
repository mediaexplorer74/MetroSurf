// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.Target
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Layouts;
using System.Threading.Tasks;

#nullable disable
namespace MetroLog.Targets
{
  public abstract class Target
  {
    protected Layout Layout { get; private set; }

    protected Target(Layout layout) => this.Layout = layout;

    internal async Task<LogWriteOperation> WriteAsync(LogWriteContext context, LogEventInfo entry)
    {
      return await this.WriteAsyncCore(context, entry).ConfigureAwait(false);
    }

    protected abstract Task<LogWriteOperation> WriteAsyncCore(
      LogWriteContext context,
      LogEventInfo entry);
  }
}
