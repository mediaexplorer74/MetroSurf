// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Transitions.SlideAndZoomNavigationTransition
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace MetroLab.Common.Transitions
{
  public class SlideAndZoomNavigationTransition : MetroLabNavigationTransitionInfo
  {
    public override Storyboard RunGoInForwardTransition(NavigationTransitionArgs args)
    {
      UIElement targetUiElement = ((UserControl) args.TargetPage).Content;
      targetUiElement.put_CacheMode((CacheMode) new BitmapCache());
      Rect bounds = Window.Current.CoreWindow.Bounds;
      UIElement uiElement = targetUiElement;
      CompositeTransform compositeTransform = new CompositeTransform();
      compositeTransform.put_CenterX(bounds.Width / 2.0);
      compositeTransform.put_CenterY(bounds.Height / 2.0);
      compositeTransform.put_ScaleX(0.85);
      compositeTransform.put_ScaleY(0.85);
      uiElement.put_RenderTransform((Transform) compositeTransform);
      targetUiElement.put_Opacity(0.0);
      Storyboard storyboard1 = new Storyboard();
      ((Timeline) storyboard1).put_Duration((Duration) TimeSpan.FromMilliseconds(230.0));
      Storyboard.SetTarget((Timeline) storyboard1, (DependencyObject) targetUiElement);
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.put_To(new double?(1.0));
      ((Timeline) doubleAnimation1).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation1).put_AutoReverse(false);
      doubleAnimation1.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation1).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase1 = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase1).put_EasingMode((EasingMode) 0);
      doubleAnimation1.put_EasingFunction((EasingFunctionBase) quadraticEase1);
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation2);
      DoubleAnimation doubleAnimation3 = new DoubleAnimation();
      doubleAnimation3.put_To(new double?(1.0));
      ((Timeline) doubleAnimation3).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation3).put_AutoReverse(false);
      doubleAnimation3.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation3).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase2 = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase2).put_EasingMode((EasingMode) 0);
      doubleAnimation3.put_EasingFunction((EasingFunctionBase) quadraticEase2);
      DoubleAnimation doubleAnimation4 = doubleAnimation3;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation4, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation4);
      DoubleAnimation doubleAnimation5 = new DoubleAnimation();
      doubleAnimation5.put_To(new double?(1.0));
      ((Timeline) doubleAnimation5).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation5).put_AutoReverse(false);
      doubleAnimation5.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation5).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase3 = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase3).put_EasingMode((EasingMode) 0);
      doubleAnimation5.put_EasingFunction((EasingFunctionBase) quadraticEase3);
      DoubleAnimation doubleAnimation6 = doubleAnimation5;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation6, "Opacity");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation6);
      EventHandler<object> completed = (EventHandler<object>) null;
      completed = (EventHandler<object>) ((sender, o) =>
      {
        targetUiElement.put_CacheMode((CacheMode) null);
        Storyboard storyboard2 = (Storyboard) sender;
        targetUiElement.put_Opacity(1.0);
        if (targetUiElement.RenderTransform is CompositeTransform renderTransform2)
        {
          renderTransform2.put_ScaleX(1.0);
          renderTransform2.put_ScaleY(1.0);
        }
        // ISSUE: virtual method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>((object) storyboard2, __vmethodptr(storyboard2, remove_Completed)), completed);
        storyboard2.Stop();
      });
      Storyboard storyboard3 = storyboard1;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard3).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard3).remove_Completed), completed);
      storyboard1.Begin();
      return storyboard1;
    }

    public override Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args)
    {
      UIElement targetUiElement = ((UserControl) args.TargetPage).Content;
      targetUiElement.put_CacheMode((CacheMode) new BitmapCache());
      targetUiElement.put_RenderTransform((Transform) new CompositeTransform());
      targetUiElement.put_Opacity(1.0);
      Storyboard storyboard1 = new Storyboard();
      ((Timeline) storyboard1).put_Duration((Duration) TimeSpan.FromMilliseconds(230.0));
      Storyboard.SetTarget((Timeline) storyboard1, (DependencyObject) targetUiElement);
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.put_To(new double?(120.0));
      ((Timeline) doubleAnimation1).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation1).put_AutoReverse(false);
      doubleAnimation1.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation1).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase1 = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase1).put_EasingMode((EasingMode) 1);
      doubleAnimation1.put_EasingFunction((EasingFunctionBase) quadraticEase1);
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation2);
      DoubleAnimation doubleAnimation3 = new DoubleAnimation();
      doubleAnimation3.put_To(new double?(0.0));
      ((Timeline) doubleAnimation3).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation3).put_AutoReverse(false);
      doubleAnimation3.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation3).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase2 = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase2).put_EasingMode((EasingMode) 1);
      doubleAnimation3.put_EasingFunction((EasingFunctionBase) quadraticEase2);
      DoubleAnimation doubleAnimation4 = doubleAnimation3;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation4, "Opacity");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation4);
      EventHandler<object> completed = (EventHandler<object>) null;
      completed = (EventHandler<object>) ((sender, o) =>
      {
        targetUiElement.put_CacheMode((CacheMode) null);
        Storyboard storyboard2 = (Storyboard) sender;
        targetUiElement.put_Opacity(0.0);
        if (args.OnCompletedAction != null)
          args.OnCompletedAction();
        // ISSUE: virtual method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>((object) storyboard2, __vmethodptr(storyboard2, remove_Completed)), completed);
        storyboard2.Stop();
      });
      Storyboard storyboard3 = storyboard1;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard3).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard3).remove_Completed), completed);
      storyboard1.Begin();
      return storyboard1;
    }

    public override Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args)
    {
      UIElement targetUiElement = ((UserControl) args.TargetPage).Content;
      targetUiElement.put_CacheMode((CacheMode) new BitmapCache());
      targetUiElement.put_Opacity(0.0);
      Storyboard storyboard1 = new Storyboard();
      ((Timeline) storyboard1).put_Duration((Duration) TimeSpan.FromMilliseconds(230.0));
      Storyboard.SetTarget((Timeline) storyboard1, (DependencyObject) targetUiElement);
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.put_To(new double?(1.0));
      ((Timeline) doubleAnimation1).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation1).put_AutoReverse(false);
      doubleAnimation1.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation1).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase).put_EasingMode((EasingMode) 0);
      doubleAnimation1.put_EasingFunction((EasingFunctionBase) quadraticEase);
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, "Opacity");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation2);
      EventHandler<object> completed = (EventHandler<object>) null;
      completed = (EventHandler<object>) ((sender, o) =>
      {
        targetUiElement.put_CacheMode((CacheMode) null);
        Storyboard storyboard2 = (Storyboard) sender;
        targetUiElement.put_Opacity(1.0);
        // ISSUE: virtual method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>((object) storyboard2, __vmethodptr(storyboard2, remove_Completed)), completed);
        storyboard2.Stop();
      });
      Storyboard storyboard3 = storyboard1;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard3).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard3).remove_Completed), completed);
      storyboard1.Begin();
      return storyboard1;
    }

    public override Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args)
    {
      UIElement targetUiElement = ((UserControl) args.TargetPage).Content;
      targetUiElement.put_CacheMode((CacheMode) new BitmapCache());
      targetUiElement.put_Opacity(1.0);
      Storyboard storyboard1 = new Storyboard();
      ((Timeline) storyboard1).put_Duration((Duration) TimeSpan.FromMilliseconds(230.0));
      Storyboard.SetTarget((Timeline) storyboard1, (DependencyObject) targetUiElement);
      DoubleAnimation doubleAnimation1 = new DoubleAnimation();
      doubleAnimation1.put_To(new double?(0.0));
      ((Timeline) doubleAnimation1).put_Duration(new Duration(TimeSpan.FromMilliseconds(220.0)));
      ((Timeline) doubleAnimation1).put_AutoReverse(false);
      doubleAnimation1.put_EnableDependentAnimation(true);
      ((Timeline) doubleAnimation1).put_FillBehavior((FillBehavior) 0);
      QuadraticEase quadraticEase = new QuadraticEase();
      ((EasingFunctionBase) quadraticEase).put_EasingMode((EasingMode) 1);
      doubleAnimation1.put_EasingFunction((EasingFunctionBase) quadraticEase);
      DoubleAnimation doubleAnimation2 = doubleAnimation1;
      Storyboard.SetTargetProperty((Timeline) doubleAnimation2, "Opacity");
      ((ICollection<Timeline>) storyboard1.Children).Add((Timeline) doubleAnimation2);
      EventHandler<object> completed = (EventHandler<object>) null;
      completed = (EventHandler<object>) ((sender, o) =>
      {
        targetUiElement.put_CacheMode((CacheMode) null);
        Storyboard storyboard2 = (Storyboard) sender;
        targetUiElement.put_Opacity(0.0);
        if (args.OnCompletedAction != null)
          args.OnCompletedAction();
        // ISSUE: virtual method pointer
        WindowsRuntimeMarshal.RemoveEventHandler<EventHandler<object>>(new Action<EventRegistrationToken>((object) storyboard2, __vmethodptr(storyboard2, remove_Completed)), completed);
        storyboard2.Stop();
      });
      Storyboard storyboard3 = storyboard1;
      WindowsRuntimeMarshal.AddEventHandler<EventHandler<object>>(new Func<EventHandler<object>, EventRegistrationToken>(((Timeline) storyboard3).add_Completed), new Action<EventRegistrationToken>(((Timeline) storyboard3).remove_Completed), completed);
      storyboard1.Begin();
      return storyboard1;
    }
  }
}
