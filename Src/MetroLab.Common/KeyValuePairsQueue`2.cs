// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.KeyValuePairsQueue`2
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Windows.System.Threading;

#nullable disable
namespace MetroLab.Common
{
  public class KeyValuePairsQueue<TKey, TValue>
  {
    private readonly object _locker = new object();
    private readonly object _pulsar = new object();
    private readonly Dictionary<TKey, TValue> _values = new Dictionary<TKey, TValue>();
    private readonly List<TKey> _queue = new List<TKey>();

    public event ProcessItemHandler<TKey, TValue> ProcessItem;

    private async Task OnProcessItem(ProcessItemEventArgs<TKey, TValue> args)
    {
      ProcessItemHandler<TKey, TValue> processItem = this.ProcessItem;
      if (processItem == null)
        return;
      await processItem((object) this, args);
    }

    public void StartWorker(int threadCount = 1)
    {
      if (threadCount < 1)
        throw new ArgumentOutOfRangeException(nameof (threadCount), "threadCount must be greater than 0");
      for (int index = 0; index < threadCount; ++index)
      {
        // ISSUE: method pointer
        ThreadPool.RunAsync(new WorkItemHandler((object) this, __methodptr(\u003CStartWorker\u003Eb__4_0)));
      }
    }

    private Dictionary<TKey, TValue> Values => this._values;

    public int Count
    {
      get
      {
        lock (this._locker)
          return this._queue.Count;
      }
    }

    private bool ContainsKey(TKey key)
    {
      lock (this._locker)
        return this.Values.ContainsKey(key);
    }

    public TValue this[TKey key]
    {
      get
      {
        lock (this._locker)
          return this.Values[key];
      }
    }

    private List<TKey> Queue => this._queue;

    public void Enqueue(TKey key, TValue value)
    {
      lock (this._locker)
      {
        if (this.Values.ContainsKey(key))
        {
          this.Values[key] = value;
        }
        else
        {
          this.Queue.Add(key);
          this.Values.Add(key, value);
          lock (this._pulsar)
            Monitor.PulseAll(this._pulsar);
        }
      }
    }

    public KeyValuePair<TKey, TValue> Dequeue()
    {
      lock (this._locker)
      {
        TKey key = this.Queue[0];
        this.Queue.RemoveAt(0);
        TValue obj = this.Values[key];
        this.Values.Remove(key);
        return new KeyValuePair<TKey, TValue>(key, obj);
      }
    }

    public bool Remove(TKey key)
    {
      lock (this._locker)
      {
        if (this.Values.ContainsKey(key))
        {
          this.Values.Remove(key);
          this.Queue.Remove(key);
          return true;
        }
      }
      return false;
    }

    private async void Worker()
    {
      while (true)
      {
        bool flag = false;
        TKey key = default (TKey);
        TValue obj = default (TValue);
        lock (this._locker)
        {
          if (this.Queue.Count > 0)
          {
            flag = true;
            key = this.Queue[0];
            this.Queue.RemoveAt(0);
            obj = this.Values[key];
            this.Values.Remove(key);
          }
        }
        if (flag)
        {
          await this.OnProcessItem(new ProcessItemEventArgs<TKey, TValue>(key, obj));
        }
        else
        {
          lock (this._pulsar)
            Monitor.Wait(this._pulsar, 1000);
        }
      }
    }
  }
}
