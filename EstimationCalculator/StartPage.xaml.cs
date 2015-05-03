using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace EstimationCalculator
{
    public partial class StartPage : PhoneApplicationPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        private void GoToTablePlate(object sender, RoutedEventArgs e)
        {
            //initalise global variables
            GlobalVariables.TotalNumberOfRequestedCalls = 0;

            GlobalVariables.RiskFactor = 0;

            GlobalVariables.BidCalls = 0;

            GlobalVariables.OrderOfRound = 1;

            GlobalVariables.GameOver = false;

            GlobalVariables.GameUnder = false;

            GlobalVariables.ALOPSC = 1;

            GlobalVariables.CheckAllLosers = false;

            GlobalVariables.LoosersCounter = 0;

            //initialise players data
            for (int i = 0 ; i < 4 ; i++)
            {
                Table.Player[i].name = null;
                Table.Player[i].bid = false;
                Table.Player[i].DashCall = false;
                Table.Player[i].DashBlind = false;
                Table.Player[i].DashBlind_Chances = true;
                Table.Player[i].NumberOfRequestedCalls = 0;
                Table.Player[i].NumberOfMadeCalls = 0;
                Table.Player[i].with = false;
                Table.Player[i].risk = false;
                Table.Player[i].win = false;
                Table.Player[i].only_winner = false;
                Table.Player[i].only_loser = false;
                Table.Player[i].GameScore = 0;
                Table.Player[i].TotalScore = 0;
            }
            
            NavigationService.Navigate(new Uri("/TablePlate.xaml", UriKind.Relative));
        }
    }
}