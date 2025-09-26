// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.MvvmFrame
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using MetroLab.Common.Transitions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

#nullable disable
namespace MetroLab.Common
{
  public class MvvmFrame : ContentControl, INavigate
  {
    private const int DefaultMaxPagesInCacheCount = 5;
    private Cache<Page> _pagesCache = new Cache<Page>(5);
    private bool _loadDataAtBackgroundThread = true;
    private readonly Stack<INavigationItem> _navigationStack = new Stack<INavigationItem>();
    private readonly IPageViewModelStackSerializer _serializer;
    private readonly Dictionary<Type, Type> _viewModelsAndPagesMapping = new Dictionary<Type, Type>();
    private const string SessionStateFileName = "_sessionState";

    private Cache<Page> PagesCache => this._pagesCache;

    public int MaxPagesInCacheCount
    {
      get => this._pagesCache.Limit;
      set => this._pagesCache = new Cache<Page>(value);
    }

    public bool LoadDataAtBackgroundThread
    {
      get => this._loadDataAtBackgroundThread;
      set => this._loadDataAtBackgroundThread = value;
    }

    private Stack<INavigationItem> NavigationStack => this._navigationStack;

    public INavigationItem CurrentNavigationItem => this.NavigationStack.Peek();

    public MvvmFrame(IPageViewModelStackSerializer serializer)
    {
      this._serializer = serializer != null ? serializer : throw new ArgumentNullException(nameof (serializer));
      // Use normal property assignments instead of decompiled put_ calls
      this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
      this.VerticalContentAlignment = VerticalAlignment.Stretch;
    }

    public void AddSupportedPageViewModel(Type viewModelType, Type pageType)
    {
      if (viewModelType == null)
        throw new NullReferenceException(nameof (viewModelType));
      if (pageType == null)
        throw new NullReferenceException(nameof (pageType));
      try
      {
        this._viewModelsAndPagesMapping.Add(viewModelType, pageType);
      }
      catch (ArgumentException ex)
      {
        throw new InvalidOperationException(string.Format("ViewModel of type {0} alredy mapped", (object) viewModelType.Name), (Exception) ex);
      }
    }

    private Type GetPageType(Type viewModelType)
    {
      Type pageType;
      if (this._viewModelsAndPagesMapping.TryGetValue(viewModelType, out pageType))
        return pageType;
      throw new InvalidOperationException(string.Format("There are no appropriate View for ViewModel {0}", (object) viewModelType.FullName));
    }

    private void ShowItem(INavigationItem navigationItem)
    {
      Page page = navigationItem != null ? navigationItem.GetPage() : throw new ArgumentNullException(nameof (navigationItem));
      this.PagesCache.Add(page);
      // set alignments normally
      if (page is FrameworkElement fe)
      {
        fe.HorizontalAlignment = HorizontalAlignment.Stretch;
        fe.VerticalAlignment = VerticalAlignment.Stretch;
      }
      this.Content = page;
      Window.Current.Activate();
      if (!(page is MvvmPage))
        return;
      (page as MvvmPage).RaiseNavigatedTo();
    }

    public bool CanGoBack => this.NavigationStack.Count > 1;

    public async Task<IPageViewModel> GoBack()
    {
      IPageViewModel previousPageViewModel = this.NavigationStack.Peek().PageViewModel;
      if (!this.RemoveLastEntryFromNavigationStack())
        return null;
      if (this.NavigationStack.Count == 0)
      {
        Application.Current.Exit();
        return null;
      }

      INavigationItem navigationItem = this.NavigationStack.Peek();
      MvvmPage page = (MvvmPage) navigationItem.GetPage();
      this.ShowItem(navigationItem);

      // Run transition if available
      MetroLabNavigationTransitionInfo navigationTransition = page.NavigationTransition;
      if (navigationTransition != null)
      {
        navigationTransition.RunGoInBackwardTransition(new NavigationTransitionArgs()
        {
          TargetPage = page,
          NextPageViewModel = navigationItem.PageViewModel,
          PreviousPageViewModel = previousPageViewModel
        });
      }

      // Ensure page is interactive
      if (page is UIElement uiPage)
        uiPage.IsHitTestVisible = true;

      var pageViewModel = navigationItem.PageViewModel;
      if (this.LoadDataAtBackgroundThread)
      {
        // Fire-and-forget initialization on background thread
        ThreadPool.RunAsync(async workItem => await pageViewModel.InitializeAsync());
      }
      else
      {
        await pageViewModel.InitializeAsync();
      }

      return pageViewModel;
    }

