// Decompiled with JetBrains decompiler
// Type: MetroLog.Targets.FileTimestampMode
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;

#nullable disable
namespace MetroLog.Targets
{
  [Flags]
  public enum FileTimestampMode
  {
    None = 0,
    Date = 1,
    Time = 2,
    DateTime = Time | Date, // 0x00000003
  }
}
