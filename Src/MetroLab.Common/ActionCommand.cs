// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.ActionCommand
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Windows.Input;

#nullable disable
namespace MetroLab.Common
{
  public class ActionCommand : BindableBase, ICommand
  {
    private bool _isCanExecute = true;

    public bool IsCanExecute
    {
      get => this._isCanExecute;
      set
      {
        if (this._isCanExecute == value)
          return;
        this.SetProperty<bool>(ref this._isCanExecute, value, nameof (IsCanExecute));
        if (this.CanExecuteChanged == null)
          return;
        this.CanExecuteChanged((object) this, (EventArgs) null);
      }
    }

    public event Action<object> ExecuteAction;

    public bool CanExecute(object parameter) => this.IsCanExecute;

    public event EventHandler CanExecuteChanged;

    public void Execute(object parameter)
    {
      if (this.ExecuteAction == null)
        return;
      this.ExecuteAction(parameter);
    }
  }
}
