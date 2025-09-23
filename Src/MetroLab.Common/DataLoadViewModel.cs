// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.DataLoadViewModel
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.UI.Popups;

#nullable disable
namespace MetroLab.Common
{
  [DataContract]
  public class DataLoadViewModel : BaseViewModel, IDataLoadViewModel
  {
    private string _alertMessage = string.Empty;
    private string _alertHeader;
    private bool _isButtonTryAgainVisible;
    private string _loadingMessage = CommonLocalizedResources.GetLocalizedString("Loading");
    private bool _isWasProblemsWithConnection;
    private bool _isWasErrorOnLoading;
    private bool _isLoadedSuccessfully;
    private bool _isInitializeLoadingActive = true;
    private string _errorMessage;
    private bool _isLoadingActive = true;
    private bool _isUpdatingActive;
    private int _activeUpdatingLoadingsCount;
    private bool _isWorkingOffline;
    private string _noInternetMessage;
    private string _noInternetHeader;
    private string _loadFailedMessage;
    private string _loadFailedHeader;
    private string _dialogOkButtonTitle;
    private List<WeakReference<ILoadedOrUpdatedSuccessfullyListener>> _loadedOrUpdatedSuccessfullyListers = new List<WeakReference<ILoadedOrUpdatedSuccessfullyListener>>();
    private Task<bool> _loadingTask;
    private Task _initializeTask;
    private Task<CacheState> _tryLoadFromCacheTask;
    protected bool _isContentActual;

    protected DataLoadViewModel()
    {
      WindowsRuntimeMarshal.AddEventHandler<NetworkStatusChangedEventHandler>(new Func<NetworkStatusChangedEventHandler, EventRegistrationToken>(NetworkInformation.add_NetworkStatusChanged), new Action<EventRegistrationToken>(NetworkInformation.remove_NetworkStatusChanged), new NetworkStatusChangedEventHandler(this.NetworkInformationOnNetworkStatusChanged));
    }

    [DataMember]
    public string AlertMessage
    {
      get => this._alertMessage;
      set => this.SetProperty<string>(ref this._alertMessage, value, nameof (AlertMessage));
    }

    [DataMember]
    public string AlertHeader
    {
      get => this._alertHeader;
      set => this.SetProperty<string>(ref this._alertHeader, value, nameof (AlertHeader));
    }

    [DataMember]
    public bool IsButtonTryAgainVisible
    {
      get => this._isButtonTryAgainVisible;
      set
      {
        this.SetProperty<bool>(ref this._isButtonTryAgainVisible, value, nameof (IsButtonTryAgainVisible));
      }
    }

    [DataMember]
    public string LoadingMessage
    {
      get => this._loadingMessage;
      set => this.SetProperty<string>(ref this._loadingMessage, value, nameof (LoadingMessage));
    }

    [DataMember]
    public bool IsWasProblemsWithConnection
    {
      get => this._isWasProblemsWithConnection;
      set
      {
        if (value)
          this.IsWorkingOffline = true;
        if (!this.SetProperty<bool>(ref this._isWasProblemsWithConnection, value, nameof (IsWasProblemsWithConnection)))
          return;
        this.OnPropertyChanged("IsLoadingProblemsMessageVisible");
      }
    }

    public bool IsWasErrorOnLoading
    {
      get => this._isWasErrorOnLoading;
      set
      {
        if (!this.SetProperty<bool>(ref this._isWasErrorOnLoading, value, nameof (IsWasErrorOnLoading)))
          return;
        this.OnPropertyChanged("IsLoadingProblemsMessageVisible");
      }
    }

    [IgnoreDataMember]
    public bool IsLoadedSuccessfully
    {
      get => this._isLoadedSuccessfully;
      set
      {
        this.SetProperty<bool>(ref this._isLoadedSuccessfully, value, nameof (IsLoadedSuccessfully));
      }
    }

    [IgnoreDataMember]
    public bool IsInitializeLoadingActive
    {
      get => this._isInitializeLoadingActive;
      protected set
      {
        if (this.SetProperty<bool>(ref this._isInitializeLoadingActive, value, nameof (IsInitializeLoadingActive)))
          this.IsLoadingActive = value || this.IsUpdatingActive;
        if (!value)
          return;
        this.IsWasProblemsWithConnection = false;
        this.IsWasErrorOnLoading = false;
      }
    }

