// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.DebugTarget
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Layouts;
using System.Diagnostics;

#nullable disable
namespace MetroLog.Targets
{
  public class DebugTarget(Layout layout) : SyncTarget(layout)
  {
    public DebugTarget()
      : this((Layout) new SingleLineLayout())
    {
    }

    protected override void Write(LogWriteContext context, LogEventInfo entry)
    {
      Debug.WriteLine(this.Layout.GetFormattedString(context, entry));
    }
  }
}
