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
      // ISSUE: object of a compiler-generated type is created
      // ISSUE: variable of a compiler-generated type
      BindableBase.\u003C\u003Ec__DisplayClass4_0 cDisplayClass40 = new BindableBase.\u003C\u003Ec__DisplayClass4_0();
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.\u003C\u003E4__this = this;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.propertyName = propertyName;
      // ISSUE: reference to a compiler-generated field
      cDisplayClass40.handler = this.PropertyChanged;
      // ISSUE: reference to a compiler-generated field
      if (cDisplayClass40.handler == null)
        return;
      CoreDispatcher dispatcher = CoreApplication.MainView.CoreWindow.Dispatcher;
      if (dispatcher.HasThreadAccess)
      {
        // ISSUE: reference to a compiler-generated field
        // ISSUE: reference to a compiler-generated field
        cDisplayClass40.handler((object) this, new PropertyChangedEventArgs(cDisplayClass40.propertyName));
      }
      else
      {
        // ISSUE: method pointer
        dispatcher.RunAsync((CoreDispatcherPriority) 0, new DispatchedHandler((object) cDisplayClass40, __methodptr(\u003COnPropertyChanged\u003Eb__0)));
      }
    }
  }
}
