using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace WpfApp2.Services
{
    public class NotificationService
    {
        private static INotification? currentNotification = null;
        

        public NotificationService(){
          
        }

        public void DisplayNotification()
        {
          

        }

        public static void UpdateNotification(INotification notification)
        {
            currentNotification?.CloseWindow();
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    currentNotification = notification;
                     notification.ShowWindow();
              
                });
           


        }

        public static void UpdateNotification()
        {
            currentNotification = null;
        }
    }
}
