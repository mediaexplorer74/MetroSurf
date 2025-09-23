// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.CacheState
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;

#nullable disable
namespace MetroLab.Common
{
  [Flags]
  public enum CacheState
  {
    Empty = 0,
    Available = 1,
    Actual = 7,
    ActualOrDispensable = 4,
  }
}
