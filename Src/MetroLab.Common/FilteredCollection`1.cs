// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.FilteredCollection`1
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

#nullable disable
namespace MetroLab.Common
{
  public class FilteredCollection<T> : INotifyPropertyChanged
  {
    private ObservableCollection<T> _result;
    private Func<T, bool> _filter;

    public ObservableCollection<T> Result
    {
      get => this._result;
      set
      {
        this._result = value;
        this.OnPropertyChanged(nameof (Result));
      }
    }

    public IList<T> SourceCollection { get; private set; }

    public Func<T, bool> Filter
    {
      get => this._filter;
      set
      {
        this._filter = value;
        this.UpdateFilter();
      }
    }

    public void UpdateFilter()
    {
      ObservableCollection<T> observableCollection = new ObservableCollection<T>(this.SourceCollection.Where<T>(this.Filter));
      if (this.Result == null)
        this.Result = observableCollection;
      int index1 = -1;
      foreach (T obj in (Collection<T>) observableCollection)
      {
        int num = this.Result.IndexOf(obj);
        if (num < 0)
        {
          ++index1;
          if (index1 < this.Result.Count)
            this.Result.Insert(index1, obj);
          else
            this.Result.Add(obj);
        }
        else
        {
          ++index1;
          for (int index2 = index1; index2 < num; ++index2)
            this.Result.RemoveAt(index1);
        }
      }
      while (this.Result.Count > index1 + 1)
        this.Result.RemoveAt(this.Result.Count - 1);
    }

    public FilteredCollection(IList<T> sourceCollection)
    {
      this.SourceCollection = sourceCollection;
      if (!(sourceCollection is INotifyCollectionChanged collectionChanged))
        return;
      collectionChanged.CollectionChanged += new NotifyCollectionChangedEventHandler(this.SourceCollectionOnCollectionChanged);
    }

    private void SourceCollectionOnCollectionChanged(
      object sender,
      NotifyCollectionChangedEventArgs args)
    {
      this.UpdateFilter();
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
      if (this.PropertyChanged == null)
        return;
      this.PropertyChanged((object) this, new PropertyChangedEventArgs(propertyName));
    }
  }
}
