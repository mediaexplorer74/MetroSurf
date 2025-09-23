// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.AnimatedImage
// Assembly: VPN, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 9341524F-1843-4B25-9300-CCE3043D39C8
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\VPN.exe

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Graphics.Imaging;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace VPN.View.Controls
{
  public sealed class AnimatedImage : Grid, IComponentConnector
  {
    private volatile bool _isStopped = true;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    public static readonly DependencyProperty SourceStreamProperty = DependencyProperty.Register(nameof (SourceStream), typeof (IRandomAccessStream), typeof (AnimatedImage), new PropertyMetadata((object) null, new Windows.UI.Xaml.PropertyChangedCallback(AnimatedImage.PropertyChangedCallback)));
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof (Stretch), typeof (Stretch), typeof (AnimatedImage), new PropertyMetadata((object) null));
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Grid Root;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private Image InnerImage;
    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    private bool _contentLoaded;

    public AnimatedImage()
    {
      this.InitializeComponent();
      this.Stretch = (Stretch) 2;
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) this).add_Unloaded), new Action<EventRegistrationToken>(((FrameworkElement) this).remove_Unloaded), (RoutedEventHandler) ((s, e) =>
      {
        this._isStopped = true;
        this._cancellationTokenSource.Cancel();
      }));
      WindowsRuntimeMarshal.AddEventHandler<RoutedEventHandler>(new Func<RoutedEventHandler, EventRegistrationToken>(((FrameworkElement) this).add_Loaded), new Action<EventRegistrationToken>(((FrameworkElement) this).remove_Loaded), (RoutedEventHandler) ((s, e) =>
      {
        this._isStopped = false;
        if (this.SourceStream == null)
          return;
        this.InitializeWithNewImage(this.SourceStream);
      }));
    }

    private static async Task<AnimatedImage.ImageFrameContainer> GetFrame(
      PixelDataProvider provider,
      int width,
      int height,
      TimeSpan delay)
    {
      byte[] buffer = provider.DetachPixelData();
      WriteableBitmap writeableBitmap = new WriteableBitmap(width, height);
      using (Stream stream = writeableBitmap.PixelBuffer.AsStream())
        await stream.WriteAsync(buffer, 0, buffer.Length);
      return new AnimatedImage.ImageFrameContainer((ImageSource) writeableBitmap, delay)
      {
        PixelWidth = width,
        PixelHeight = height
      };
    }

    private async Task InitializeWithNewImage(IRandomAccessStream imageStream)
    {
      try
      {
        this._isStopped = true;
        this._cancellationTokenSource.Cancel();
        if (imageStream == null || imageStream.Size == 0UL)
        {
          this.InnerImage.put_Source((ImageSource) null);
        }
        else
        {
          imageStream.Seek(0UL);
          List<AnimatedImage.ImageFrameContainer> items = new List<AnimatedImage.ImageFrameContainer>();
          BitmapDecoder decoder = await BitmapDecoder.CreateAsync(imageStream);
          uint frameCount = decoder.FrameCount;
          List<AnimatedImage.ImageFrameContainer> imageFrameContainerList;
          if (frameCount > 1U)
          {
            for (uint frameNumber = 0; frameNumber < frameCount; ++frameNumber)
            {
              BitmapFrame frame = await decoder.GetFrameAsync(frameNumber);
              PixelDataProvider pixelDataProvider = await frame.GetPixelDataAsync((BitmapPixelFormat) 87, (BitmapAlphaMode) 2, new BitmapTransform(), (ExifOrientationMode) 1, (ColorManagementMode) 0);
              TimeSpan delay = TimeSpan.FromSeconds(double.Parse(((IDictionary<string, BitmapTypedValue>) await ((BitmapPropertiesView) ((IDictionary<string, BitmapTypedValue>) await frame.BitmapProperties.GetPropertiesAsync((IEnumerable<string>) new List<string>()))["/grctlext"].Value).GetPropertiesAsync((IEnumerable<string>) new List<string>()
              {
                "/Delay"
              }))["/Delay"].Value.ToString()) / 100.0);
              imageFrameContainerList = items;
              AnimatedImage.ImageFrameContainer frame1 = await AnimatedImage.GetFrame(pixelDataProvider, (int) frame.PixelWidth, (int) frame.PixelHeight, delay);
              imageFrameContainerList.Add(frame1);
              imageFrameContainerList = (List<AnimatedImage.ImageFrameContainer>) null;
              frame = (BitmapFrame) null;
              pixelDataProvider = (PixelDataProvider) null;
            }
          }
          else
          {
            PixelDataProvider pixelDataAsync = await decoder.GetPixelDataAsync((BitmapPixelFormat) 87, (BitmapAlphaMode) 2, new BitmapTransform(), (ExifOrientationMode) 1, (ColorManagementMode) 0);
            imageFrameContainerList = items;
            int pixelWidth = (int) decoder.PixelWidth;
            int pixelHeight = (int) decoder.PixelHeight;
            TimeSpan zero = TimeSpan.Zero;
            AnimatedImage.ImageFrameContainer frame = await AnimatedImage.GetFrame(pixelDataAsync, pixelWidth, pixelHeight, zero);
            imageFrameContainerList.Add(frame);
            imageFrameContainerList = (List<AnimatedImage.ImageFrameContainer>) null;
          }
          this._isStopped = false;
          this._cancellationTokenSource = new CancellationTokenSource();
          await this.ShowFramesLoopAsync(items, this._cancellationTokenSource.Token);
          items = (List<AnimatedImage.ImageFrameContainer>) null;
          decoder = (BitmapDecoder) null;
        }
      }
      catch (TaskCanceledException ex)
      {
      }
      catch (Exception ex)
      {
        if (!Debugger.IsAttached)
          return;
        Debugger.Break();
      }
    }

    private async Task ShowFramesLoopAsync(
      List<AnimatedImage.ImageFrameContainer> items,
      CancellationToken cancellationToken)
    {
      if (items.Count > 1)
      {
        int currentFrameNumber = 0;
        while (!cancellationToken.IsCancellationRequested && !this._isStopped && ((FrameworkElement) this).Parent != null)
        {
          currentFrameNumber %= items.Count;
          AnimatedImage.ImageFrameContainer imageFrameContainer = items[currentFrameNumber];
          if (!this.IsStretch)
          {
            ((FrameworkElement) this.InnerImage).put_MaxWidth((double) imageFrameContainer.PixelWidth);
            ((FrameworkElement) this.InnerImage).put_MaxHeight((double) imageFrameContainer.PixelHeight);
          }
          if (this.NeedToEnlarge)
            this.EnlargeImage(imageFrameContainer.PixelWidth, imageFrameContainer.PixelHeight);
          this.InnerImage.put_Source(imageFrameContainer.ImageSource);
          await Task.Delay(imageFrameContainer.Delay, cancellationToken);
          ++currentFrameNumber;
        }
      }
      else
      {
        if (items.Count != 1)
          return;
        AnimatedImage.ImageFrameContainer imageFrameContainer = items[0];
        if (!this.IsStretch)
        {
          ((FrameworkElement) this.InnerImage).put_MaxWidth((double) imageFrameContainer.PixelWidth);
          ((FrameworkElement) this.InnerImage).put_MaxHeight((double) imageFrameContainer.PixelHeight);
        }
        if (this.NeedToEnlarge)
          this.EnlargeImage(imageFrameContainer.PixelWidth, imageFrameContainer.PixelHeight);
        this.InnerImage.put_Source(imageFrameContainer.ImageSource);
      }
    }

    public bool IsStretch { get; set; }

    public bool NeedToEnlarge { get; set; }

    private void EnlargeImage(int currentWidth, int currentHeight)
    {
      Rect bounds = Window.Current.Bounds;
      if ((double) currentWidth > bounds.Width || (double) currentHeight > bounds.Height)
        return;
      double num1 = 1.5;
      double num2 = (double) currentWidth * num1;
      double num3 = (double) currentHeight * num1;
      if (num3 > bounds.Height || num2 > bounds.Width)
      {
        double num4 = Math.Min(bounds.Height / (double) currentHeight, bounds.Width / (double) currentWidth);
        if (num4 < 1.0)
          return;
        num2 = (double) currentWidth * num4;
        num3 = (double) currentHeight * num4;
      }
      ((FrameworkElement) this.InnerImage).put_MaxWidth(num2);
      ((FrameworkElement) this.InnerImage).put_MaxHeight(num3);
    }

    public IRandomAccessStream SourceStream
    {
      get
      {
        return (IRandomAccessStream) ((DependencyObject) this).GetValue(AnimatedImage.SourceStreamProperty);
      }
      set => ((DependencyObject) this).SetValue(AnimatedImage.SourceStreamProperty, (object) value);
    }

    private static async void PropertyChangedCallback(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs args)
    {
      AnimatedImage aImage = (AnimatedImage) dependencyObject;
      IRandomAccessStream stream = (IRandomAccessStream) args.NewValue;
      if (stream == null)
        await aImage.InitializeWithNewImage((IRandomAccessStream) null);
      else
        aImage.InitializeWithNewImage(stream);
    }

    public Stretch Stretch
    {
      get => (Stretch) ((DependencyObject) this).GetValue(AnimatedImage.StretchProperty);
      set => ((DependencyObject) this).SetValue(AnimatedImage.StretchProperty, (object) value);
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void InitializeComponent()
    {
      if (this._contentLoaded)
        return;
      this._contentLoaded = true;
      Application.LoadComponent((object) this, new Uri("ms-appx:///View/Controls/AnimatedImage.xaml"), (ComponentResourceLocation) 0);
      this.Root = (Grid) ((FrameworkElement) this).FindName("Root");
      this.InnerImage = (Image) ((FrameworkElement) this).FindName("InnerImage");
    }

    [GeneratedCode("Microsoft.Windows.UI.Xaml.Build.Tasks", " 4.0.0.0")]
    [DebuggerNonUserCode]
    public void Connect(int connectionId, object target) => this._contentLoaded = true;

    private class ImageFrameContainer
    {
      public ImageFrameContainer(ImageSource imageSource, TimeSpan delay)
      {
        this.ImageSource = imageSource;
        this.Delay = delay;
      }

      public ImageSource ImageSource { get; private set; }

      public TimeSpan Delay { get; private set; }

      public int PixelHeight { get; set; }

      public int PixelWidth { get; set; }
    }
  }
}
