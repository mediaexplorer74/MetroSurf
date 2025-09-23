// Decompiled with JetBrains decompiler
// Type: VPN.View.LoginKeepSolidIDPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

#nullable disable
namespace VPN.View
{
  public sealed partial class LoginKeepSolidIDPage : MvvmPage
  {
    public LoginKeepSolidIDPage()
    {
      this.InitializeComponent();
    }

    private void LoginTextBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != Windows.System.VirtualKey.Enter)
        return;
      this.PasswordBox.Focus(FocusState.Programmatic);
    }

    private void PasswordBoxOnKeyUp(object sender, KeyRoutedEventArgs e)
    {
      if (e.Key != Windows.System.VirtualKey.Enter)
        return;
      InputPane.GetForCurrentView().TryHide();
    }
  }
}
