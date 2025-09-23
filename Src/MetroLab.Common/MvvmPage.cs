// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.MvvmPage
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using MetroLab.Common.Transitions;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.System;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

#nullable disable
namespace MetroLab.Common
{
  [WebHostHidden]
  public class MvvmPage : Page//, ILoadedOrUpdatedSuccessfullyListener
  {
    private List<Control> _layoutAwareControls;
    public static readonly DependencyProperty NavigationTransitionProperty = DependencyProperty.Register(nameof (NavigationTransition), typeof (MetroLabNavigationTransitionInfo), typeof (MvvmPage), (PropertyMetadata) null);
    private IPageViewModel _viewModel;
    private bool _firstNavigation = true;

    public bool LoadDataAtBackgroundThread { get; internal set; }

    public MetroLabNavigationTransitionInfo NavigationTransition
    {
      get
      {
        return (MetroLabNavigationTransitionInfo) ((DependencyObject) this).GetValue(MvvmPage.NavigationTransitionProperty);
      }
      set
      {
        ((DependencyObject) this).SetValue(MvvmPage.NavigationTransitionProperty, (object) value);
      }
    }

    protected MvvmPage()
    {
      if (DesignMode.DesignModeEnabled)
        return;
      this.NavigationCacheMode = NavigationCacheMode.Required;
      this.Transitions = null;
      // subscribe to Loaded/Unloaded using standard events
      ((FrameworkElement)this).Loaded += this.OnLoaded;
      ((FrameworkElement)this).Unloaded += this.OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs args)
    {
      this.StartLayoutUpdates(sender, args);
      Window current = Window.Current;
      Rect bounds = current.Bounds;
      if (((FrameworkElement)this).ActualHeight != bounds.Height || ((FrameworkElement)this).ActualWidth != bounds.Width)
        return;
      CoreWindow coreWindow = current.CoreWindow;
      CoreDispatcher dispatcher = coreWindow.Dispatcher;

      // subscribe to accelerator and pointer events using normal +=
      dispatcher.AcceleratorKeyActivated += this.CoreDispatcherAcceleratorKeyActivated;
      coreWindow.PointerPressed += this.CoreWindowPointerPressed;
    }

    private void OnUnloaded(object sender, RoutedEventArgs args)
    {
      this.StopLayoutUpdates(sender, args);
      CoreWindow coreWindow = Window.Current.CoreWindow;
      try
      {
        coreWindow.Dispatcher.AcceleratorKeyActivated -= this.CoreDispatcherAcceleratorKeyActivated;
      }
      catch { }
      try
      {
        coreWindow.PointerPressed -= this.CoreWindowPointerPressed;
      }
      catch { }
    }

    public IPageViewModel ViewModel
    {
      get => this._viewModel;
      set
      {
        if (object.Equals((object) this._viewModel, (object) value))
          return;
        if (this._viewModel is IDataLoadViewModel viewModel)
          viewModel.RemoveLoadedOrUpdatedSuccessfullyListener((ILoadedOrUpdatedSuccessfullyListener) this);
        this._viewModel = value;
        if (value is IDataLoadViewModel dataLoadViewModel)
          dataLoadViewModel.AddLoadedOrUpdatedSuccessfullyListener((ILoadedOrUpdatedSuccessfullyListener) this);
        this.DataContext = (object) value;
      }
    }

    public void OnLoadedOrUpdatedSuccessfully(object sender, EventArgs args)
    {
      this.OnViewModelLoadedOrUpdatedSuccessfully();
    }

    public void OnRemoved()
    {
      IPageViewModel viewModel1 = this.ViewModel;
      if (viewModel1 != null)
      {
        viewModel1.UnSubscribeFromEvents();
        if (this._viewModel is IDataLoadViewModel viewModel2)
          viewModel2.RemoveLoadedOrUpdatedSuccessfullyListener((ILoadedOrUpdatedSuccessfullyListener) this);
      }
      this._viewModel = (IPageViewModel) null;
    }

    protected virtual void OnViewModelLoadedOrUpdatedSuccessfully()
    {
    }

    protected virtual void GoBack(object sender, RoutedEventArgs e)
    {
      if (!AppViewModel.Current.CanGoBack)
        return;
      AppViewModel.Current.GoBack();
    }

