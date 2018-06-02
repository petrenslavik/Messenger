﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MessengerWindowsClient
{
    public static class AnimationManager
    {
        private static FrameworkElement _newPage;
        private static FrameworkElement _oldPage;
        private static FrameworkElement _container;
        private static double _containerInitialWidth;
        private static HorizontalAlignment _alignment;

        static AnimationManager()
        {

        }

        public static void AnimateForwardPage(Control newPage, Control oldPage, FrameworkElement container, double windowWidth)
        {
            _newPage = newPage;
            _oldPage = oldPage;
            _container = container;
            _containerInitialWidth = windowWidth;
            _alignment = HorizontalAlignment.Right;
            BeginDoubleAnimation(HorizontalAlignment.Left);
        }

        public static void AnimateBackwardPage(Control newPage, Control oldPage, FrameworkElement container, double windowWidth)
        {
            _newPage = newPage;
            _oldPage = oldPage;
            _container = container;
            _containerInitialWidth = windowWidth;
            _alignment = HorizontalAlignment.Left;
            BeginDoubleAnimation(HorizontalAlignment.Right);
        }

        private static void BeginDoubleAnimation(HorizontalAlignment toSide)
        {
            var animation = CreateDoubleAnimation(_containerInitialWidth, 0, 0.1, TimeSpan.FromSeconds(0.5));
            animation.Completed += ContinueAnimation;
            _container.HorizontalAlignment = toSide;
            RunStoryboard(animation, new PropertyPath(FrameworkElement.WidthProperty));
        }

        private static void ContinueAnimation(object sender, EventArgs e)
        {
            _oldPage.Visibility = Visibility.Collapsed;
            _newPage.Visibility = Visibility.Visible;
            var animation = CreateDoubleAnimation(0, _containerInitialWidth, 0.1, TimeSpan.FromSeconds(0.5));
            animation.Completed += FinishAnimation;
            _container.HorizontalAlignment = _alignment;
            RunStoryboard(animation, new PropertyPath(FrameworkElement.WidthProperty));
        }

        private static void FinishAnimation(object sender, EventArgs e)
        {
            _container.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private static DoubleAnimation CreateDoubleAnimation(double from, double to, double acceleration, TimeSpan duration)
        {
            return new DoubleAnimation
            {
                EasingFunction = new QuarticEase(),
                From = from,
                To = to,
                AccelerationRatio = acceleration,
                Duration = duration
            };
        }

        private static void RunStoryboard(Timeline animation, PropertyPath property)
        {
            var storyBoard = new Storyboard();
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, _container);
            Storyboard.SetTargetProperty(animation, property);
            storyBoard.Begin();
        }
    }
}
