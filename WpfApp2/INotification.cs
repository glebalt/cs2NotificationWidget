using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp2
{
    public interface INotification 
    {
        public void WindowLoaded(object sender, RoutedEventArgs e);
        public void SetPosition();

        public void ShowWindow();

        public void CloseWindow();
    }
}