    protected virtual void GoForward(object sender, RoutedEventArgs e)
    {
    }

    private void CoreDispatcherAcceleratorKeyActivated(
      CoreDispatcher sender,
      AcceleratorKeyEventArgs args)
    {
      VirtualKey virtualKey = args.VirtualKey;
      if (args.EventType != 4 && args.EventType != null || virtualKey != 37 && virtualKey != 39 && virtualKey != 166 && virtualKey != 167)
        return;
      CoreWindow coreWindow = Window.Current.CoreWindow;
      CoreVirtualKeyStates virtualKeyStates = (CoreVirtualKeyStates) 1;
      bool flag1 = (coreWindow.GetKeyState((VirtualKey) 18) & virtualKeyStates) == virtualKeyStates;
      bool flag2 = (coreWindow.GetKeyState((VirtualKey) 17) & virtualKeyStates) == virtualKeyStates;
      bool flag3 = (coreWindow.GetKeyState((VirtualKey) 16) & virtualKeyStates) == virtualKeyStates;
      bool flag4 = !flag1 && !flag2 && !flag3;
      bool flag5 = flag1 && !flag2 && !flag3;
      if (virtualKey == 166 & flag4 || virtualKey == 37 & flag5)
      {
        args.Handled = true;
        this.GoBack((object) this, new RoutedEventArgs());
      }
      else
      {
        if (!(virtualKey == 167 & flag4) && !(virtualKey == 39 & flag5))
          return;
        args.Handled = true;
        this.GoForward((object) this, new RoutedEventArgs());
      }
    }

    private void CoreWindowPointerPressed(CoreWindow sender, PointerEventArgs args)
    {
      PointerPointProperties properties = args.CurrentPoint.Properties;
      if (properties.IsLeftButtonPressed || properties.IsRightButtonPressed || properties.IsMiddleButtonPressed)
        return;
      bool isXbutton1Pressed = properties.IsXButton1Pressed;
      bool isXbutton2Pressed = properties.IsXButton2Pressed;
      if (!(isXbutton1Pressed ^ isXbutton2Pressed))
        return;
      args.Handled = true;
      if (isXbutton1Pressed)
        this.GoBack((object) this, new RoutedEventArgs());
      if (!isXbutton2Pressed)
        return;
      this.GoForward((object) this, new RoutedEventArgs());
    }

    private void StartLayoutUpdates(object sender, RoutedEventArgs e)
    {
      if (!(sender is Control control))
        return;
      if (this._layoutAwareControls == null)
      {
        Window current = Window.Current;
        // subscribe to SizeChanged
        current.SizeChanged += this.WindowSizeChanged;
        this._layoutAwareControls = new List<Control>();
      }
      this._layoutAwareControls.Add(control);
    }

        private void WindowSizeChanged(object sender, WindowSizeChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StopLayoutUpdates(object sender, RoutedEventArgs e)
    {
      if (!(sender is Control control) || this._layoutAwareControls == null)
        return;
      this._layoutAwareControls.Remove(control);
      if (this._layoutAwareControls.Count != 0)
        return;
      this._layoutAwareControls = (List<Control>) null;
      try { Window.Current.SizeChanged -= this.WindowSizeChanged; } catch { }
    }

    protected virtual void OnNavigatedTo()
    {
      var dataLoadViewModel = this.ViewModel as IDataLoadViewModel;
      if (this._firstNavigation)
      {
        this._firstNavigation = false;
      }
      else
      {
        if (dataLoadViewModel == null || !dataLoadViewModel.UpdatingOnNavigationEnabled)
          return;
        if (this.LoadDataAtBackgroundThread)
        {
          ThreadPool.RunAsync(async (workItem) => await dataLoadViewModel.UpdateAsync());
        }
        else
        {
          dataLoadViewModel.UpdateAsync();
        }
      }
    }

    protected virtual bool OnNavigatingFrom(NavigationType type) => true;

    public void RaiseNavigatedTo() => this.OnNavigatedTo();

    public bool RaiseNavigatingFrom(NavigationType type) => this.OnNavigatingFrom(type);
  }
}