    [DataMember]
    public string ErrorMessage
    {
      get => this._errorMessage;
      set => this.SetProperty<string>(ref this._errorMessage, value, nameof (ErrorMessage));
    }

    public bool IsLoadingActive
    {
      get => this._isLoadingActive;
      set
      {
        if (!this.SetProperty<bool>(ref this._isLoadingActive, value, nameof (IsLoadingActive)))
          return;
        this.OnPropertyChanged("IsLoadingProblemsMessageVisible");
      }
    }

    [IgnoreDataMember]
    public bool IsUpdatingActive
    {
      get => this._isUpdatingActive;
      private set
      {
        if (!this.SetProperty<bool>(ref this._isUpdatingActive, value, nameof (IsUpdatingActive)))
          return;
        this.IsLoadingActive = value || this.IsInitializeLoadingActive;
        this.OnPropertyChanged("IsLoadingProblemsMessageVisible");
        if (!value)
          return;
        this.IsWasProblemsWithConnection = false;
        this.IsWasErrorOnLoading = false;
        if (this.IsLoadedSuccessfully)
          return;
        this.IsInitializeLoadingActive = true;
      }
    }

    public void BeginDisplayLoadingWhenUpdating()
    {
      if (this._activeUpdatingLoadingsCount < 0)
        this._activeUpdatingLoadingsCount = 0;
      ++this._activeUpdatingLoadingsCount;
      this.IsUpdatingActive = this._activeUpdatingLoadingsCount > 0;
    }

    public void EndDisplayLoadingWhenUpdating()
    {
      --this._activeUpdatingLoadingsCount;
      if (this._activeUpdatingLoadingsCount < 0)
        this._activeUpdatingLoadingsCount = 0;
      this.IsUpdatingActive = this._activeUpdatingLoadingsCount > 0;
    }

    public bool IsWorkingOffline
    {
      get => this._isWorkingOffline;
      set => this.SetProperty<bool>(ref this._isWorkingOffline, value, nameof (IsWorkingOffline));
    }

    [IgnoreDataMember]
    public string WorkingOfflineMessage
    {
      get => CommonLocalizedResources.GetLocalizedString("txt_offline");
    }

    [IgnoreDataMember]
    protected string NoInternetMessage
    {
      get
      {
        return this._noInternetMessage ?? CommonLocalizedResources.GetLocalizedString("exceptionMsg_ServerNotRespondOrConnectionFailure");
      }
      set => this._noInternetMessage = value;
    }

    [IgnoreDataMember]
    protected string NoInternetHeader
    {
      get
      {
        return this._noInternetHeader ?? CommonLocalizedResources.GetLocalizedString("exceptionHeader_ServerNotRespondOrConnectionFailure");
      }
      set => this._noInternetHeader = value;
    }

    [IgnoreDataMember]
    protected string LoadFailedMessage
    {
      get
      {
        return this._loadFailedMessage ?? CommonLocalizedResources.GetLocalizedString("exceptionMsg_LoadFailed");
      }
      set => this._loadFailedMessage = value;
    }

    [IgnoreDataMember]
    protected string LoadFailedHeader
    {
      get
      {
        return this._loadFailedHeader ?? CommonLocalizedResources.GetLocalizedString("exceptionHeader_LoadFailed");
      }
      set => this._loadFailedHeader = value;
    }

    [IgnoreDataMember]
    protected string DialogOkButtonTitle
    {
      get => this._dialogOkButtonTitle ?? CommonLocalizedResources.GetLocalizedString("Ok");
      set => this._dialogOkButtonTitle = value;
    }

    [IgnoreDataMember]
    public bool IsLoadingProblemsMessageVisible
    {
      get
      {
        return (this.IsWasProblemsWithConnection || this.IsWasErrorOnLoading) && !this.IsLoadingActive && !this.IsLoadedSuccessfully;
      }
    }

    public void AddLoadedOrUpdatedSuccessfullyListener(ILoadedOrUpdatedSuccessfullyListener listener)
    {
      if (this.IsLoadedSuccessfully && listener != null)
        listener.OnLoadedOrUpdatedSuccessfully((object) this, EventArgs.Empty);
      if (this._loadedOrUpdatedSuccessfullyListers == null)
        return;
      this._loadedOrUpdatedSuccessfullyListers.Add(new WeakReference<ILoadedOrUpdatedSuccessfullyListener>(listener));
    }

