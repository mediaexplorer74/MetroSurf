// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Transitions.Slide2NavigationTransition
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
  public class Slide2NavigationTransition : MetroLabNavigationTransitionInfo
  {
    private const double TranslateMagnitude = 120.0;

    private Storyboard RunTransition(
      MvvmPage page,
      EasingFunctionBase easingFunction,
      double startOpacity,
      double endOpacity,
      double startPosition,
      double endPosition,
      bool animateYAxis = true,
      Action onCompleted = null)
    {
      UIElement targetUiElement = ((UserControl) page).Content;
      UIElement uiElement = targetUiElement;
      TranslateTransform translateTransform1;
      if (!animateYAxis)
      {
        translateTransform1 = new TranslateTransform() { X = startPosition };
      }
      else
      {
        translateTransform1 = new TranslateTransform() { Y = startPosition };
      }
      uiElement.RenderTransform = translateTransform1;
      targetUiElement.Opacity = startOpacity;
      Storyboard storyboard1 = new Storyboard();
      storyboard1.Duration = new Duration(TimeSpan.FromMilliseconds(230.0));
      Storyboard.SetTarget(storyboard1, targetUiElement);
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.From = startPosition;
      doubleAnimation1.To = endPosition;
      doubleAnimation1.Duration = new Duration(TimeSpan.FromMilliseconds(220.0));
      doubleAnimation1.AutoReverse = false;
      doubleAnimation1.EnableDependentAnimation = true;
      doubleAnimation1.FillBehavior = FillBehavior.Stop;
      doubleAnimation1.EasingFunction = easingFunction;
      Storyboard.SetTargetProperty(doubleAnimation1, animateYAxis ? "(UIElement.RenderTransform).(TranslateTransform.Y)" : "(UIElement.RenderTransform).(TranslateTransform.X)");
      storyboard1.Children.Add(doubleAnimation1);
      DoubleAnimation doubleAnimation3 = new DoubleAnimation();
      doubleAnimation3.From = startOpacity;
      doubleAnimation3.To = endOpacity;
      doubleAnimation3.Duration = new Duration(TimeSpan.FromMilliseconds(220.0));
      doubleAnimation3.AutoReverse = false;
      doubleAnimation3.EnableDependentAnimation = true;
      doubleAnimation3.FillBehavior = FillBehavior.Stop;
      // ease for opacity change
      var quadraticEase = new QuadraticEase() { EasingMode = EasingMode.EaseIn };
      doubleAnimation3.EasingFunction = quadraticEase;
      Storyboard.SetTargetProperty(doubleAnimation3, "Opacity");
      storyboard1.Children.Add(doubleAnimation3);
      EventHandler<object> completed = null;
      completed = (sender, o) =>
      {
        Storyboard storyboard2 = (Storyboard)sender;
        targetUiElement.Opacity = endOpacity;
        TranslateTransform renderTransform = (TranslateTransform)targetUiElement.RenderTransform;
        if (animateYAxis)
          renderTransform.Y = endPosition;
        else
          renderTransform.X = endPosition;
        onCompleted?.Invoke();
        storyboard2.Completed -= completed;
        storyboard2.Stop();
      };
      storyboard1.Completed += completed;
      storyboard1.Begin();
      return storyboard1;
    }

    public override Storyboard RunGoInForwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase)easingFunction, 0.0, 1.0, 120.0, 0.0, false, onCompletedAction);
    }

    public override Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseIn };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase)easingFunction, 1.0, 0.0, 0.0, -120.0, onCompleted: onCompletedAction);
    }

    public override Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseOut };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase)easingFunction, 0.0, 1.0, 120.0, 0.0, false, onCompletedAction);
    }

    public override Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase() { EasingMode = EasingMode.EaseIn };
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase)easingFunction, 1.0, 0.0, 0.0, 120.0, onCompleted: onCompletedAction);
    }
  }
}
