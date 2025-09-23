// Decompiled with JetBrains decompiler
// Type: ReleaseDateAttribute
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Runtime.CompilerServices;

#nullable disable
[CompilerGenerated]
internal sealed class ReleaseDateAttribute : Attribute
{
  public string Date { get; private set; }

  public ReleaseDateAttribute(string date) => this.Date = date;
}
