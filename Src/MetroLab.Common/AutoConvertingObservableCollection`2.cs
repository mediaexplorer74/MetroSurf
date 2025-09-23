// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.AutoConvertingObservableCollection`2
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLab.Common
{
  public class AutoConvertingObservableCollection<T, TSource> : 
    BindableBase,
    IList,
    ICollection,
    IEnumerable,
    IList<T>,
    ICollection<T>,
    IEnumerable<T>,
    INotifyCollectionChanged
  {
    private IList<TSource> _sourceCollection;
    private readonly Dictionary<NotifyCollectionChangedEventHandler, CoreDispatcher> _collectionChanged = new Dictionary<NotifyCollectionChangedEventHandler, CoreDispatcher>();

    public Func<TSource, T> Converter { get; private set; }

    public Func<T, TSource> BackConverter { get; private set; }

    public AutoConvertingObservableCollection(
      Func<TSource, T> converter,
      Func<T, TSource> backConverter)
    {
      if (converter == null)
        throw new ArgumentNullException(nameof (converter));
      if (backConverter == null)
        throw new ArgumentNullException(nameof (backConverter));
      this.Converter = converter;
      this.BackConverter = backConverter;
    }

    public IList<TSource> SourceCollection
    {
      get => this._sourceCollection;
      set
      {
        if (this._sourceCollection is INotifyCollectionChanged sourceCollection)
          sourceCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.SourceCollectionOnCollectionChanged);
        if (value is INotifyCollectionChanged collectionChanged)
          collectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SourceCollectionOnCollectionChanged);
        this._sourceCollection = value;
        this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
      }
    }

    private void SourceCollectionOnCollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs sourceArgs)
    {
      try
      {
        NotifyCollectionChangedEventArgs args;
        if (sourceArgs.Action == NotifyCollectionChangedAction.Reset)
          args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset);
        else if (sourceArgs.Action == NotifyCollectionChangedAction.Move)
          args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move);
        else if (sourceArgs.Action == NotifyCollectionChangedAction.Add)
        {
          args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, (IList) sourceArgs.NewItems.Cast<TSource>().Select<TSource, T>(this.Converter).ToList<T>(), sourceArgs.NewStartingIndex);
        }
        else
        {
          if (sourceArgs.Action != NotifyCollectionChangedAction.Remove)
            throw new NotImplementedException();
          args = new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, (IList) sourceArgs.OldItems.Cast<TSource>().Select<TSource, T>(this.Converter).ToList<T>(), sourceArgs.OldStartingIndex);
        }
        this.OnCollectionChanged(args);
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    public void Add(T item) => this.SourceCollection.Add(this.BackConverter(item));

    public void Insert(int index, T item)
    {
      TSource source = this.BackConverter(item);
      this.SourceCollection.Insert(index, source);
    }

    public int IndexOf(T item) => this.SourceCollection.IndexOf(this.BackConverter(item));

    public T this[int index]
    {
      get => this.Converter(this.SourceCollection[index]);
      set => this.SourceCollection[index] = this.BackConverter(value);
    }

    public void Clear() => this.SourceCollection.Clear();

    public bool Contains(T item) => this.SourceCollection.Contains(this.BackConverter(item));

    public void CopyTo(T[] array, int arrayIndex)
    {
      this.SourceCollection.CopyTo(((IEnumerable<T>) array).Select<T, TSource>(this.BackConverter).ToArray<TSource>(), arrayIndex);
    }

    public int Count
    {
      get
      {
        IList<TSource> sourceCollection = this.SourceCollection;
        return sourceCollection != null ? sourceCollection.Count : 0;
      }
    }

    public bool IsReadOnly => true;

    public bool Remove(T item)
    {
      TSource objA = this.BackConverter(item);
      return !object.Equals((object) objA, (object) null) && this.SourceCollection.Remove(objA);
    }

    public void RemoveAt(int index) => this.SourceCollection.RemoveAt(index);

    public IEnumerator<T> GetEnumerator()
    {
      return this.SourceCollection.Select<TSource, T>(this.Converter).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.GetEnumerator();

    public event NotifyCollectionChangedEventHandler CollectionChanged
    {
      add
      {
        CoreDispatcher dispatcher = Window.Current.CoreWindow.Dispatcher;
        if (dispatcher == null && Debugger.IsAttached)
          Debugger.Break();
        if (this._collectionChanged.ContainsKey(value))
          return;
        this._collectionChanged.Add(value, dispatcher);
      }
      remove => this._collectionChanged.Remove(value);
    }

    private async void OnCollectionChanged(NotifyCollectionChangedEventArgs args)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      AutoConvertingObservableCollection<T, TSource>.\u003C\u003Ec__DisplayClass35_1 cDisplayClass351 = new AutoConvertingObservableCollection<T, TSource>.\u003C\u003Ec__DisplayClass35_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass351.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass351.args = args;
      List<KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher>> list;
      lock (this._collectionChanged)
        list = this._collectionChanged.ToList<KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher>>();
      foreach (KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher> keyValuePair in list)
      {
        CoreDispatcher coreDispatcher = keyValuePair.Value;
        if (coreDispatcher != null)
        {
          if (coreDispatcher.HasThreadAccess)
          {
            // ISSUE: reference to a compiler-generated field
            keyValuePair.Key((object) this, cDisplayClass351.args);
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            await coreDispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) new AutoConvertingObservableCollection<T, TSource>.\u003C\u003Ec__DisplayClass35_0()
            {
              CS\u0024\u003C\u003E8__locals1 = cDisplayClass351,
              item1 = keyValuePair
            }, __methodptr(\u003COnCollectionChanged\u003Eb__0)));
          }
        }
      }
      this.OnPropertyChanged("Count");
    }

    int IList.Add(object value)
    {
      this.Add((T) value);
      return this.Count - 1;
    }

    void IList.Clear() => this.Clear();

    bool IList.Contains(object value) => this.Contains((T) value);

    int IList.IndexOf(object value) => value is T obj ? this.IndexOf(obj) : -1;

    void IList.Insert(int index, object value) => this.Insert(index, (T) value);

    bool IList.IsFixedSize => false;

    bool IList.IsReadOnly => false;

    void IList.Remove(object value) => this.Remove((T) value);

    void IList.RemoveAt(int index) => this.RemoveAt(index);

    object IList.this[int index]
    {
      get => (object) this[index];
      set => this[index] = (T) value;
    }

    void ICollection.CopyTo(Array array, int index) => this.CopyTo((T[]) array, index);

    int ICollection.Count => this.Count;

    bool ICollection.IsSynchronized => false;

    object ICollection.SyncRoot => throw new NotImplementedException();
  }
}
