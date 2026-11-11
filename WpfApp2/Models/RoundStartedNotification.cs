using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WpfApp2.Services;

namespace WpfApp2.Models
{
    public partial class RoundStartedNotification : Window, INotification
    {
        public App.NotificationPosition position;
        public RoundStartedNotification()
        {
            InitializeComponent();
            Loaded += WindowLoaded;
        }

        public RoundStartedNotification(App.NotificationPosition position)
        {
            this.position = position;
            InitializeComponent();
            Loaded += WindowLoaded;
        }
        public void SetPosition()
        {


            var workArea = SystemParameters.WorkArea;
            if (position == App.NotificationPosition.Topleft)
            {
                Left = workArea.Left + 20;
                Top = workArea.Top + 20;
            }
            else if (position == App.NotificationPosition.Topright)
            {
                Left = workArea.Right - Width - 20;
                Top = workArea.Top + 20;
            }
            else if (position == App.NotificationPosition.Leftbottom)
            {
                Left = workArea.Left + 20;
                Top = workArea.Bottom - Height;
            }
            else if (position == App.NotificationPosition.Rightbottom)
            {
                Left = workArea.Right - Width - 20;
                Top = workArea.Top + 20;
            }
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

        public async void WindowLoaded(object sender, RoutedEventArgs e)
        {
            SetPosition();

            // 1. Появление: от 80% до 108%
          

            await  Animate();

           

            System.Diagnostics.Debug.WriteLine("SIEVBEN");
            DispatcherTimer timer = new();
            timer.Interval = new TimeSpan(0, 0, 5);

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
