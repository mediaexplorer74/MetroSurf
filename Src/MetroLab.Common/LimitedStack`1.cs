// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.LimitedStack`1
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace MetroLab.Common
{
  public class LimitedStack<T>
  {
    private readonly List<T> _innerList = new List<T>();

    public event LimitedStack<T>.GotOutItemRemovedHandler GotOutItemRemoved;

    private void OnGotOutItemRemoved(T removedItem)
    {
      if (this.GotOutItemRemoved == null)
        return;
      this.GotOutItemRemoved((object) this, new LimitedStack<T>.GotOutItemRemovedArgs(removedItem));
    }

    public int Limit { get; private set; }

    public LimitedStack(int limit)
    {
      this.Limit = limit >= 0 ? limit : throw new ArgumentOutOfRangeException(nameof (limit));
    }

    public void Clear() => this._innerList.Clear();

    public int Count => this._innerList.Count;

    public T Peek()
    {
      return this._innerList.Count != 0 ? this._innerList[this._innerList.Count - 1] : throw new ArgumentOutOfRangeException();
    }

    public T Pop()
    {
      if (this._innerList.Count == 0)
        throw new ArgumentOutOfRangeException();
      int index = this._innerList.Count - 1;
      T inner = this._innerList[index];
      this._innerList.RemoveAt(index);
      return inner;
    }

    public void Push(T item)
    {
      this._innerList.Add(item);
      if (this._innerList.Count <= this.Limit)
        return;
      T inner = this._innerList[0];
      this._innerList.RemoveAt(0);
      this.OnGotOutItemRemoved(inner);
    }

    public class GotOutItemRemovedArgs : EventArgs
    {
      public T Removeditem { get; private set; }

      public GotOutItemRemovedArgs(T removeditem) => this.Removeditem = removeditem;
    }

    public delegate void GotOutItemRemovedHandler(
      object sender,
      LimitedStack<T>.GotOutItemRemovedArgs args);
  }
}
