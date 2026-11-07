using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace WpfApp2.Models
{
    public partial class FreezetimeStartedNotification : Window,INotification
    {

        public FreezetimeStartedNotification()
        {
            InitializeComponent();
            Loaded += WindowLoaded;
        }
        public void SetPosition()
        {
            var workArea = SystemParameters.WorkArea;
            WindowStartupLocation = WindowStartupLocation.Manual;
            Left = workArea.Right - Width - 20;
            Top = workArea.Bottom - Height - 20;
        }

        public async Task Animate()
        {
            var scaleUp = new DoubleAnimation
            {
                From = 0.2,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new BackEase
                {
                    Amplitude = 0.3,
                    EasingMode = EasingMode.EaseOut
                }
            };


            Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleUp);
            Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleUp);


            await Task.Delay(2000);

            var scaleDown = new DoubleAnimation
            {
                From = 1,
                To = 0.1,
                Duration = TimeSpan.FromSeconds(0.25),
                EasingFunction = new BackEase
                {
                    Amplitude = 0.3,
                    EasingMode = EasingMode.EaseOut
                }
            };

            Scale.BeginAnimation(ScaleTransform.ScaleXProperty, scaleDown);
            Scale.BeginAnimation(ScaleTransform.ScaleYProperty, scaleDown);
        }


        public async void  WindowLoaded(object sender, RoutedEventArgs e)
        {
          
            SetPosition();
            await Animate();
            DispatcherTimer timer = new();
            timer.Interval = new TimeSpan(0, 0, 2);

            timer.Tick += (sender, e) =>
            {

                timer.Stop();
                NotificationService.UpdateNotification();
                Close();
            };

            timer.Start();
        }

        public void ShowWindow()
        {
            this.Show();
        }

        public void CloseWindow()
        {
            this.Close();
        }
    }
}
