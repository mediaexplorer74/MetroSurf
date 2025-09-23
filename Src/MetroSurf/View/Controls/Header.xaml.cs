// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.Header
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.Reflection;
using VPN.Localization;
using VPN.ViewModel.Http;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace VPN.View.Controls
{
    public sealed partial class Header : UserControl
    {
        public static readonly DependencyProperty IsStatusNeededProperty = DependencyProperty.Register(nameof(IsStatusNeeded), typeof(bool), typeof(Header), new PropertyMetadata((object)null));
        public static readonly DependencyProperty IsStatusBarColorNeededProperty = DependencyProperty.Register(nameof(IsStatusBarColorNeeded), typeof(bool), typeof(Header), new PropertyMetadata((object)null, new PropertyChangedCallback(Header.OnOrOffPropertyChnaged)));
        public static readonly DependencyProperty OnOrOffProperty = DependencyProperty.Register(nameof(OnOrOff), typeof(bool), typeof(Header), new PropertyMetadata((object)null, new PropertyChangedCallback(Header.OnOrOffPropertyChnaged)));


        public bool IsStatusNeeded
        {
            get => (bool)this.GetValue(Header.IsStatusNeededProperty);
            set => this.SetValue(Header.IsStatusNeededProperty, (object)value);
        }

        public bool IsStatusBarColorNeeded
        {
            get => (bool)this.GetValue(Header.IsStatusBarColorNeededProperty);
            set => this.SetValue(Header.IsStatusBarColorNeededProperty, (object)value);
        }

        public bool OnOrOff
        {
            get => (bool)this.GetValue(Header.OnOrOffProperty);
            set => this.SetValue(Header.OnOrOffProperty, (object)value);
        }

        public static void OnOrOffPropertyChnaged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Header header = d as Header;
            header?.TryUpdateStatusBar();
        }

        public string ApplicationName => LocalizedResources.GetLocalizedString("app_name");

        private void TryUpdateStatusBar()
        {
            try
            {
                // StatusBar exists only on some devices. Load type by name to avoid referencing Windows.UI.ViewManagement.StatusBar directly.
                var statusBarType = Type.GetType("Windows.UI.ViewManagement.StatusBar, Windows");
                if (statusBarType == null)
                    return;
                var getForCurrentView = statusBarType.GetMethod("GetForCurrentView", BindingFlags.Public | BindingFlags.Static);
                var statusBar = getForCurrentView?.Invoke(null, null);
                if (statusBar == null)
                    return;

                // Set BackgroundOpacity = 1.0 if available
                var backgroundOpacityProp = statusBarType.GetProperty("BackgroundOpacity");
                if (backgroundOpacityProp != null && backgroundOpacityProp.CanWrite)
                    backgroundOpacityProp.SetValue(statusBar, 1.0, null);

                // Setting colors requires Windows.UI.Color; skip color assignment to avoid complex runtime type construction.
            }
            catch
            {
                // ignore if status bar APIs are not available or reflection fails
            }
        }
    }
}
