// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.LoggingEnvironmentBase
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace MetroLog.Internal
{
  public abstract class LoggingEnvironmentBase : ILoggingEnvironment
  {
    public Guid SessionId { get; private set; }

    public string FxProfile { get; private set; }

    public bool IsDebugging { get; private set; }

    public Version MetroLogVersion { get; private set; }

    protected LoggingEnvironmentBase(string fxProfile)
    {
      this.SessionId = Guid.NewGuid();
      this.FxProfile = fxProfile;
      this.IsDebugging = Debugger.IsAttached;
      this.MetroLogVersion = typeof (ILogger).GetTypeInfo().Assembly.GetName().Version;
    }

    public string ToJson() => SimpleJson.SerializeObject((object) this);
  }
}