    public void RemoveLoadedOrUpdatedSuccessfullyListener(
      ILoadedOrUpdatedSuccessfullyListener listener)
    {
      for (int index = 0; index < this._loadedOrUpdatedSuccessfullyListers.Count; ++index)
      {
        ILoadedOrUpdatedSuccessfullyListener target;
        if (this._loadedOrUpdatedSuccessfullyListers[index].TryGetTarget(out target) && object.Equals((object) listener, (object) target))
        {
          this._loadedOrUpdatedSuccessfullyListers.RemoveAt(index);
          break;
        }
      }
    }

    public static bool GetIsConnectedToNetwork()
    {
      ConnectionProfile connectionProfile = NetworkInformation.GetInternetConnectionProfile();
      return connectionProfile != null && connectionProfile.GetNetworkConnectivityLevel() == 3;
    }

    public void UpdateGlobalIsWorkingOffline()
    {
      if (DataLoadViewModel.GetIsConnectedToNetwork())
      {
        this.IsWorkingOffline = false;
        if (this.IsLoadedSuccessfully)
          return;
        this.UpdateAsync();
      }
      else
        this.IsWorkingOffline = true;
    }

    private void NetworkInformationOnNetworkStatusChanged(object sender)
    {
      this.UpdateGlobalIsWorkingOffline();
    }

    private void OnLoadedOrUpdatedSuccessfully(EventArgs args)
    {
      foreach (WeakReference<ILoadedOrUpdatedSuccessfullyListener> weakReference in this._loadedOrUpdatedSuccessfullyListers.ToList<WeakReference<ILoadedOrUpdatedSuccessfullyListener>>())
      {
        ILoadedOrUpdatedSuccessfullyListener target;
        if (weakReference.TryGetTarget(out target) && target != null)
          target.OnLoadedOrUpdatedSuccessfully((object) this, args);
      }
    }

    [System.Runtime.Serialization.OnDeserialized]
    public void OnDeserialized(StreamingContext context)
    {
      this._loadedOrUpdatedSuccessfullyListers = new List<WeakReference<ILoadedOrUpdatedSuccessfullyListener>>();
    }

    public virtual void UnSubscribeFromEvents()
    {
      WindowsRuntimeMarshal.RemoveEventHandler<NetworkStatusChangedEventHandler>(new Action<EventRegistrationToken>(NetworkInformation.remove_NetworkStatusChanged), new NetworkStatusChangedEventHandler(this.NetworkInformationOnNetworkStatusChanged));
    }

    protected virtual async Task<bool> LoadAsync()
    {
      bool flag;
      return flag;
    }

    protected Task<bool> TryLoadAsync()
    {
      lock (this)
        return this._loadingTask ?? (this._loadingTask = this.TryLoadAsyncInner((Func<Task<bool>>) (async () => await this.LoadAsync())));
    }

    protected Task<CacheState> TryLoadFromCache()
    {
      return this._tryLoadFromCacheTask ?? (this._tryLoadFromCacheTask = this.TryLoadFromCacheInner());
    }

    private async Task<CacheState> TryLoadFromCacheInner()
    {
      try
      {
        CacheState cacheState = await this.LoadFromCacheAsync();
        this._isContentActual = (cacheState & CacheState.Actual) == CacheState.Actual;
        return cacheState;
      }
      catch (Exception ex)
      {
        return CacheState.Empty;
      }
    }

    protected virtual async Task<CacheState> LoadFromCacheAsync() => CacheState.Empty;

    protected async void RunLoadFailed(
      string alertMessage,
      bool isNeedToShowDialog = true,
      UICommandInvokedHandler dialogOkAction = null,
      string alertHeader = "",
      bool isButtonTryAgainVisible = false)
    {
      this.AlertMessage = alertMessage;
      this.AlertHeader = alertHeader;
      this.IsButtonTryAgainVisible = isButtonTryAgainVisible;
      if (isNeedToShowDialog)
        await BaseViewModel.ShowDialogAsync(this.AlertMessage, string.Empty, dialogOkAction, this.DialogOkButtonTitle);
      this.EndDisplayLoadingWhenUpdating();
    }

