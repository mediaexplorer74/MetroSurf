// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ProcessItemEventArgs`2
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

#nullable disable
namespace MetroLab.Common
{
  public class ProcessItemEventArgs<TKey, TValue>
  {
    public TKey Key { get; private set; }

    public TValue Value { get; private set; }

    internal ProcessItemEventArgs(TKey key, TValue value)
    {
      this.Key = key;
      this.Value = value;
    }
  }
}
