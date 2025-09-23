// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.BaseViewModel
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using MetroLog;
using NotificationsExtensions.ToastContent;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using VPN.Localization;

#nullable disable
namespace MetroLab.Common
{
  [DataContract]
  [KnownType(typeof (DataLoadViewModel))]
  public abstract class BaseViewModel : BindableBase
  {
    private Guid _guid = Guid.NewGuid();
    private static readonly SemaphoreSlim MessageBoxSemaphore = new SemaphoreSlim(1);

    protected BaseViewModel()
    {
      this.Log = LogManagerFactory.DefaultLogManager.GetLogger(this.GetType());
    }

    [IgnoreDataMember]
    protected ILogger Log { get; set; }

    [DataMember]
    public Guid Guid
    {
      get => this._guid;
      set => this.SetProperty<Guid>(ref this._guid, value, nameof (Guid));
    }

    public static async Task ShowDialogAsync(
      string message,
      string title = "",
      UICommandInvokedHandler dialogOkAction = null,
      string dialogOkTitle = null)
    {
      await BaseViewModel.MessageBoxSemaphore.WaitAsync();
      try
      {
        var main = await AppViewModel.GetMainAsync();
        await main.RunAtDispatcherAsync(new DispatchedHandler(async () =>
        {
          var md = new MessageDialog(message, title);
          string okTitle = string.IsNullOrEmpty(dialogOkTitle) ? ResourceLoader.GetForViewIndependentUse().GetString("S_OK") ?? "OK" : dialogOkTitle;
          md.Commands.Add(new UICommand(okTitle));
          var result = await md.ShowAsync();
          dialogOkAction?.Invoke(result);
        }));
      }
      finally
      {
        BaseViewModel.MessageBoxSemaphore.Release();
      }
    }

    public static async Task ShowOkCancelDialogAsync(
      string message,
      string title = "",
      UICommandInvokedHandler dialogOkAction = null,
      string dialogOkTitle = null,
      string dialogCancelTitle = null)
    {
      await BaseViewModel.MessageBoxSemaphore.WaitAsync();
      try
      {
        var main = await AppViewModel.GetMainAsync();
        await main.RunAtDispatcherAsync(new DispatchedHandler(async () =>
        {
          var md = new MessageDialog(message, title);
          string okTitle = string.IsNullOrEmpty(dialogOkTitle) ? ResourceLoader.GetForViewIndependentUse().GetString("S_OK") ?? "OK" : dialogOkTitle;
          string cancelTitle = string.IsNullOrEmpty(dialogCancelTitle) ? ResourceLoader.GetForViewIndependentUse().GetString("S_CANCEL") ?? "Cancel" : dialogCancelTitle;
          md.Commands.Add(new UICommand(okTitle));
          md.Commands.Add(new UICommand(cancelTitle));
          var result = await md.ShowAsync();
          if (result != null && result.Label == okTitle)
            dialogOkAction?.Invoke(result);
        }));
      }
      finally
      {
        BaseViewModel.MessageBoxSemaphore.Release();
      }
    }

    public static void ShowToast(
      string textHeading,
      string textBody,
      TypedEventHandler<ToastNotification, object> onActivatedHandler = null)
    {
      IToastText02 toastText02 = ToastContentFactory.CreateToastText02();
      toastText02.TextHeading.Text = textHeading;
      toastText02.TextBodyWrap.Text = textBody;
      ToastNotification notification = toastText02.CreateNotification();
      if (onActivatedHandler != null)
      {
        notification.Activated += onActivatedHandler;
      }
      ToastNotificationManager.CreateToastNotifier().Show(notification);
    }

    public static ICommand GetCommand(
      ref ICommand command,
      Action<object> action,
      bool isCanExecute = true)
    {
      if (command != null)
        return command;
      ActionCommand actionCommand = new ActionCommand()
      {
        IsCanExecute = isCanExecute
      };
      actionCommand.ExecuteAction += action;
      command = (ICommand) actionCommand;
      return command;
    }
  }
}
