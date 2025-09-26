// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
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
        private Storyboard CreateScaleAndFadeStoryboard(UIElement target, double durationMs = 220, bool fadeIn = true)
        {
            var sb = new Storyboard();
            sb.Duration = new Duration(TimeSpan.FromMilliseconds(230));
            Storyboard.SetTarget(sb, target);

            var ease = new QuadraticEase { EasingMode = EasingMode.EaseOut };

            var animScaleX = new DoubleAnimation
            {
                To = fadeIn ? 1.0 : 0.85,
                Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop,
                EasingFunction = ease
            };
            Storyboard.SetTargetProperty(animScaleX, "(UIElement.RenderTransform).(CompositeTransform.ScaleX)");
            sb.Children.Add(animScaleX);

            var animScaleY = new DoubleAnimation
            {
                To = fadeIn ? 1.0 : 0.85,
                Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop,
                EasingFunction = ease
            };
            Storyboard.SetTargetProperty(animScaleY, "(UIElement.RenderTransform).(CompositeTransform.ScaleY)");
            sb.Children.Add(animScaleY);

            var animOpacity = new DoubleAnimation
            {
                To = fadeIn ? 1.0 : 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(durationMs)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop
            };
            Storyboard.SetTargetProperty(animOpacity, "Opacity");
            sb.Children.Add(animOpacity);

            return sb;
        }

        public override Storyboard RunGoInForwardTransition(NavigationTransitionArgs args)
        {
            if (!(args.TargetPage is UserControl uc))
                return null;

            var target = uc.Content as UIElement;
            if (target == null)
                return null;

            // prepare transform and initial state
            target.CacheMode = new BitmapCache();
            var bounds = Window.Current.CoreWindow.Bounds;
            var ct = new CompositeTransform
            {
                CenterX = bounds.Width / 2.0,
                CenterY = bounds.Height / 2.0,
                ScaleX = 0.85,
                ScaleY = 0.85
            };
            target.RenderTransform = ct;
            target.Opacity = 0.0;

            var sb = CreateScaleAndFadeStoryboard(target, 220, fadeIn: true);

            void Completed(object s, object e)
            {
                target.CacheMode = null;
                target.Opacity = 1.0;
                if (target.RenderTransform is CompositeTransform rt)
                {
                    rt.ScaleX = 1.0;
                    rt.ScaleY = 1.0;
                }
                sb.Completed -= Completed;
                sb.Stop();
            }

            sb.Completed += Completed;
            sb.Begin();
            return sb;
        }

        public override Storyboard RunGoAwayBackwardTransition(NavigationTransitionArgs args)
        {
            if (!(args.TargetPage is UserControl uc))
                return null;
            var target = uc.Content as UIElement;
            if (target == null)
                return null;

            target.CacheMode = new BitmapCache();
            target.RenderTransform = new CompositeTransform();
            target.Opacity = 1.0;

            var sb = new Storyboard();
            sb.Duration = new Duration(TimeSpan.FromMilliseconds(230));
            Storyboard.SetTarget(sb, target);

            var ease = new QuadraticEase { EasingMode = EasingMode.EaseIn };

            var animTranslateY = new DoubleAnimation
            {
                To = 120.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(220)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop,
                EasingFunction = ease
            };
            Storyboard.SetTargetProperty(animTranslateY, "(UIElement.RenderTransform).(CompositeTransform.TranslateY)");
            sb.Children.Add(animTranslateY);

            var animOpacity = new DoubleAnimation
            {
                To = 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(220)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop,
                EasingFunction = ease
            };
            Storyboard.SetTargetProperty(animOpacity, "Opacity");
            sb.Children.Add(animOpacity);

            void Completed(object s, object e)
            {
                target.CacheMode = null;
                target.Opacity = 0.0;
                args.OnCompletedAction?.Invoke();
                sb.Completed -= Completed;
                sb.Stop();
            }

            sb.Completed += Completed;
            sb.Begin();
            return sb;
        }

        public override Storyboard RunGoInBackwardTransition(NavigationTransitionArgs args)
        {
            if (!(args.TargetPage is UserControl uc))
                return null;
            var target = uc.Content as UIElement;
            if (target == null)
                return null;

            target.CacheMode = new BitmapCache();
            target.Opacity = 0.0;

            var sb = new Storyboard();
            sb.Duration = new Duration(TimeSpan.FromMilliseconds(230));
            Storyboard.SetTarget(sb, target);

            var animOpacity = new DoubleAnimation
            {
                To = 1.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(220)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop
            };
            Storyboard.SetTargetProperty(animOpacity, "Opacity");
            sb.Children.Add(animOpacity);

            void Completed(object s, object e)
            {
                target.CacheMode = null;
                target.Opacity = 1.0;
                sb.Completed -= Completed;
                sb.Stop();
            }

            sb.Completed += Completed;
            sb.Begin();
            return sb;
        }

        public override Storyboard RunGoAwayForwardTransition(NavigationTransitionArgs args)
        {
            if (!(args.TargetPage is UserControl uc))
                return null;
            var target = uc.Content as UIElement;
            if (target == null)
                return null;

            target.CacheMode = new BitmapCache();
            target.Opacity = 1.0;

            var sb = new Storyboard();
            sb.Duration = new Duration(TimeSpan.FromMilliseconds(230));
            Storyboard.SetTarget(sb, target);

            var animOpacity = new DoubleAnimation
            {
                To = 0.0,
                Duration = new Duration(TimeSpan.FromMilliseconds(220)),
                EnableDependentAnimation = true,
                FillBehavior = FillBehavior.Stop,
                EasingFunction = new QuadraticEase { EasingMode = EasingMode.EaseIn }
            };
            Storyboard.SetTargetProperty(animOpacity, "Opacity");
            sb.Children.Add(animOpacity);

            void Completed(object s, object e)
            {
                target.CacheMode = null;
                target.Opacity = 0.0;
                args.OnCompletedAction?.Invoke();
                sb.Completed -= Completed;
                sb.Stop();
            }

            sb.Completed += Completed;
            sb.Begin();
            return sb;
        }
    }
}
