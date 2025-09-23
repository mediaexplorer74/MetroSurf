// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.TargetBinding
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System.Diagnostics;

#nullable disable
namespace MetroLog.Targets
{
  [DebuggerDisplay("Name = {Target.GetType().Name}, Min = {MinLevel}, Max = {MaxLevel}")]
  internal class TargetBinding
  {
    private LogLevel MinLevel { get; set; }

    private LogLevel MaxLevel { get; set; }

    internal Target Target { get; private set; }

    internal TargetBinding(LogLevel min, LogLevel max, Target target)
    {
      this.MinLevel = min;
      this.MaxLevel = max;
      this.Target = target;
    }

    internal bool SupportsLevel(LogLevel level) => level >= this.MinLevel && level <= this.MaxLevel;
  }
}
