using System;
using System.Collections.Generic;
using System.Text;
using Forms = System.Windows.Forms;


namespace WpfApp2.Services
{
    public  class TrayMenuBuildService
    {
        private readonly Forms.NotifyIcon icon;
        private readonly StateManager stateManager;
        private readonly string iconPath;

        public TrayMenuBuildService(NotifyIcon icon,StateManager man,string iconPath    )
        {
            this.icon = icon;
            stateManager = man;
            this.iconPath = iconPath;
        }

        public void BuildTrayMenu()
        {
            icon.Icon = new System.Drawing.Icon(iconPath);
            icon.Visible = true;
            icon.Text = "MANILOV";

            icon.ContextMenuStrip = new();

            Forms.ToolStripButton button = new();
          

            var settingsItem = new ToolStripMenuItem("Choose Position");


            settingsItem.DropDownItems.Add("leftBottom");
            settingsItem.DropDownItems.Add("Topleft");
            settingsItem.DropDownItems.Add("RightBottom");
            settingsItem.DropDownItems.Add("TopRight");

            settingsItem.DropDownItems[0].Click += (s, e) =>
            {
                stateManager.UpdateNotificationPosition(App.NotificationPosition.Leftbottom);
                SettingsService.Save(new Settings() { duration = 5, position = App.NotificationPosition.Leftbottom });
            };
            
            settingsItem.DropDownItems[1].Click += (s, e) => {
                SettingsService.Save(new Settings() { duration = 5, position = App.NotificationPosition.Topleft});
                stateManager.UpdateNotificationPosition(App.NotificationPosition.Topleft);
                };

            settingsItem.DropDownItems[2].Click += (s, e) => {
                SettingsService.Save(new Settings() { duration = 5, position = App.NotificationPosition.Rightbottom });
                stateManager.UpdateNotificationPosition(App.NotificationPosition.Rightbottom);
                }; 

            settingsItem.DropDownItems[3].Click += (s, e) => {
                SettingsService.Save(new Settings() { duration = 5, position = App.NotificationPosition.Topright });
                stateManager.UpdateNotificationPosition(App.NotificationPosition.Topright);
            };

            icon.ContextMenuStrip.Items.Add(settingsItem);


       
            button.Text = "Exit";
            button.Click += ExitApp;
            icon.ContextMenuStrip.Items.Add(button);
         
        }

        private void ExitApp(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        public void ChangeNotificationPosition(App.NotificationPosition pos)
        {
            
              
          
            
        }
    }
}
