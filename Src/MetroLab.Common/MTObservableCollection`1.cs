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
      this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
      List<KeyValuePair<NotifyCollectionChangedEventHandler, CoreDispatcher>> list;
      lock (this._collectionChanged)
        list = this._collectionChanged.ToList();

      foreach (var kv in list)
      {
        var handler = kv.Key;
        var dispatcher = kv.Value;
        if (dispatcher == null)
        {
          try { handler(this, eventArgs); } catch { if (Debugger.IsAttached) Debugger.Break(); }
        }
        else if (dispatcher.HasThreadAccess)
        {
          try { handler(this, eventArgs); } catch { if (Debugger.IsAttached) Debugger.Break(); }
        }
        else
        {
          var capturedHandler = handler;
          var capturedArgs = eventArgs;
          await dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
          {
            try { capturedHandler(this, capturedArgs); } catch { if (Debugger.IsAttached) Debugger.Break(); }
          }));
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
      List<KeyValuePair<PropertyChangedEventHandler, CoreDispatcher>> list;
      lock (this._propertyChanged)
        list = this._propertyChanged.ToList();

      foreach (var kv in list)
      {
        var handler = kv.Key;
        var dispatcher = kv.Value;
        if (dispatcher == null)
        {
          try { handler(this, eventArgs); } catch { if (Debugger.IsAttached) Debugger.Break(); }
        }
        else if (dispatcher.HasThreadAccess)
        {
          try { handler(this, eventArgs); } catch { if (Debugger.IsAttached) Debugger.Break(); }
        }
        else
        {
          var capHandler = handler;
          var capArgs = eventArgs;
          await dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
          {
            try { capHandler(this, capArgs); } catch { if (Debugger.IsAttached) Debugger.Break(); }
          }));
        }
      }
    }
  }
}
