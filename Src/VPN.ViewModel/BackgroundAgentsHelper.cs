// Decompiled with JetBrains decompiler
// Type: VPN.ViewModel.BackgroundAgentsHelper
// Assembly: VPN.ViewModel, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 71614725-2E26-43C3-AAA3-3648952758ED
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.ViewModel.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

#nullable disable
namespace VPN.ViewModel
{
  public static class BackgroundAgentsHelper
  {
    private const string TaskEntryPoint = "VPN.BackgroundAgent.UpdateApplicationTileTask";
    private const string OnInternetUpdater = "VPN on internet available task";
    private const string OnMaintenanceUpdater = "VPN on maintenance timer task";

    public static async Task RegisterUpdateApplicationTileAgent()
    {
      foreach (KeyValuePair<Guid, IBackgroundTaskRegistration> allTask in (IEnumerable<KeyValuePair<Guid, IBackgroundTaskRegistration>>) BackgroundTaskRegistration.AllTasks)
      {
        string name = allTask.Value.Name;
        if (name == "VPN on internet available task" || name == "VPN on maintenance timer task")
          allTask.Value.Unregister(true);
      }
      if (BackgroundTaskRegistration.AllTasks.Values.FirstOrDefault<IBackgroundTaskRegistration>((Func<IBackgroundTaskRegistration, bool>) (i => i.Name == "VPN on internet available task")) != null)
        return;
      await BackgroundAgentsHelper.RegisterUpdateAppTileTask("VPN on internet available task", (IBackgroundTrigger) new SystemTrigger((SystemTriggerType) 6, false), false);
    }

    private static async Task RegisterUpdateAppTileTask(
      string name,
      IBackgroundTrigger trigger,
      bool isWithInternetCondition)
    {
      BackgroundAccessStatus backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
      BackgroundTaskBuilder backgroundTaskBuilder1 = new BackgroundTaskBuilder();
      backgroundTaskBuilder1.put_Name(name);
      backgroundTaskBuilder1.put_TaskEntryPoint("VPN.BackgroundAgent.UpdateApplicationTileTask");
      BackgroundTaskBuilder backgroundTaskBuilder2 = backgroundTaskBuilder1;
      backgroundTaskBuilder2.SetTrigger(trigger);
      if (isWithInternetCondition)
        backgroundTaskBuilder2.AddCondition((IBackgroundCondition) new SystemCondition((SystemConditionType) 3));
      backgroundTaskBuilder2.Register();
    }
  }
}
