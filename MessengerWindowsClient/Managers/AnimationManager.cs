using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MessengerWindowsClient.Managers
{
    public class AnimationManager
    {
        public double WindowWidth { private get; set; }

        private FrameworkElement _newPage;
        private FrameworkElement _oldPage;
        private FrameworkElement _container;
        private double _containerInitialWidth;
        private HorizontalAlignment _alignment;

        public AnimationManager(FrameworkElement container, double windowWidth)
        {
            _container = container;
            WindowWidth = windowWidth;
        }

        public void AnimateForwardPage(Control newPage, Control oldPage)
        {
            _newPage = newPage;
            _oldPage = oldPage;
            _containerInitialWidth = WindowWidth;
            _alignment = HorizontalAlignment.Right;
            BeginDoubleAnimation(HorizontalAlignment.Left);
        }

        public void AnimateBackwardPage(Control newPage, Control oldPage)
        {
            _newPage = newPage;
            _oldPage = oldPage;
            _containerInitialWidth = WindowWidth;
            _alignment = HorizontalAlignment.Left;
            BeginDoubleAnimation(HorizontalAlignment.Right);
        }

        private void BeginDoubleAnimation(HorizontalAlignment toSide)
        {
            var animation = CreateDoubleAnimation(_containerInitialWidth, 0, 0.1, TimeSpan.FromSeconds(0.5));
            animation.Completed += ContinueAnimation;
            _container.HorizontalAlignment = toSide;
            RunStoryboard(animation, new PropertyPath(FrameworkElement.WidthProperty));
        }

        private void ContinueAnimation(object sender, EventArgs e)
        {
            (_container as Panel).Children.Remove(_oldPage);
            (_container as Panel).Children.Add(_newPage);
            _newPage.Visibility = Visibility.Visible;
            var animation = CreateDoubleAnimation(0, _containerInitialWidth, 0.1, TimeSpan.FromSeconds(0.5));
            animation.Completed += FinishAnimation;
            _container.HorizontalAlignment = _alignment;
            RunStoryboard(animation, new PropertyPath(FrameworkElement.WidthProperty));
        }

        private void FinishAnimation(object sender, EventArgs e)
        {
            _container.HorizontalAlignment = HorizontalAlignment.Stretch;
        }

        private DoubleAnimation CreateDoubleAnimation(double from, double to, double acceleration, TimeSpan duration)
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

        private void RunStoryboard(Timeline animation, PropertyPath property)
        {
            var storyBoard = new Storyboard();
            storyBoard.Children.Add(animation);
            Storyboard.SetTarget(animation, _container);
            Storyboard.SetTargetProperty(animation, property);
            storyBoard.Begin();
        }
    }
}
