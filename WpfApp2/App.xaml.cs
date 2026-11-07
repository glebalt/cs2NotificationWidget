using CounterStrike2GSI;
using CounterStrike2GSI.EventMessages;
using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Threading;
using WpfApp2.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Forms = System.Windows.Forms;
using IWshRuntimeLibrary;
using System.Runtime.InteropServices;
using System.IO;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private const string name = @"\Cs2Widget.lnk";
        private readonly Forms.NotifyIcon icon;


        public App()
        {
            icon = new Forms.NotifyIcon();
        }

        private CounterStrike2GSI.Nodes.Phase prevState;
        protected override void OnStartup(StartupEventArgs e)
        {
          BuildTrayMenu();
            bool res = CreateSC();
            System.Diagnostics.Debug.WriteLine(res);
          
            GameStateListener gsl = new GameStateListener(3000);
            StateManager man = new(gsl);
            man.Init();
        

     

            MainWindow mainWindow = new MainWindow();
           


            base.OnStartup(e);
        }

        public void BuildTrayMenu()
        {
            icon.Icon = new System.Drawing.Icon("Resources/icon.ico");
            icon.Visible = true;
            icon.Text = "MANILOV";

            icon.ContextMenuStrip = new();
       
            Forms.ToolStripButton button = new();
            button.Text = "Exit";
            button.Click += ExitApp;
            icon.ContextMenuStrip.Items.Add(button);
        }

        private void ExitApp(object? sender, EventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            icon.Dispose();
        }

       
      

        public void TimerTest(GameState state)
        {
            CounterStrike2GSI.Nodes.Phase current = state.Round.Phase;

            if (current != prevState)
            {
               
    
                    prevState = current;
                System.Diagnostics.Debug.WriteLine(state.Round.Phase);
            }
          


        }

        public bool CreateSC()
        {

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string shortcutPath = Path.Combine(desktopPath, name);
   
            System.Diagnostics.Debug.WriteLine(desktopPath + name);
            if (System.IO.File.Exists(desktopPath + name)) {
                System.Windows.MessageBox.Show("БЛЯДОТА");
                return false;

            }

            WshShell shell = new WshShell();
          IWshShortcut shortcut = (IWshShortcut)  shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + name);
            shortcut.Save();

            return true;
        }
    }


}
