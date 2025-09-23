// Decompiled with JetBrains decompiler
// Type: VPN.View.InviteFriendPage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using MetroLab.Common;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using VPN.ViewModel;
using Windows.ApplicationModel.Contacts;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace VPN.View
{
  public sealed partial class InviteFriendPage : MvvmPage
  {
    public InviteFriendPage()
    {
      this.InitializeComponent();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
      base.OnNavigatedTo(e);
    }

    private async void InviteFriendFromContacts(object sender, RoutedEventArgs e)
    {
      ContactPicker contactPicker = new ContactPicker();
      contactPicker.DesiredFieldsWithContactFieldType.Add(ContactFieldType.Email);
      Contact contact = await contactPicker.PickContactAsync();
      if (contact == null)
        return;
      await ((InviteFriendPageViewModel) this.ViewModel).InviteFriendAsync(contact.Emails[0].Address);
    }
  }
}
