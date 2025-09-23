// Decompiled with JetBrains decompiler
// Type: MetroLog.LazyFlushManager
// Assembly: MetroLog.Platform, Version=0.8.8.0, Culture=neutral, PublicKeyToken=ba4ace74c3b410f3
// MVID: 47301DAE-21E2-4CF0-9219-C7AA5F04B757
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLog.Platform.dll

using MetroLog.Targets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.System.Threading;
using Windows.UI.Xaml;

#nullable disable
namespace MetroLog
{
  public class LazyFlushManager
  {
    private object _lock = new object();

    private ILogManager Owner { get; set; }

    private List<ILazyFlushable> Clients { get; set; }

    private ThreadPoolTimer Timer { get; set; }

    private static Dictionary<ILogManager, LazyFlushManager> Owners { get; set; }

    private LazyFlushManager(ILogManager owner)
    {
      this.Owner = owner;
      this.Owner.LoggerCreated += new EventHandler<LoggerEventArgs>(this.Owner_LoggerCreated);
      this.Clients = new List<ILazyFlushable>();
      this.Timer = ThreadPoolTimer.CreatePeriodicTimer((t) => { _ = this.LazyFlushAsync(new LogWriteContext()); }, TimeSpan.FromMinutes(2.0));
    }

    private void Owner_LoggerCreated(object sender, LoggerEventArgs e)
    {
      lock (this._lock)
      {
        foreach (Target target in ((ILoggerQuery) e.Logger).GetTargets())
        {
          if (target is ILazyFlushable && !this.Clients.Contains((ILazyFlushable) target))
            this.Clients.Add((ILazyFlushable) target);
        }
      }
    }

    static LazyFlushManager()
    {
      LazyFlushManager.Owners = new Dictionary<ILogManager, LazyFlushManager>();
      if (LoggingEnvironment.XamlApplicationState != XamlApplicationState.Available)
        return;
      Application current = Application.Current;
      WindowsRuntimeMarshal.AddEventHandler<SuspendingEventHandler>(new Func<SuspendingEventHandler, EventRegistrationToken>(current.add_Suspending), 
          new Action<EventRegistrationToken>(current.remove_Suspending), new SuspendingEventHandler(LazyFlushManager.Current_Suspending));
    }

    private static async void Current_Suspending(object sender, SuspendingEventArgs e)
    {
      await LazyFlushManager.FlushAllAsync(new LogWriteContext());
    }

    public static async Task FlushAllAsync(LogWriteContext context)
    {
      List<Task> taskList = new List<Task>();
      foreach (LazyFlushManager lazyFlushManager in LazyFlushManager.Owners.Values)
        taskList.Add(lazyFlushManager.LazyFlushAsync(context));
      await Task.WhenAll((IEnumerable<Task>) taskList);
    }

    private async Task LazyFlushAsync(LogWriteContext context)
    {
      List<ILazyFlushable> source = (List<ILazyFlushable>) null;
      lock (this._lock)
        source = new List<ILazyFlushable>((IEnumerable<ILazyFlushable>) this.Clients);
      if (!source.Any<ILazyFlushable>())
        return;
      await Task.WhenAll((IEnumerable<Task>) source.Select<ILazyFlushable, Task>((Func<ILazyFlushable, Task>) (client => client.LazyFlushAsync(context))).ToList<Task>());
    }

    internal static void Initialize(ILogManager manager)
    {
      Dictionary<ILogManager, LazyFlushManager> owners = LazyFlushManager.Owners;
      ILogManager logManager = manager;
      LazyFlushManager lazyFlushManager = new LazyFlushManager(logManager);
      owners[logManager] = lazyFlushManager;
    }
  }
}
