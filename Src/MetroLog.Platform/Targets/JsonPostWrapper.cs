// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.JsonPostWrapper
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Internal;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace MetroLog.Targets
{
  public class JsonPostWrapper
  {
    public ILoggingEnvironment Environment { get; set; }

    public LogEventInfo[] Events { get; set; }

    internal JsonPostWrapper(ILoggingEnvironment environment, IEnumerable<LogEventInfo> events)
    {
      this.Environment = environment;
      this.Events = events.ToArray<LogEventInfo>();
    }

    internal string ToJson() => SimpleJson.SerializeObject((object) this);
  }
}
