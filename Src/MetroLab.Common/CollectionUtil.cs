// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.CollectionUtil
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.Collections.ObjectModel;

#nullable disable
namespace MetroLab.Common
{
  public class CollectionUtil
  {
    public static void UpdateCollection<T>(
      ref ObservableCollection<T> targetCollection,
      ObservableCollection<T> nextCollection)
    {
      if (targetCollection == null)
      {
        targetCollection = nextCollection;
      }
      else
      {
        int index1 = -1;
        foreach (T next in (Collection<T>) nextCollection)
        {
          int num = targetCollection.IndexOf(next);
          if (num < 0)
          {
            ++index1;
            if (index1 < targetCollection.Count)
              targetCollection.Insert(index1, next);
            else
              targetCollection.Add(next);
          }
          else
          {
            ++index1;
            for (int index2 = index1; index2 < num; ++index2)
              targetCollection.RemoveAt(index1);
          }
        }
        while (targetCollection.Count > index1 + 1)
          targetCollection.RemoveAt(targetCollection.Count - 1);
      }
    }

    public static void UpdateCollection<T>(
      ref MTObservableCollection<T> targetCollection,
      MTObservableCollection<T> nextCollection)
    {
      if (targetCollection == null)
      {
        targetCollection = nextCollection;
      }
      else
      {
        int index1 = -1;
        foreach (T next in (Collection<T>) nextCollection)
        {
          int num = targetCollection.IndexOf(next);
          if (num < 0)
          {
            ++index1;
            if (index1 < targetCollection.Count)
              targetCollection.Insert(index1, next);
            else
              targetCollection.Add(next);
          }
          else
          {
            ++index1;
            for (int index2 = index1; index2 < num; ++index2)
              targetCollection.RemoveAt(index1);
          }
        }
        while (targetCollection.Count > index1 + 1)
          targetCollection.RemoveAt(targetCollection.Count - 1);
      }
    }
  }
}
