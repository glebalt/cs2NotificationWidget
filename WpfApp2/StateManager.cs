using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using CounterStrike2GSI;
using CounterStrike2GSI.EventMessages;
using WpfApp2.Models;
namespace WpfApp2
{
    public class StateManager
    {
        private CounterStrike2GSI.Nodes.Phase prevState;
       
        private readonly GameStateListener gsl;

        public StateManager( GameStateListener gsl)
        {
            this.gsl = gsl;
        }
        public INotification CheckState(CounterStrike2GSI.Nodes.Phase current, CounterStrike2GSI.Nodes.Phase  prev)
        {
            INotification not;
            if (prevState == CounterStrike2GSI.Nodes.Phase.Over && current == CounterStrike2GSI.Nodes.Phase.Freezetime)
            {
               
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    not =  new FreezetimeStartedNotification();
                    NotificationService.UpdateNotification(not);

                });
            }
            if (prevState == CounterStrike2GSI.Nodes.Phase.Freezetime && current == CounterStrike2GSI.Nodes.Phase.Live)
            {
               
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    not =  new RoundStartedNotification();
                    NotificationService.UpdateNotification(not);
                });
            
            }



            prevState = current;
            return null;
        }


        public bool Init()
        {
            prevState = CounterStrike2GSI.Nodes.Phase.Live;
            gsl.NewGameState += TimerTest;
            bool result = gsl.GenerateGSIConfigFile("Example");

            if (result) { System.Diagnostics.Debug.Write("SUCCESS"); }
            gsl.Start();


            return result;
        }

        public void TimerTest(GameState state)
        {
            CounterStrike2GSI.Nodes.Phase current = state.Round.Phase;
           INotification notification = CheckState(current,prevState);
            System.Diagnostics.Debug.WriteLine(current);
      ;


        }
    }
}
