// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.MTObservableCollection`1
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Core;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLab.Common
{
  public class MTObservableCollection<T> : ObservableCollection<T>
  {
    private readonly Dictionary<NotifyCollectionChangedEventHandler, CoreDispatcher> _collectionChanged = new Dictionary<NotifyCollectionChangedEventHandler, CoreDispatcher>();
    private readonly Dictionary<PropertyChangedEventHandler, CoreDispatcher> _propertyChanged = new Dictionary<PropertyChangedEventHandler, CoreDispatcher>();

    public override event NotifyCollectionChangedEventHandler CollectionChanged
    {
      add
      {
        CoreDispatcher coreDispatcher = (CoreDispatcher) null;
        if (Window.Current != null && Window.Current.CoreWindow != null)
          coreDispatcher = Window.Current.CoreWindow.Dispatcher;
        if (coreDispatcher == null && Debugger.IsAttached)
          Debugger.Break();
        if (this._collectionChanged.ContainsKey(value))
          return;
        this._collectionChanged.Add(value, coreDispatcher);
      }
      remove => this._collectionChanged.Remove(value);
    }

    public MTObservableCollection()
    {
    }

    public MTObservableCollection(IEnumerable<T> collection)
      : base(collection)
    {
    }

    protected override async void OnCollectionChanged(NotifyCollectionChangedEventArgs eventArgs)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MTObservableCollection<T>.\u003C\u003Ec__DisplayClass6_1 cDisplayClass61 = new MTObservableCollection<T>.\u003C\u003Ec__DisplayClass6_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass61.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass61.eventArgs = eventArgs;
      this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
      List<KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher>> list;
      lock (this._collectionChanged)
        list = this._collectionChanged.ToList<KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher>>();
      foreach (KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher> keyValuePair in list)
      {
        CoreDispatcher coreDispatcher = keyValuePair.Value;
        if (coreDispatcher == null)
        {
          try
          {
            // ISSUE: reference to a compiler-generated field
            keyValuePair.Key((object) this, cDisplayClass61.eventArgs);
          }
          catch (Exception ex)
          {
            if (Debugger.IsAttached)
              Debugger.Break();
          }
        }
        else if (coreDispatcher.HasThreadAccess)
        {
          // ISSUE: reference to a compiler-generated field
          keyValuePair.Key((object) this, cDisplayClass61.eventArgs);
        }
        else
        {
          // ISSUE: object of a compiler-generated type is created
          // ISSUE: method pointer
          await coreDispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) new MTObservableCollection<T>.\u003C\u003Ec__DisplayClass6_0()
          {
            CS\u0024\u003C\u003E8__locals1 = cDisplayClass61,
            item1 = keyValuePair
          }, __methodptr(\u003COnCollectionChanged\u003Eb__0)));
        }
      }
    }

    protected override event PropertyChangedEventHandler PropertyChanged
    {
      add
      {
        CoreDispatcher dispatcher = Window.Current.CoreWindow.Dispatcher;
        if (dispatcher == null && Debugger.IsAttached)
          Debugger.Break();
        lock (this._propertyChanged)
        {
          if (this._propertyChanged.ContainsKey(value))
            return;
          this._propertyChanged.Add(value, dispatcher);
        }
      }
      remove
      {
        lock (this._propertyChanged)
          this._propertyChanged.Remove(value);
      }
    }

    protected override async void OnPropertyChanged(PropertyChangedEventArgs eventArgs)
    {
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      MTObservableCollection<T>.\u003C\u003Ec__DisplayClass11_1 cDisplayClass111 = new MTObservableCollection<T>.\u003C\u003Ec__DisplayClass11_1();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass111.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass111.eventArgs = eventArgs;
      List<KeyValuePair<PropertyChangedEventHandler, CoreDispatcher>> list;
      lock (this._propertyChanged)
        list = this._propertyChanged.ToList<KeyValuePair<PropertyChangedEventHandler, CoreDispatcher>>();
      foreach (KeyValuePair<PropertyChangedEventHandler, CoreDispatcher> keyValuePair in list)
      {
        CoreDispatcher coreDispatcher = keyValuePair.Value;
        if (coreDispatcher != null)
        {
          if (coreDispatcher.HasThreadAccess)
          {
            // ISSUE: reference to a compiler-generated field
            keyValuePair.Key((object) this, cDisplayClass111.eventArgs);
          }
          else
          {
            // ISSUE: object of a compiler-generated type is created
            // ISSUE: method pointer
            await coreDispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) new MTObservableCollection<T>.\u003C\u003Ec__DisplayClass11_0()
            {
              CS\u0024\u003C\u003E8__locals1 = cDisplayClass111,
              item1 = keyValuePair
            }, __methodptr(\u003COnPropertyChanged\u003Eb__0)));
          }
        }
      }
    }
  }
}
