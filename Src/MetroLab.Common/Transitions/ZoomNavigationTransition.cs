// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Transitions.ZoomNavigationTransition
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace MetroLab.Common.Transitions
{
  public class ZoomNavigationTransition : MetroLabNavigationTransitionInfo
  {
    private const double ScaleMagnitude = 0.85;

    private Storyboard RunTransition(
      NavigationTransitionArgs args,
      EasingFunctionBase easingFunction,
      double startOpacity,
      double endOpacity,
      double startScale,
      double endScale)
    {
      UIElement targetUiElement = ((UserControl) args.TargetPage).Content;
      targetUiElement.CacheMode = new BitmapCache();
      Rect bounds = Window.Current.CoreWindow.Bounds;

      var compositeTransform = new CompositeTransform
      {
        CenterX = bounds.Width / 2.0,
        CenterY = bounds.Height / 2.0,
        ScaleX = startScale,
        ScaleY = startScale
      };
      targetUiElement.RenderTransform = compositeTransform;
      targetUiElement.Opacity = startOpacity;

      var storyboard = new Storyboard();
      storyboard.Duration = new Duration(TimeSpan.FromMilliseconds(230.0));
      Storyboard.SetTarget(storyboard, targetUiElement);

      var animScaleX = new DoubleAnimation
      {
        To = endScale,
        Duration = new Duration(TimeSpan.FromMilliseconds(220.0)),
        AutoReverse = false,
        EnableDependentAnimation = true,
        FillBehavior = FillBehavior.Stop,
        EasingFunction = easingFunction
      };
      Storyboard.SetTargetProperty(animScaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
      storyboard.Children.Add(animScaleX);

      var animScaleY = new DoubleAnimation
      {
        To = endScale,
        Duration = new Duration(TimeSpan.FromMilliseconds(220.0)),
        AutoReverse = false,
        EnableDependentAnimation = true,
        FillBehavior = FillBehavior.Stop,
        EasingFunction = easingFunction
      };
      Storyboard.SetTargetProperty(animScaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");
      storyboard.Children.Add(animScaleY);

      var animOpacity = new DoubleAnimation
      {
        To = endOpacity,
        Duration = new Duration(TimeSpan.FromMilliseconds(220.0)),
        AutoReverse = false,
        EnableDependentAnimation = true,
        FillBehavior = FillBehavior.Stop,
        EasingFunction = easingFunction
      };
      Storyboard.SetTargetProperty(animOpacity, "Opacity");
      storyboard.Children.Add(animOpacity);

      EventHandler<object> completed = null;
      completed = (sender, o) =>
      {
        targetUiElement.CacheMode = null;
        var sb = (Storyboard)sender;
        targetUiElement.Opacity = endOpacity;
        if (targetUiElement.RenderTransform is CompositeTransform renderTransform2)
        {
          renderTransform2.ScaleX = endScale;
          renderTransform2.ScaleY = endScale;
        }
        args.OnCompletedAction?.Invoke();
        sb.Completed -= completed;
        sb.Stop();
      };

      storyboard.Completed += completed;
      storyboard.Begin();
      return storyboard;
    }

    public override Storyboard RunGoInForwardTransition(NavigationTransitionArgs args)
    {
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };
      return this.RunTransition(args, easingFunction, 0.0, 1.0, 0.85, 1.0);
    }

    public override Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args)
    {
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn };
      return this.RunTransition(args, easingFunction, 1.0, 0.0, 1.0, 0.85);
    }

    public override Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args)
    {
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };
      return this.RunTransition(args, easingFunction, 0.0, 1.0, 0.85, 1.0);
    }

    public override Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args)
    {
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn };
      return this.RunTransition(args, easingFunction, 1.0, 0.0, 1.0, 0.85);
    }
  }
}
