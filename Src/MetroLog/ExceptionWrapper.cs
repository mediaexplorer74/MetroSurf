// Decompiled with JetBrains decompiler
// Type: MetroLog.ExceptionWrapper
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using MetroLog.Internal;
using System;

#nullable disable
namespace MetroLog
{
  public class ExceptionWrapper
  {
    public string TypeName { get; set; }

    public string AsString { get; set; }

    public int Hresult { get; set; }

    public ExceptionWrapper()
    {
    }

    internal ExceptionWrapper(Exception ex)
    {
      this.TypeName = ex.GetType().AssemblyQualifiedName;
      this.AsString = ex.ToString();
      this.Hresult = ex.HResult;
    }

    public string ToJson() => SimpleJson.SerializeObject((object) this);
  }
}
