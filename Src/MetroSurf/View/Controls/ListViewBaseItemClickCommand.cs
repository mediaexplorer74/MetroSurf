// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.ListViewBaseItemClickCommand
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace VPN.View.Controls
{
  public class ListViewBaseItemClickCommand
  {
    public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof (ICommand), typeof (ListViewBaseItemClickCommand), new PropertyMetadata((object) null, new PropertyChangedCallback(ListViewBaseItemClickCommand.CommandPropertyChanged)));

    public static void SetCommand(DependencyObject attached, ICommand value)
    {
      attached.SetValue(ListViewBaseItemClickCommand.CommandProperty, (object) value);
    }

    public static ICommand GetCommand(DependencyObject attached)
    {
      return (ICommand) attached.GetValue(ListViewBaseItemClickCommand.CommandProperty);
    }

    private static void CommandPropertyChanged(
      DependencyObject d,
      DependencyPropertyChangedEventArgs e)
    {
      ListViewBase listViewBase = (ListViewBase) d;
      WindowsRuntimeMarshal.AddEventHandler<ItemClickEventHandler>(new Func<ItemClickEventHandler, EventRegistrationToken>(listViewBase.add_ItemClick), new Action<EventRegistrationToken>(listViewBase.remove_ItemClick), new ItemClickEventHandler(ListViewBaseItemClickCommand.ListViewBaseItemClick));
    }

    private static void ListViewBaseItemClick(object sender, ItemClickEventArgs e)
    {
      ListViewBaseItemClickCommand.GetCommand((DependencyObject) sender).Execute(e.ClickedItem);
    }
  }
}
