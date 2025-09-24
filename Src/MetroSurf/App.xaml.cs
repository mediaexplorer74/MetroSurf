using MetroLab.Common;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using VPN.Common;
using VPN.View;
using VPN.ViewModel;
using VPN.ViewModel.Http;
using VPN.ViewModel.Pages;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Markup;

#nullable disable
namespace VPN
{
    public sealed partial class App : Application
    {
        private static readonly Type HomePageViewModelType = typeof(LoginPageViewModel);
        private static readonly Type StartViewModelType = typeof(LoginPageViewModel);
        private MvvmFrame _mvvmFrame;
        private Task _initializeTask;

        public App()
        {
            this.InitializeComponent();

            // subscribe to application events in a normal way
            this.UnhandledException += CurrentUnhandledExceptionAsync;
            this.Suspending += OnSuspendingAsync;
        }

        public Action OnNewLogEntry { get; set; }

        private static MvvmFrame CreateMainFrame()
        {
            var mainFrame = new MvvmFrame((IPageViewModelStackSerializer)new App.DataContractViewModelSerializer())
            {
                LoadDataAtBackgroundThread = true
            };

            mainFrame.AddSupportedPageViewModel(typeof(MainPageViewModel), typeof(MainPage));
            mainFrame.AddSupportedPageViewModel(typeof(GuideGalleryPageViewModel), typeof(GuideGalleryPage));
            mainFrame.AddSupportedPageViewModel(typeof(LoginPageViewModel), typeof(LoginPage));
            mainFrame.AddSupportedPageViewModel(typeof(LoginKeepSolidIDPageViewModel), typeof(LoginKeepSolidIDPage));
            mainFrame.AddSupportedPageViewModel(typeof(SettingsPageViewModel), typeof(SettingsPage));
            mainFrame.AddSupportedPageViewModel(typeof(ServerCredentialsPageViewModel), typeof(ServerCredentialsPage));
            mainFrame.AddSupportedPageViewModel(typeof(ServerCredentialsPageViewModelWin10), typeof(ServerCredentialsPageWin10));
            mainFrame.AddSupportedPageViewModel(typeof(InviteFriendPageViewModel), typeof(InviteFriendPage));
            mainFrame.AddSupportedPageViewModel(typeof(OverviewGalleryPageViewModel), typeof(OverviewGalleryPage));
            mainFrame.AddSupportedPageViewModel(typeof(RegistrationPageViewModel), typeof(VPN.View.RegistrationPage));
            mainFrame.AddSupportedPageViewModel(typeof(ChangePasswordPageViewModel), typeof(ChangePasswordPage));

            return mainFrame;
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            await InitializeAsync();

            Window.Current.Content = _mvvmFrame ?? (_mvvmFrame = CreateMainFrame());
            Window.Current.Activate();

            AppViewModel.Current.HomePageViewModelType = HomePageViewModelType;

            if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
            {
                try
                {
                    await _mvvmFrame.LoadFromStorageAsync();
                    AutoLoginAgent.Current = CacheAgent.LoadFromLocalSettings<AutoLoginAgent>("temp");
                }
                catch
                {
                    AppViewModel.Current.NavigateToViewModel((IPageViewModel)Activator.CreateInstance(StartViewModelType));
                }
            }
            else if (CacheAgent.IsNeedToShowApplicationTour)
            {
                AppViewModel.Current.NavigateToViewModel((IPageViewModel)Activator.CreateInstance(typeof(OverviewGalleryPageViewModel)));
            }
            else
            {
                AppViewModel.Current.NavigateToViewModel((IPageViewModel)Activator.CreateInstance(StartViewModelType));
            }
        }

