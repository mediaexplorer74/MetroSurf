// Decompiled with JetBrains decompiler
// Type: MetroLog.LoggingConfiguration
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MetroLog
{
  public class LoggingConfiguration
  {
    private readonly List<TargetBinding> _bindings;
    private readonly object _bindingsLock = new object();
    private bool _frozen;

    public bool IsEnabled { get; set; }

    public LoggingConfiguration()
    {
      this.IsEnabled = true;
      this._bindings = new List<TargetBinding>();
    }

    public void AddTarget(LogLevel level, Target target)
    {
      int num = (int) level;
      this.AddTarget((LogLevel) num, (LogLevel) num, target);
    }

    public void AddTarget(LogLevel min, LogLevel max, Target target)
    {
      if (this._frozen)
        throw new InvalidOperationException("Cannot modify config after initialization");
      lock (this._bindingsLock)
        this._bindings.Add(new TargetBinding(min, max, target));
    }

    internal IEnumerable<Target> GetTargets()
    {
      lock (this._bindingsLock)
      {
        List<Target> targets = new List<Target>();
        foreach (TargetBinding binding in this._bindings)
          targets.Add(binding.Target);
        return (IEnumerable<Target>) targets;
      }
    }

    internal IEnumerable<Target> GetTargets(LogLevel level)
    {
      lock (this._bindingsLock)
        return (IEnumerable<Target>) this._bindings.Where<TargetBinding>((Func<TargetBinding, bool>) (v => v.SupportsLevel(level))).Select<TargetBinding, Target>((Func<TargetBinding, Target>) (binding => binding.Target)).ToList<Target>();
    }

    internal void Freeze() => this._frozen = true;
  }
}
