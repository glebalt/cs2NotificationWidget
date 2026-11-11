using CounterStrike2GSI;
using CounterStrike2GSI.EventMessages;
using IWshRuntimeLibrary;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using WpfApp2.Models;
using WpfApp2.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Forms = System.Windows.Forms;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private const string name = "Cs2Widget.lnk";
        private string iconPath = Path.Combine(Environment.CurrentDirectory,@"Resources\icon.ico") ;
        private readonly Forms.NotifyIcon icon;
        public enum NotificationPosition {Topleft, Topright, Rightbottom,Leftbottom }
        private string LogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "logs.txt");
        private bool consoleEnabled = false;


        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();
        public App() { 
        
          

            icon = new Forms.NotifyIcon();
        }

        private CounterStrike2GSI.Nodes.Phase prevState;
        protected override void OnStartup(StartupEventArgs e)
        {
            consoleEnabled = e.Args.Contains("-console");
            string resPath;
            bool res = CreateSC(out resPath);
           INotification not = new FreezetimeStartedNotification(NotificationPosition.Rightbottom);
            not.ShowWindow();
            System.Diagnostics.Debug.WriteLine(iconPath);
            CheckConsoleArguments(e);
            if (consoleEnabled)
            {
                AllocConsole();
            }
            Console.WriteLine(resPath);
        
            GameStateListener gsl = new GameStateListener(3000);
            StateManager man = new(gsl);

            CreateLog();
            TrayMenuBuildService traymenu = new(icon, man, iconPath);
            traymenu.BuildTrayMenu();
         

            man.Init();
            SettingsService.Start("Cs2Widget",man);
           

            MainWindow mainWindow = new MainWindow();
         
           

            base.OnStartup(e);

         

        }

        void CheckConsoleArguments(StartupEventArgs e)
        {
            if (e.Args.Contains("-console")){
                consoleEnabled = true;
            }
            else if (e.Args.Contains("-noconsole"))
            {
                consoleEnabled = false;
            }
        }
        

       

        protected override void OnExit(ExitEventArgs e)
        {
            icon.Dispose();
        }




       

        public void CreateLog()
        {

         

            var writer = TextWriter.Synchronized(new StreamWriter(LogPath, true) { AutoFlush = true});
    
            Console.SetError(writer);
            iconPath = Path.Combine(Path.GetDirectoryName(Environment.ProcessPath)!, @"Resources\icon.ico");
            Console.WriteLine(iconPath);
            Console.WriteLine("Application started");
            Console.Error.WriteLine("Error test");
      
   
        }

        public bool CreateSC(out string resultPath)
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutPath = Path.Combine(desktopPath, name);

            string exePath = Environment.ProcessPath!;
            resultPath = exePath;
            System.Diagnostics.Debug.WriteLine(exePath);

            WshShell shell = new WshShell();
            

            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.Arguments = "--console";
            shortcut.Arguments = "--noconsole";
            shortcut.TargetPath = exePath;
            shortcut.WorkingDirectory = exePath;
            shortcut.Save();

            if (System.IO.File.Exists(desktopPath + name))
            {
               
                return false;

            }

           
  

            return true;
        }
    }


}
