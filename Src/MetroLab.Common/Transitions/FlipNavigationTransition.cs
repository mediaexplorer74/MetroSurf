// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Transitions.FlipNavigationTransition
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace MetroLab.Common.Transitions
{
  public class FlipNavigationTransition : MetroLabNavigationTransitionInfo
  {
    private const double PlaneMagnitude = 40.0;
    private const double MinScale = 0.8;

    private Storyboard RunTransition(
      MvvmPage page,
      EasingFunctionBase easingFunction,
      double startOpacity,
      double endOpacity,
      double startRotationY,
      double endRotationY,
      Action onCompleted = null)
    {
      UIElement targetUiElement = ((UserControl) page).Content;

      var planeProjection = new PlaneProjection();
      planeProjection.CenterOfRotationX = 0.0;
      planeProjection.CenterOfRotationY = 0.0;
      planeProjection.CenterOfRotationZ = -70.0;
      targetUiElement.Projection = planeProjection;

      targetUiElement.CacheMode = new BitmapCache();
      planeProjection.RotationY = startRotationY;
      targetUiElement.Opacity = startOpacity;

      var storyboard = new Storyboard();
      storyboard.Duration = new Duration(TimeSpan.FromMilliseconds(150.0));
      Storyboard.SetTarget(storyboard, targetUiElement);

      var rotationAnim = new DoubleAnimation
      {
        To = endRotationY,
        Duration = new Duration(TimeSpan.FromMilliseconds(140.0)),
        AutoReverse = false,
        FillBehavior = FillBehavior.Stop,
        EnableDependentAnimation = true,
        EasingFunction = easingFunction
      };
      Storyboard.SetTargetProperty(rotationAnim, "(UIElement.Projection).(PlaneProjection.RotationY)");
      storyboard.Children.Add(rotationAnim);

      var opacityAnim = new DoubleAnimation
      {
        To = endOpacity,
        Duration = new Duration(TimeSpan.FromMilliseconds(140.0)),
        AutoReverse = false,
        EnableDependentAnimation = true,
        FillBehavior = FillBehavior.Stop,
        EasingFunction = easingFunction
      };
      Storyboard.SetTargetProperty(opacityAnim, "Opacity");
      storyboard.Children.Add(opacityAnim);

      EventHandler<object> completed = null;
      completed = (sender, o) =>
      {
        targetUiElement.CacheMode = null;
        var sb = (Storyboard)sender;
        if (targetUiElement.Projection is PlaneProjection p)
          p.RotationY = endRotationY;
        targetUiElement.Opacity = endOpacity;
        onCompleted?.Invoke();
        sb.Completed -= completed;
        sb.Stop();
      };

      storyboard.Completed += completed;
      storyboard.Begin();
      return storyboard;
    }

    public override Storyboard RunGoInForwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 0.0, 1.0, -40.0, 0.0, onCompletedAction);
    }

    public override Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 1.0, 0.0, 0.0, -40.0, onCompletedAction);
    }

    public override Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseOut };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 0.0, 1.0, 40.0, 0.0, onCompletedAction);
    }

    public override Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      var easingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 1.0, 0.0, 0.0, 40.0, onCompletedAction);
    }
  }
}
