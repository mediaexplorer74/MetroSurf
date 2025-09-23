// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.JsonArray
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace MetroLog.Internal
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class JsonArray : List<object>
  {
    public JsonArray()
    {
    }

    public JsonArray(int capacity)
      : base(capacity)
    {
    }

    public override string ToString() => SimpleJson.SerializeObject((object) this) ?? string.Empty;
  }
}
