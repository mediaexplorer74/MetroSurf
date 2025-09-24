// Decompiled with JetBrains decompiler
// Type: MetroLog.GlobalCrashHandler
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using System;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLog
{
  public static class GlobalCrashHandler
  {
    public static void Configure()
    {
      Application current = Application.Current;
      current.UnhandledException += GlobalCrashHandler.App_UnhandledException;
    }

    private static async void App_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
      Application.Current.UnhandledException -= GlobalCrashHandler.App_UnhandledException;
      e.Handled = true;
      LogWriteOperation[] logWriteOperationArray = await ((ILoggerAsync) LogManagerFactory.DefaultLogManager.GetLogger<Application>()).FatalAsync("The application crashed: " + e.Message, (object) e);
      await LazyFlushManager.FlushAllAsync(new LogWriteContext());
      Application.Current.Exit();
    }
  }
}
