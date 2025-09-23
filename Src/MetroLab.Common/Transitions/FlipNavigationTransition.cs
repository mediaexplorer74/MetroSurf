// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Transitions.FlipNavigationTransition
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
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
      PlaneProjection planeProjection1 = new PlaneProjection();
      planeProjection1.put_CenterOfRotationX(0.0);
      planeProjection1.put_CenterOfRotationY(0.0);
      planeProjection1.put_CenterOfRotationZ(-70.0);
      PlaneProjection planeProjection2 = planeProjection1;
      targetUiElement.put_Projection((Projection) planeProjection2);
      targetUiElement.put_CacheMode((CacheMode) new BitmapCache());
      planeProjection2.put_RotationY(startRotationY);
      targetUiElement.put_Opacity(startOpacity);
      Storyboard storyboard1 = new Storyboard();
      ((Timeline) storyboard1).put_Duration((Duration) TimeSpan.FromMilliseconds(150.0));
      Storyboard.SetTarget((Timeline) storyboard1, (DependencyObject) targetUiElement);
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.put_To(new double?(endRotationY));
      ((Timeline) doubleAnimation1).put_Duration(new Duration(TimeSpan.FromMilliseconds(140.0)));
      ((Timeline) doubleAnimation1).put_AutoReverse(false);
      ((Timeline) doubleAnimation1).put_FillBehavior((FillBehavior) 0);
      doubleAnimation1.put_EasingFunction(easingFunction);
      doubleAnimation1.put_EnableDependentAnimation(true);
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, "(UIElement.Projection).(PlaneProjection.RotationY)");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation2);
      DoubleAnimation doubleAnimation3 = new DoubleAnimation();
      doubleAnimation3.put_To(new double?(endOpacity));
      ((Timeline) doubleAnimation3).put_Duration(new Duration(TimeSpan.FromMilliseconds(140.0)));
      ((Timeline) doubleAnimation3).put_AutoReverse(false);
      doubleAnimation3.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation3).put_FillBehavior((FillBehavior) 0);
      doubleAnimation3.put_EasingFunction(easingFunction);
      DoubleAnimation doubleAnimation4 = doubleAnimation3;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation4, "Opacity");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation4);
      EventHandler<object> completed = (EventHandler<object>) null;
      completed = (EventHandler<object>) ((sender, o) =>
      {
        targetUiElement.put_CacheMode((CacheMode) null);
        Storyboard storyboard2 = (Storyboard) sender;
        ((PlaneProjection) targetUiElement.Projection).put_RotationY(endRotationY);
        targetUiElement.put_Opacity(endOpacity);
        if (onCompleted != null)
          onCompleted();
        // ISSUE: virtual method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>((object) storyboard2, __vmethodptr(storyboard2, remove_Completed)), completed);
        storyboard2.Stop();
      });
      Storyboard storyboard3 = storyboard1;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard3).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard3).remove_Completed), completed);
      storyboard1.Begin();
      return storyboard1;
    }

    public override Storyboard RunGoInForwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase();
      ((EasingFunctionBase) easingFunction).put_EasingMode((EasingMode) 0);
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 0.0, 1.0, -40.0, 0.0, onCompletedAction);
    }

    public override Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase();
      ((EasingFunctionBase) easingFunction).put_EasingMode((EasingMode) 1);
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 1.0, 0.0, 0.0, -40.0, onCompletedAction);
    }

    public override Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase();
      ((EasingFunctionBase) easingFunction).put_EasingMode((EasingMode) 0);
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 0.0, 1.0, 40.0, 0.0, onCompletedAction);
    }

    public override Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args)
    {
      MvvmPage targetPage = args.TargetPage;
      QuadraticEase easingFunction = new QuadraticEase();
      ((EasingFunctionBase) easingFunction).put_EasingMode((EasingMode) 1);
      Action onCompletedAction = args.OnCompletedAction;
      return this.RunTransition(targetPage, (EasingFunctionBase) easingFunction, 1.0, 0.0, 0.0, 40.0, onCompletedAction);
    }
  }
}
