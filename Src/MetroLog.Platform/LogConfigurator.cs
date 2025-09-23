// Decompiled with JetBrains decompiler
// Type: MetroLog.LogConfigurator
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Internal;
using MetroLog.Targets;

#nullable disable
namespace MetroLog
{
  internal sealed class LogConfigurator : LogConfiguratorBase
  {
    public override LoggingConfiguration CreateDefaultSettings()
    {
      LoggingConfiguration defaultSettings = base.CreateDefaultSettings();
      defaultSettings.AddTarget(LogLevel.Error, LogLevel.Fatal, (Target) new FileSnapshotTarget());
      return defaultSettings;
    }

    public override void OnLogManagerCreated(ILogManager manager)
    {
      LazyFlushManager.Initialize(manager);
    }
  }
}
