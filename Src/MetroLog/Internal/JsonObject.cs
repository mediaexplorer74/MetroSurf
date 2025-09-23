// Decompiled with JetBrains decompiler
// Type: MetroLog.Internal.JsonObject
// Assembly: MetroLog, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 3D4A35DD-41C3-4323-9D05-41E20096794C
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

#nullable disable
namespace MetroLog.Internal
{
  [EditorBrowsable(EditorBrowsableState.Never)]
  internal class JsonObject : 
    IDictionary<string, object>,
    ICollection<KeyValuePair<string, object>>,
    IEnumerable<KeyValuePair<string, object>>,
    IEnumerable
  {
    private readonly Dictionary<string, object> _members = new Dictionary<string, object>();

    public object this[int index]
    {
      get => JsonObject.GetAtIndex((IDictionary<string, object>) this._members, index);
    }

    internal static object GetAtIndex(IDictionary<string, object> obj, int index)
    {
      if (obj == null)
        throw new ArgumentNullException(nameof (obj));
      if (index >= obj.Count)
        throw new ArgumentOutOfRangeException(nameof (index));
      int num = 0;
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) obj)
      {
        if (num++ == index)
          return keyValuePair.Value;
      }
      return (object) null;
    }

    public void Add(string key, object value) => this._members.Add(key, value);

    public bool ContainsKey(string key) => this._members.ContainsKey(key);

    public ICollection<string> Keys => (ICollection<string>) this._members.Keys;

    public bool Remove(string key) => this._members.Remove(key);

    public bool TryGetValue(string key, out object value)
    {
      return this._members.TryGetValue(key, out value);
    }

    public ICollection<object> Values => (ICollection<object>) this._members.Values;

    public object this[string key]
    {
      get => this._members[key];
      set => this._members[key] = value;
    }

    public void Add(KeyValuePair<string, object> item) => this._members.Add(item.Key, item.Value);

    public void Clear() => this._members.Clear();

    public bool Contains(KeyValuePair<string, object> item)
    {
      return this._members.ContainsKey(item.Key) && this._members[item.Key] == item.Value;
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      int count = this.Count;
      foreach (KeyValuePair<string, object> keyValuePair in this)
      {
        array[arrayIndex++] = keyValuePair;
        if (--count <= 0)
          break;
      }
    }

    public int Count => this._members.Count;

    public bool IsReadOnly => false;

    public bool Remove(KeyValuePair<string, object> item) => this._members.Remove(item.Key);

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return (IEnumerator<KeyValuePair<string, object>>) this._members.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this._members.GetEnumerator();

    public override string ToString() => SimpleJson.SerializeObject((object) this);
  }
}
