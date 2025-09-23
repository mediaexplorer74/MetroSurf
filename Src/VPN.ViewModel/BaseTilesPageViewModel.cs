// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.BaseTilesPageViewModel
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using MetroLab.Common;
using System;
using System.Runtime.Serialization;
using System.Windows.Input;
using VPN.ViewModel.Items;

#nullable disable
namespace VPN.ViewModel
{
  [DataContract]
  public abstract class BaseTilesPageViewModel : PageViewModel
  {
    private ICommand _tileCommand;

    [IgnoreDataMember]
    public ICommand TileCommand
    {
      get => BaseViewModel.GetCommand(ref this._tileCommand, new Action<object>(this.TileAction));
    }

    protected virtual void TileAction(object clickedItem)
    {
      if (!(clickedItem is TileItemViewModel tileItemViewModel) || tileItemViewModel.TileCommand == null)
        return;
      tileItemViewModel.TileCommand.Execute(clickedItem);
    }
  }
}
