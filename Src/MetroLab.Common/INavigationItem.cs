// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.INavigationItem
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace MetroLab.Common
{
  public interface INavigationItem : IDisposable
  {
    Page GetPage();

    WeakReference<Page> GetPageReference();

    IPageViewModel PageViewModel { get; }
  }
}
