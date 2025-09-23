// Decompiled with JetBrains decompiler
// Type: MetroLog.XamlExtensionMethods
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using Windows.UI.Xaml.Controls;

#nullable disable
namespace MetroLog
{
  public static class XamlExtensionMethods
  {
    public static ILogger GetLogger(this UserControl control, LoggingConfiguration config = null)
    {
      return LogManagerFactory.DefaultLogManager.GetLogger(control.GetType(), config);
    }
  }
}
