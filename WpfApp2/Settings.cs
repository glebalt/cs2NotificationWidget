using System;
using System.Collections.Generic;
using System.Text;

namespace WpfApp2
{
    public class Settings
    {
        public int duration { get; set; }
        public App.NotificationPosition position { get; set; }


        public Settings()
        {
            duration = 5;
            position = App.NotificationPosition.Rightbottom;
        }
    }
}