    public async Task Navigate(IPageViewModel pageViewModel)
    {
      if (pageViewModel == null)
        throw new ArgumentNullException(nameof(pageViewModel));

      var mvvmNavigationItem = new MvvmNavigationItem(pageViewModel, this);
      this.NavigationStack.Push(mvvmNavigationItem);
      MvvmPage page = (MvvmPage) mvvmNavigationItem.GetPage();
      this.ShowItem((INavigationItem) mvvmNavigationItem);

      MetroLabNavigationTransitionInfo navigationTransition = page.NavigationTransition;
      if (this.NavigationStack.Count > 1 && navigationTransition != null)
      {
        navigationTransition.RunGoInForwardTransition(new NavigationTransitionArgs()
        {
          TargetPage = page,
          NextPageViewModel = pageViewModel
        });
      }

      if (this.LoadDataAtBackgroundThread)
      {
        ThreadPool.RunAsync(async workItem => await pageViewModel.InitializeAsync());
      }
      else
      {
        await pageViewModel.InitializeAsync();
      }
    }

    public bool Navigate(Type sourcePageType)
    {
      lock (this)
      {
        MvvmFrame.PageNavigationItem pageNavigationItem = new MvvmFrame.PageNavigationItem((Page) Activator.CreateInstance(sourcePageType));
        this.NavigationStack.Push((INavigationItem) pageNavigationItem);
        this.ShowItem((INavigationItem) pageNavigationItem);
        return true;
      }
    }

    public async Task SaveToStorageAsync()
    {
      IPageViewModel[] array = this.NavigationStack.OfType<MvvmFrame.MvvmNavigationItem>().Select<MvvmFrame.MvvmNavigationItem, IPageViewModel>((Func<MvvmFrame.MvvmNavigationItem, IPageViewModel>) (x => x.PageViewModel)).ToList<IPageViewModel>().ToArray();
      MemoryStream sessionData = new MemoryStream();
      await this._serializer.SerializeAsync(array, (Stream) sessionData);
      using (Stream fileStream = await ((IStorageFile) await ApplicationData.Current.RoamingFolder.CreateFileAsync("_sessionState", (CreationCollisionOption) 1)).OpenStreamForWriteAsync())
      {
        sessionData.Seek(0L, SeekOrigin.Begin);
        await sessionData.CopyToAsync(fileStream);
        await fileStream.FlushAsync();
      }
    }

    public async Task LoadFromStorageAsync(bool showImmediately = true)
    {
      using (IInputStream inStream = await (await ApplicationData.Current.RoamingFolder.CreateFileAsync("_sessionState", (CreationCollisionOption) 3)).OpenSequentialReadAsync())
      {
        IPageViewModel[] sessionState = await this._serializer.DeserializeAsync(inStream.AsStreamForRead());
        for (int index = 0; index < sessionState.Length; ++index)
          await sessionState[index].OnDeserializedFromStorageAsync();
        foreach (IPageViewModel pageViewModel in ((IEnumerable<IPageViewModel>) sessionState).Reverse<IPageViewModel>())
          this.NavigationStack.Push((INavigationItem) new MvvmFrame.MvvmNavigationItem(pageViewModel, this));
      }
      INavigationItem navigationItem = this.NavigationStack.Peek();
      if (showImmediately)
        this.ShowItem(navigationItem);

      var pageVm = navigationItem.PageViewModel;
      if (this.LoadDataAtBackgroundThread)
      {
        ThreadPool.RunAsync(async workItem => await pageVm.InitializeAsync());
      }
      else
      {
        await pageVm.InitializeAsync();
      }
    }

    public void ClearNavigationStack()
    {
      foreach (IDisposable navigation in this.NavigationStack)
        navigation.Dispose();
      this.NavigationStack.Clear();
    }

