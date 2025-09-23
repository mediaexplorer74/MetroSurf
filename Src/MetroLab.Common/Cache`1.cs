// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Cache`1
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MetroLab.Common
{
  public class Cache<T>
  {
    private const int MaxPagesInCacheCount = 3;
    private List<T> _items = new List<T>();

    public int Limit { get; private set; }

    public Cache() => this.Limit = 3;

    public Cache(int limit)
    {
      this.Limit = limit >= 0 ? limit : throw new ArgumentOutOfRangeException(nameof (limit));
    }

    public void Remove(T item) => this._items.Remove(item);

    public void Add(T item)
    {
      this._items.Remove(item);
      if (this.Limit > 0)
        this._items.Add(item);
      if (this._items.Count <= this.Limit)
        return;
      this._items.RemoveAt(0);
    }

    public void Clear() => this._items.Clear();
  }
}
