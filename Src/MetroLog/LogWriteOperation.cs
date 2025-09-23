// Decompiled with JetBrains decompiler
// Type: MetroLog.LogWriteOperation
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Targets;
using System.Collections.Generic;

#nullable disable
namespace MetroLog
{
  public struct LogWriteOperation
  {
    private readonly Target _target;
    private readonly List<LogEventInfo> _entries;
    private readonly bool _success;

    public LogWriteOperation(Target target, LogEventInfo entry, bool success)
    {
      Target target1 = target;
      List<LogEventInfo> entries = new List<LogEventInfo>();
      entries.Add(entry);
      int num = success ? 1 : 0;
      this = new LogWriteOperation(target1, (IEnumerable<LogEventInfo>) entries, num != 0);
    }

    public LogWriteOperation(Target target, IEnumerable<LogEventInfo> entries, bool success)
    {
      this._target = target;
      this._entries = new List<LogEventInfo>(entries);
      this._success = success;
    }

    public Target Target => this._target;

    public IEnumerable<LogEventInfo> GetEntries()
    {
      return (IEnumerable<LogEventInfo>) new List<LogEventInfo>((IEnumerable<LogEventInfo>) this._entries);
    }

    public bool Success => this._success;
  }
}
