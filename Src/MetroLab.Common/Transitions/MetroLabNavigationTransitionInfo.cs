// Decompiled with JetBrains decompiler
// Type: MetroLab.Common.Transitions.MetroLabNavigationTransitionInfo
// Assembly: MetroLab.Common, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DBAE00CF-9C6B-4D8D-ACBC-54BA0CE44A06
// Assembly location: C:\Users\Admin\Desktop\RE\VPN_4.14.1.52\1\MetroLab.Common.dll

using Windows.UI.Xaml.Media.Animation;

#nullable disable
namespace MetroLab.Common.Transitions
{
  public abstract class MetroLabNavigationTransitionInfo
  {
    public abstract Storyboard RunGoInForwardTransition(NavigationTransitionArgs args);

    public abstract Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args);

    public abstract Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args);

    public abstract Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args);
  }
}