        protected override void OnActivated(IActivatedEventArgs args)
        {
            base.OnActivated(args);
            if (args is IContinuationActivatedEventArgs contArgs)
            {
                new ContinuationManager().Continue(contArgs);
            }

            if (args is ProtocolActivatedEventArgs protocolArgs)
            {
                LoginPage.Current.ContinueAppAuthentication(Uri.UnescapeDataString(protocolArgs.Uri.ToString()));
            }

            if (Window.Current.Content == null)
            {
                Window.Current.Content = _mvvmFrame ?? (_mvvmFrame = CreateMainFrame());
                Window.Current.Activate();
            }
        }

        private Task InitializeTask => _initializeTask ?? (_initializeTask = InitializeAsync());

        private async Task InitializeAsync()
        {
            try
            {
                if (!this.Resources.ContainsKey("AppViewModel"))
                    this.Resources.Add("AppViewModel", AppViewModel.Current);

                _mvvmFrame = CreateMainFrame();

                if (Constants.GetCurrentLanguage() == "ar")
                {
                    if (_mvvmFrame is FrameworkElement fe)
                        fe.FlowDirection = FlowDirection.RightToLeft;
                }

                AppViewModel.Current.MvvmFrame = _mvvmFrame;
                await BackgroundAgentsHelper.RegisterUpdateApplicationTileAgent();
                await ContentCache.Current.Initialize();
            }
            catch (Exception ex)
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                throw;
            }
        }

        private void CurrentUnhandledExceptionAsync(object sender, UnhandledExceptionEventArgs e)
        {
            e.Handled = false;
            this.ShowErrorMessage(CommonLocalizedResources.GetLocalizedString("msg_GeneralUnhandledException"), CommonLocalizedResources.GetLocalizedString("txt_Error"));
        }

        private async void ShowErrorMessage(string content, string title)
        {
            var messageDialog = new MessageDialog(content ?? string.Empty, title ?? string.Empty);
            messageDialog.Commands.Add(new UICommand("Ok"));
            await messageDialog.ShowAsync();
            Application.Current.Exit();
        }

        private async void OnSuspendingAsync(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            await AppViewModel.Current.MvvmFrame.SaveToStorageAsync();
            CacheAgent.SaveToLocalSettings<AutoLoginAgent>("temp", AutoLoginAgent.Current);
            deferral.Complete();
        }

       

        [DataContract]
        private class AppStateSnapshot
        {
            [DataMember]
            public IPageViewModel[] PageViewModels { get; set; }
        }

        private class DataContractViewModelSerializer : IPageViewModelStackSerializer
        {
            private readonly DataContractSerializerSettings _settings;

            public DataContractViewModelSerializer()
            {
                this._settings = new DataContractSerializerSettings()
                {
                    KnownTypes = new Type[]
                    {
                        typeof(BindableBase),
                        typeof(BaseViewModel),
                        typeof(DataLoadViewModel),
                        typeof(SimplePageViewModel),
                        typeof(MainPageViewModel),
                        typeof(GuideGalleryPageViewModel),
                        typeof(LoginPageViewModel),
                        typeof(LoginKeepSolidIDPageViewModel),
                        typeof(SettingsPageViewModel),
                        typeof(ServerCredentialsPageViewModel),
                        typeof(ServerCredentialsPageViewModelWin10),
                        typeof(InviteFriendPageViewModel),
                        typeof(OverviewGalleryPageViewModel),
                        typeof(RegistrationPageViewModel),
                        typeof(ChangePasswordPageViewModel)
                    },
                    SerializeReadOnlyTypes = false
                };
            }

            public async Task SerializeAsync(IPageViewModel[] pageViewModels, Stream streamForWrite)
            {
                var graph = new AppStateSnapshot() { PageViewModels = pageViewModels };
                new DataContractSerializer(typeof(AppStateSnapshot), this._settings).WriteObject(streamForWrite, graph);
                await streamForWrite.FlushAsync();
            }

            public async Task<IPageViewModel[]> DeserializeAsync(Stream streamForRead)
            {
                return ((AppStateSnapshot)new DataContractSerializer(typeof(AppStateSnapshot), this._settings).ReadObject(streamForRead)).PageViewModels;
            }
        }
    }
}

