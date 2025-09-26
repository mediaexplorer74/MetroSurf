using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using System.ComponentModel;
using System.Runtime.Serialization;

#nullable disable
namespace MetroLab.Common
{
  // Provides base behavior expected by VPN.ViewModel page view models
  [DataContract]
  public abstract class PageViewModel : DataLoadViewModel, IPageViewModel
  {
    // Subclasses typically override these two to provide load/update logic
    protected /*virtual*/override async Task<bool> LoadAsync()
    {
      return await Task.FromResult(false);
    }

    protected /*virtual*/override async Task<bool> UpdateInnerAsync()
    {
      // Default delegates to LoadAsync
      return await LoadAsync();
    }

    // IPageViewModel implementations - wire to the protected methods
    public override async Task InitializeAsync()
    {
      await this.LoadAsync();
    }

    public override async Task UpdateAsync()
    {
      await this.UpdateInnerAsync();
    }

    public override async Task RequestData(DataTransferManager manager, DataRequestedEventArgs args)
    {
      await Task.CompletedTask;
    }

    public override async Task OnSerializingToStorageAsync()
    {
      await Task.CompletedTask;
    }

    public override async Task OnSerializedToStorageAsync()
    {
      await Task.CompletedTask;
    }

    public override async Task OnDeserializedFromStorageAsync()
    {
      await Task.CompletedTask;
    }

    public override async Task OnNavigatingAsync()
    {
      await Task.CompletedTask;
    }

    public override void UnSubscribeFromEvents()
    {
      base.UnSubscribeFromEvents();
    }

    // Added property to satisfy IPageViewModel
    [IgnoreDataMember]
    public IPrintHelper PrintHelper { get; set; }

    // Compatibility shim: some ported view models call LoadLocalization/LoadLocaliztion.
    // Provide no-op implementations here so view models compile and can call
    // their own localization helpers (they typically use VPN.Localization.LocalizedResources directly).
    protected void LoadLocalization()
    {
      // No operation by default. Kept for compatibility with original WP8/UWP ported code.
    }

    // Preserve common misspelling observed in decompiled code.
    protected void LoadLocaliztion()
    {
      LoadLocalization();
    }
  }
}
