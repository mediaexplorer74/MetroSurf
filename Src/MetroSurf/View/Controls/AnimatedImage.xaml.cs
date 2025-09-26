// Decompiled with JetBrains decompiler
// Type: VPN.View.Controls.AnimatedImage

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
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

#nullable disable
namespace VPN.View.Controls
{
  public sealed partial class AnimatedImage : UserControl
  {
    private volatile bool _isStopped = true;
    private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    public static readonly DependencyProperty SourceStreamProperty = DependencyProperty.Register(nameof(SourceStream), typeof(IRandomAccessStream), typeof(AnimatedImage), new PropertyMetadata((object)null, new Windows.UI.Xaml.PropertyChangedCallback(AnimatedImage.PropertyChangedCallback)));
    public static readonly DependencyProperty StretchProperty = DependencyProperty.Register(nameof(Stretch), typeof(Stretch), typeof(AnimatedImage), new PropertyMetadata((object)null));

    public AnimatedImage()
    {
      this.InitializeComponent();
      this.Stretch = (Stretch)2;

      this.Unloaded += (s, e) =>
      {
        this._isStopped = true;
        this._cancellationTokenSource.Cancel();
      };

      this.Loaded += (s, e) =>
      {
        this._isStopped = false;
        if (this.SourceStream == null)
          return;
        _ = this.InitializeWithNewImage(this.SourceStream);
      };
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
      return new AnimatedImage.ImageFrameContainer((ImageSource)writeableBitmap, delay)
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
          this.InnerImage.Source = (ImageSource)null;
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
              PixelDataProvider pixelDataProvider = await frame.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, new BitmapTransform(), ExifOrientationMode.RespectExifOrientation, ColorManagementMode.ColorManageToSRgb);
              // Attempt to read frame delay from GIF metadata; fallback to 0.1s when unavailable
              TimeSpan delay = TimeSpan.FromSeconds(0.1);
              try
              {
                var props = await frame.BitmapProperties.GetPropertiesAsync(new List<string> { "/grctlext" });
                if (props != null && props.ContainsKey("/grctlext"))
                {
                  var val = props["/grctlext"].Value as BitmapTypedValue;
                  if (val != null)
                  {
                    var sub = val.Value as BitmapPropertiesView;
                    if (sub != null)
                    {
                      var delayProps = await sub.GetPropertiesAsync(new List<string> { "/Delay" });
                      if (delayProps != null && delayProps.ContainsKey("/Delay"))
                      {
                        var d = delayProps["/Delay"].Value;
                        if (d != null)
                        {
                          double parsed;
                          if (double.TryParse(d.ToString(), out parsed))
                            delay = TimeSpan.FromSeconds(parsed / 100.0);
                        }
                      }
                    }
                  }
                }
              }
              catch { }

              imageFrameContainerList = items;
              AnimatedImage.ImageFrameContainer frame1 = await AnimatedImage.GetFrame(pixelDataProvider, (int)frame.PixelWidth, (int)frame.PixelHeight, delay);
              imageFrameContainerList.Add(frame1);
              imageFrameContainerList = null;
              frame = null;
              pixelDataProvider = null;
            }
          }
          else
          {
            PixelDataProvider pixelDataAsync = await decoder.GetPixelDataAsync(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Straight, new BitmapTransform(), ExifOrientationMode.RespectExifOrientation, ColorManagementMode.ColorManageToSRgb);
            imageFrameContainerList = items;
            int pixelWidth = (int)decoder.PixelWidth;
            int pixelHeight = (int)decoder.PixelHeight;
            TimeSpan zero = TimeSpan.Zero;
            AnimatedImage.ImageFrameContainer frame = await AnimatedImage.GetFrame(pixelDataAsync, pixelWidth, pixelHeight, zero);
            imageFrameContainerList.Add(frame);
            imageFrameContainerList = null;
          }
          this._isStopped = false;
          this._cancellationTokenSource = new CancellationTokenSource();
          await this.ShowFramesLoopAsync(items, this._cancellationTokenSource.Token);
          items = null;
          decoder = null;
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
        while (!cancellationToken.IsCancellationRequested && !this._isStopped && ((FrameworkElement)this).Parent != null)
        {
          currentFrameNumber %= items.Count;
          AnimatedImage.ImageFrameContainer imageFrameContainer = items[currentFrameNumber];
          if (!this.IsStretch)
          {
            this.InnerImage.MaxWidth = (double)imageFrameContainer.PixelWidth;
            this.InnerImage.MaxHeight = (double)imageFrameContainer.PixelHeight;
          }
          if (this.NeedToEnlarge)
            this.EnlargeImage(imageFrameContainer.PixelWidth, imageFrameContainer.PixelHeight);
          this.InnerImage.Source = imageFrameContainer.ImageSource;
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
          this.InnerImage.MaxWidth = (double)imageFrameContainer.PixelWidth;
          this.InnerImage.MaxHeight = (double)imageFrameContainer.PixelHeight;
        }
        if (this.NeedToEnlarge)
          this.EnlargeImage(imageFrameContainer.PixelWidth, imageFrameContainer.PixelHeight);
        this.InnerImage.Source = imageFrameContainer.ImageSource;
      }
    }

    public bool IsStretch { get; set; }

    public bool NeedToEnlarge { get; set; }

    private void EnlargeImage(int currentWidth, int currentHeight)
    {
      Rect bounds = Window.Current.Bounds;
      if ((double)currentWidth > bounds.Width || (double)currentHeight > bounds.Height)
        return;
      double num1 = 1.5;
      double num2 = (double)currentWidth * num1;
      double num3 = (double)currentHeight * num1;
      if (num3 > bounds.Height || num2 > bounds.Width)
      {
        double num4 = Math.Min(bounds.Height / (double)currentHeight, bounds.Width / (double)currentWidth);
        if (num4 < 1.0)
          return;
        num2 = (double)currentWidth * num4;
        num3 = (double)currentHeight * num4;
      }
      this.InnerImage.MaxWidth = num2;
      this.InnerImage.MaxHeight = num3;
    }

    public IRandomAccessStream SourceStream
    {
      get
      {
        return (IRandomAccessStream)((DependencyObject)this).GetValue(AnimatedImage.SourceStreamProperty);
      }
      set => ((DependencyObject)this).SetValue(AnimatedImage.SourceStreamProperty, (object)value);
    }

    private static async void PropertyChangedCallback(
      DependencyObject dependencyObject,
      DependencyPropertyChangedEventArgs args)
    {
      AnimatedImage aImage = (AnimatedImage)dependencyObject;
      IRandomAccessStream stream = (IRandomAccessStream)args.NewValue;
      if (stream == null)
        await aImage.InitializeWithNewImage((IRandomAccessStream)null);
      else
        aImage.InitializeWithNewImage(stream);
    }

    public Stretch Stretch
    {
      get => (Stretch)((DependencyObject)this).GetValue(AnimatedImage.StretchProperty);
      set => ((DependencyObject)this).SetValue(AnimatedImage.StretchProperty, (object)value);
    }

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
