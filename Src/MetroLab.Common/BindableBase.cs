// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.BindableBase
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Windows.ApplicationModel.Core;
using Windows.Foundation.Metadata;
using Windows.UI.Core;

#nullable disable
namespace MetroLab.Common
{
  [WebHostHidden]
  [DataContract]
  public abstract class BindableBase : INotifyPropertyChanged
  {
    public event PropertyChangedEventHandler PropertyChanged;

    protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
    {
      if (object.Equals((object) storage, (object) value))
        return false;
      storage = value;
      this.OnPropertyChanged(propertyName);
      return true;
    }

    public void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
      var handler = this.PropertyChanged;
      if (handler == null)
        return;

      CoreDispatcher dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
      var args = new PropertyChangedEventArgs(propertyName);
      if (dispatcher != null && !dispatcher.HasThreadAccess)
      {
        // marshal to UI thread
        var capturedHandler = handler;
        dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(() =>
        {
          try { capturedHandler(this, args); } catch { }
        }));
      }
      else
      {
        try { handler(this, args); } catch { }
      }
    }
  }
}