    public bool RemoveLastEntryFromNavigationStack()
    {
      if (this.NavigationStack.Count <= 0)
        throw new InvalidOperationException(CommonLocalizedResources.GetLocalizedString("exceptionMsg_CantGoBack"));
      if (this.Content is MvvmPage content && !content.RaiseNavigatingFrom(NavigationType.Back))
        return false;
      WeakReference<Page> pageReference = this.CurrentNavigationItem.GetPageReference();
      Page target;
      if (pageReference != null && pageReference.TryGetTarget(out target))
        this.PagesCache.Remove(target);
      this.NavigationStack.Pop().Dispose();
      return true;
    }

    public void UpdateView()
    {
      this.PagesCache.Clear();
      foreach (INavigationItem navigation in this.NavigationStack)
      {
        if (navigation is MvvmFrame.MvvmNavigationItem mvvmNavigationItem)
        {
          mvvmNavigationItem.PageViewModel.UpdateAsync();
          mvvmNavigationItem.UpdateView();
        }
      }
      this.ShowItem(this.NavigationStack.Peek());
    }

    private class MvvmNavigationItem : INavigationItem, IDisposable
    {
      private readonly MvvmFrame _mvvmFrame;
      private WeakReference<MvvmPage> _pageReference;
      private readonly IPageViewModel _pageViewModel;

      protected WeakReference<MvvmPage> PageReference => this._pageReference;

      public IPageViewModel PageViewModel => this._pageViewModel;

      public MvvmNavigationItem(IPageViewModel pageViewModel, MvvmFrame mvvmFrame)
      {
        if (mvvmFrame == null)
          throw new ArgumentNullException(nameof (mvvmFrame));
        if (pageViewModel == null)
          throw new ArgumentNullException(nameof (pageViewModel));
        this._mvvmFrame = mvvmFrame;
        this._pageViewModel = pageViewModel;
      }

      protected internal void InitializePage(MvvmPage page) => page.ViewModel = this._pageViewModel;

      public Page GetPage()
      {
        lock (this)
        {
          MvvmPage target;
          if (this._pageReference != null && this._pageReference.TryGetTarget(out target))
            return (Page) target;
          MvvmPage instance = (MvvmPage) Activator.CreateInstance(this._mvvmFrame.GetPageType(this._pageViewModel.GetType()));
          instance.LoadDataAtBackgroundThread = this._mvvmFrame.LoadDataAtBackgroundThread;
          this.InitializePage(instance);
          if (this._pageReference == null)
            this._pageReference = new WeakReference<MvvmPage>(instance);
          else
            this._pageReference.SetTarget(instance);
          return (Page) instance;
        }
      }

      public WeakReference<Page> GetPageReference()
      {
        lock (this)
        {
          MvvmPage target;
          return this._pageReference != null && this._pageReference.TryGetTarget(out target) && target != null ? new WeakReference<Page>((Page) target) : (WeakReference<Page>) null;
        }
      }

      public void UpdateView()
      {
        lock (this)
          this._pageReference = (WeakReference<MvvmPage>) null;
      }

      public void Dispose()
      {
        MvvmPage target;
        if (this._pageReference == null || !this._pageReference.TryGetTarget(out target))
          return;
        this._mvvmFrame.PagesCache.Remove((Page) target);
        target.OnRemoved();
      }

      public override string ToString()
      {
        return string.Format("MvvmNavigationItem: {0}", (object) this._pageViewModel);
      }
    }

    private class PageNavigationItem : INavigationItem, IDisposable
    {
      private readonly Page _page;

      public PageNavigationItem(Page page)
      {
        this._page = page != null ? page : throw new ArgumentNullException(nameof (page));
      }

      public Page GetPage() => this._page;

      public WeakReference<Page> GetPageReference() => new WeakReference<Page>(this.GetPage());

      public IPageViewModel PageViewModel => (IPageViewModel) null;

      public void Dispose()
      {
      }
    }

    [DataContract]
    public class AppStateSnapshot
    {
      [DataMember]
      public IPageViewModel[] PageViewModels { get; set; }
    }
  }
}