    protected virtual async Task<bool> TryLoadAsyncInner(Func<Task<bool>> loadAction)
    {
      try
      {
        return await loadAction();
      }
      catch (WebException ex)
      {
        this.IsWasProblemsWithConnection = true;
        if (ex.Response == null && ((byte) ex.Status == (byte) 1 || (byte) ex.Status == (byte) 2))
          this.RunLoadFailed(this.NoInternetMessage, false, alertHeader: this.NoInternetHeader);
        else
          this.RunLoadFailed(this.LoadFailedMessage, false, alertHeader: this.LoadFailedHeader, isButtonTryAgainVisible: true);
        return false;
      }
      catch (HttpRequestException ex)
      {
        this.IsWasProblemsWithConnection = true;
        if (ex.InnerException is WebException innerException && innerException.Response == null && ((byte) innerException.Status == (byte) 1 || (byte) innerException.Status == (byte) 2))
          this.RunLoadFailed(this.NoInternetMessage, false, alertHeader: this.NoInternetHeader);
        else
          this.RunLoadFailed(this.LoadFailedMessage, false, alertHeader: this.LoadFailedHeader, isButtonTryAgainVisible: true);
        return false;
      }
      catch (Exception ex)
      {
        if (Debugger.IsAttached)
          Debugger.Break();
        this.IsWasErrorOnLoading = true;
        this.RunLoadFailed(this.LoadFailedMessage, false, alertHeader: this.LoadFailedHeader);
        return false;
      }
    }

    public async Task InitializeAsync()
    {
      await (this._initializeTask ?? (this._initializeTask = this.InitializeInnerAsync()));
    }

    private async Task InitializeInnerAsync()
    {
      this.IsInitializeLoadingActive = true;
      CacheState cacheState = await this.TryLoadFromCache();
      if ((cacheState & CacheState.Actual) == CacheState.Actual)
      {
        this.IsInitializeLoadingActive = false;
        this.EndDisplayLoadingWhenUpdating();
        this.IsLoadedSuccessfully = true;
        this.OnLoadedOrUpdatedSuccessfully(EventArgs.Empty);
      }
      else
      {
        if ((cacheState & CacheState.Available) == CacheState.Available)
        {
          this.IsInitializeLoadingActive = false;
          this.IsLoadedSuccessfully = true;
          this.BeginDisplayLoadingWhenUpdating();
          this.OnLoadedOrUpdatedSuccessfully(EventArgs.Empty);
        }
        if (await this.TryLoadAsync())
        {
          this.IsLoadedSuccessfully = true;
          this.OnLoadedOrUpdatedSuccessfully(EventArgs.Empty);
        }
        this.EndDisplayLoadingWhenUpdating();
        this.IsInitializeLoadingActive = false;
      }
    }

    protected async Task LoadNextItem()
    {
      this._isContentActual = false;
      this.IsInitializeLoadingActive = true;
      this.IsLoadedSuccessfully = false;
      this._loadingTask = (Task<bool>) null;
      if (await this.TryLoadAsync())
      {
        this.IsLoadedSuccessfully = true;
        this.OnLoadedOrUpdatedSuccessfully(EventArgs.Empty);
      }
      this.IsInitializeLoadingActive = false;
    }

    [IgnoreDataMember]
    public virtual bool UpdatingOnNavigationEnabled => false;

    protected virtual async Task<bool> UpdateInnerAsync()
    {
      bool flag;
      return flag;
    }

    public async Task UpdateAsync()
    {
      try
      {
        this.BeginDisplayLoadingWhenUpdating();
        this.IsButtonTryAgainVisible = false;
        this._isContentActual = false;
        this._loadingTask = (Task<bool>) null;
        if (!await this.TryLoadAsyncInner(new Func<Task<bool>>(this.UpdateInnerAsync)))
          return;
        this.IsLoadedSuccessfully = true;
        this.OnLoadedOrUpdatedSuccessfully(EventArgs.Empty);
      }
      finally
      {
        this.EndDisplayLoadingWhenUpdating();
      }
    }

    public virtual async Task OnDeserializedFromStorageAsync()
    {
      WindowsRuntimeMarshal.AddEventHandler<NetworkStatusChangedEventHandler>(new Func<NetworkStatusChangedEventHandler, EventRegistrationToken>(NetworkInformation.add_NetworkStatusChanged), new Action<EventRegistrationToken>(NetworkInformation.remove_NetworkStatusChanged), new NetworkStatusChangedEventHandler(this.NetworkInformationOnNetworkStatusChanged));
    }
  }
}
