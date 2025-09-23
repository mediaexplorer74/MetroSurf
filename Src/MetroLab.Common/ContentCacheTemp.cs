// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ContentCacheTemp
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLab.Common
{
  public class ContentCacheTemp : ContentCache
  {
    private static volatile ContentCacheTemp _current;
    public static readonly DependencyProperty SourceUrlTempProperty = DependencyProperty.RegisterAttached("SourceUrlTemp", typeof (string), typeof (ContentCache), new PropertyMetadata((object) null, new PropertyChangedCallback(((ContentCache) ContentCacheTemp.Current).OnSourceUriPropertyChanged)));

    protected override string BinaryCacheFolderName => "BinaryCacheTemp";

    protected override int ThreadCount => 1;

    public static ContentCacheTemp Current
    {
      get => ContentCacheTemp._current ?? (ContentCacheTemp._current = new ContentCacheTemp());
    }

    public static string GetSourceUrlTemp(DependencyObject frameworkControl)
    {
      return (string) frameworkControl.GetValue(ContentCacheTemp.SourceUrlTempProperty);
    }

    public static void SetSourceUrlTemp(DependencyObject frameworkControl, string value)
    {
      frameworkControl.SetValue(ContentCacheTemp.SourceUrlTempProperty, (object) value);
    }

    private ContentCacheTemp()
    {
    }

    public async Task ClearCachedFiles()
    {
      try
      {
        StorageFolder folderAsync = await ApplicationData.Current.LocalFolder.CreateFolderAsync(this.BinaryCacheFolderName, (CreationCollisionOption) 1);
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }
  }
}
