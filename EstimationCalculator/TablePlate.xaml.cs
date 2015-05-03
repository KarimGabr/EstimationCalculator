using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using LoopingSelector;
using System.Windows.Media;

namespace EstimationCalculator
{
    public static class MySelectors
    {
        //bidsuitsshapes
        public static char[] BidSuitsShapes = { '♠' , '♥' , '♦' , '♣' , '☼' };
        public static MySelector[] BidSuitChoosers = new MySelector[4];
        static MySelectors ()
        {
            for(int p = 0 ; p < 4 ; p++)
            {    
                BidSuitChoosers[p] = new MySelector("Suit",BidSuitsShapes);
            }
        }
    }
    
    public partial class TablePlate : PhoneApplicationPage
    {        
        //these variables will be used later on for handling the unchecking of radio buttons
        bool isChecked1 = false;
        bool isChecked2 = false;
        bool isChecked3 = false;
        bool isChecked4 = false;
        bool isChecked5 = false;
        bool isChecked6 = false;
        bool isChecked7 = false;
        bool isChecked8 = false;
        bool isChecked9 = false;
        bool isChecked10 = false;
        bool isChecked11 = false;
        bool isChecked12 = false;
        //
        
        //variable used later on to validate call entries
        int callo;
        
        //variables used later on to validate start of calcualations
        bool StartCalculate1 = true;
        bool StartCalculate2 = true;
        bool StartCalculate3 = true;
        bool StartCalculate4 = true;
        bool StartCalculate5 = true;
        bool StartCalculate6 = true;

        int TotalNumberOfMadeCalls = 0;
        //

        string FinalMessage = null;
        void Check_PlayerRisk()
        {

            if (Table.Player[0].bid == true)
            {
                if (Table.Player[3].DashCall == true)
                {
                    if (Table.Player[2].DashCall == true)
                    {
                        Table.Player[1].risk = true;
                        Table.Player[1].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                    else if (Table.Player[2].DashCall == false)
                    {
                        Table.Player[2].risk = true;
                        Table.Player[2].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                }

                else if (Table.Player[3].DashCall == false)
                {
                    Table.Player[3].risk = true;
                    Table.Player[3].Risks[GlobalVariables.OrderOfRound-1] = true;
                }
            }

            if (Table.Player[1].bid == true)
            {
                if (Table.Player[0].DashCall == true)
                {
                    if (Table.Player[3].DashCall == true)
                    {
                        Table.Player[2].risk = true;
                        Table.Player[2].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                    else if (Table.Player[3].DashCall == false)
                    {
                        Table.Player[3].risk = true;
                        Table.Player[3].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                }

                else if (Table.Player[0].DashCall == false)
                {
                    Table.Player[0].risk = true;
                    Table.Player[0].Risks[GlobalVariables.OrderOfRound-1] = true;
                }
            }

            if (Table.Player[2].bid == true)
            {
                if (Table.Player[1].DashCall == true)
                {
                    if (Table.Player[0].DashCall == true)
                    {
                        Table.Player[3].risk = true;
                        Table.Player[3].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                    else if (Table.Player[0].DashCall == false)
                    {
                        Table.Player[0].risk = true;
                        Table.Player[0].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                }

                else if (Table.Player[1].DashCall == false)
                {
                    Table.Player[1].risk = true;
                    Table.Player[1].Risks[GlobalVariables.OrderOfRound-1] = true;
                }
            }

            if (Table.Player[3].bid == true)
            {
                if (Table.Player[2].DashCall == true)
                {
                    if (Table.Player[1].DashCall == true)
                    {
                        Table.Player[0].risk = true;
                        Table.Player[0].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                    else if (Table.Player[1].DashCall == false)
                    {
                        Table.Player[1].risk = true;
                        Table.Player[1].Risks[GlobalVariables.OrderOfRound-1] = true;
                    }
                }

                else if (Table.Player[2].DashCall == false)
                {
                    Table.Player[2].risk = true;
                    Table.Player[2].Risks[GlobalVariables.OrderOfRound-1] = true;
                }
            }

        }

        void Retrieve_RiskFactor()
        {
            GlobalVariables.RiskFactor = GlobalVariables.TotalNumberOfRequestedCalls - 13;

            GlobalVariables.RiskFactor = Math.Abs(GlobalVariables.RiskFactor);

            if (GlobalVariables.RiskFactor % 2 == 0)
            {
                GlobalVariables.RiskFactor = GlobalVariables.RiskFactor / 2;
                GlobalVariables.RiskFactors[GlobalVariables.OrderOfRound-1] = GlobalVariables.RiskFactor;
            }

            else
            {
                GlobalVariables.RiskFactor = 0;
                GlobalVariables.RiskFactors[GlobalVariables.OrderOfRound-1] = 0;
            }
        }

        void Check_With()
        {

            for (int i = 0; i < 4; i++)
            {
                if (Table.Player[i].bid == false && Table.Player[i].NumberOfRequestedCalls == GlobalVariables.BidCalls)
                {
                    Table.Player[i].with = true;
                    Table.Player[i].Withs[GlobalVariables.OrderOfRound-1] = true;
                }
            }

        }

        void Check_OverOrUnder()
        {
            if (GlobalVariables.TotalNumberOfRequestedCalls > 13)
            {
                GlobalVariables.GameOver = true;
                GlobalVariables.GameUnder = false;
            }

            else if (GlobalVariables.TotalNumberOfRequestedCalls < 13)
            {
                GlobalVariables.GameUnder = true;
                GlobalVariables.GameOver = false;
            }

        }

        bool Check_AllLosers()
        {

            if (GlobalVariables.LoosersCounter == 4)    return true;
            else return false;

        }

        bool Check_3PlayersWithSameCall()
        {

            for (int i = 0 ; i < 4 ; i++)
            {
                for (int j = i + 1 ; j < 4 ; j++)
                {
                    for (int k = j + 1 ; k < 4 ; k++)
                    {
                        if (Table.Player[i].NumberOfRequestedCalls == Table.Player[j].NumberOfRequestedCalls && Table.Player[j].NumberOfRequestedCalls == Table.Player[k].NumberOfRequestedCalls)   return true;
                    }
                }
            }

            return false;

        }

        void Check_NOT_AllLosers_Or_3PlayersWithSameCall()
        {

            if (Check_AllLosers() == false && Check_3PlayersWithSameCall() == false)
                GlobalVariables.ALOPSC = 1;
            return;

        }

        void Check_WinorLose()
        {

            for (int i = 0; i < 4; i++)
            {
                if (Table.Player[i].NumberOfRequestedCalls == Table.Player[i].NumberOfMadeCalls) Table.Player[i].win = true;
                else
                {
                    Table.Player[i].win = false;
                    GlobalVariables.LoosersCounter++;
                }
                    
            }
        }

        void Check_OnlyWinner()
        {

            if (Table.Player[0].win == true && Table.Player[1].win == false && Table.Player[2].win == false && Table.Player[3].win == false)
            {
                Table.Player[0].only_winner = true;
                Table.Player[0].OnlyWinners[GlobalVariables.OrderOfRound-1] = true;
            }
            if (Table.Player[0].win == false && Table.Player[1].win == true && Table.Player[2].win == false && Table.Player[3].win == false)
            {
                Table.Player[1].only_winner = true;
                Table.Player[1].OnlyWinners[GlobalVariables.OrderOfRound-1] = true;
            }
            if (Table.Player[0].win == false && Table.Player[1].win == false && Table.Player[2].win == true && Table.Player[3].win == false)
            {
                Table.Player[2].only_winner = true;
                Table.Player[2].OnlyWinners[GlobalVariables.OrderOfRound-1] = true;
            }
            if (Table.Player[0].win == false && Table.Player[1].win == false && Table.Player[2].win == false && Table.Player[3].win == true)
            {
                Table.Player[3].only_winner = true;
                Table.Player[3].OnlyWinners[GlobalVariables.OrderOfRound-1] = true;
            }
        }

        void Check_OnlyLoser()
        {

            if (Table.Player[0].win == false && Table.Player[1].win == true && Table.Player[2].win == true && Table.Player[3].win == true)
            {
                Table.Player[0].only_loser = true;
                Table.Player[0].OnlyLosers[GlobalVariables.OrderOfRound-1] = true;
            }
            if (Table.Player[0].win == true && Table.Player[1].win == false && Table.Player[2].win == true && Table.Player[3].win == true)
            {
                Table.Player[1].only_loser = true;
                Table.Player[1].OnlyLosers[GlobalVariables.OrderOfRound-1] = true;
            }
            if (Table.Player[0].win == true && Table.Player[1].win == true && Table.Player[2].win == false && Table.Player[3].win == true)
            {
                Table.Player[2].only_loser = true;
                Table.Player[2].OnlyLosers[GlobalVariables.OrderOfRound-1] = true;
            }
            if (Table.Player[0].win == true && Table.Player[1].win == true && Table.Player[2].win == true && Table.Player[3].win == false)
            {
                Table.Player[3].only_loser = true;
                Table.Player[3].OnlyLosers[GlobalVariables.OrderOfRound-1] = true;
            }
        }



        void CalculateResult()
        {

            //alopsc factor for 3 players with same call
            if (Check_3PlayersWithSameCall() == true)
            {
                GlobalVariables.ALOPSC *= 2;
            }
            //omar katab esmo fel app da nihahahahha 3ala allah tms7o ya ****

            int i;
   
            for (i = 0; i < 4; i++)
            {

                // win or lose
                if (Table.Player[i].win == true)
                {

                        if (Table.Player[i].DashBlind == true) Table.Player[i].GameScore += 200;
                        
                        if (Table.Player[i].DashCall == true)
                        {
                            if (GlobalVariables.GameUnder == true && GlobalVariables.GameOver == false) Table.Player[i].GameScore += 30;
                            else if (GlobalVariables.GameOver == true && GlobalVariables.GameUnder == false) Table.Player[i].GameScore += 20;
                        }

                        //1- add bid
                        if (Table.Player[i].bid == true) Table.Player[i].GameScore += 10;
                        //2- add calls
                        if(Table.Player[i].DashCall == false && Table.Player[i].DashBlind == false)
                        {
                            if (Table.Player[i].NumberOfMadeCalls > 7) Table.Player[i].GameScore += (Table.Player[i].NumberOfMadeCalls * Table.Player[i].NumberOfMadeCalls);
                            else if (Table.Player[i].NumberOfMadeCalls < 8) Table.Player[i].GameScore += (10 + Table.Player[i].NumberOfMadeCalls);
                        }
                        //3- add only winner
                        if (Table.Player[i].only_winner == true) Table.Player[i].GameScore += 10;
                        //4- add with
                        if (Table.Player[i].with == true) Table.Player[i].GameScore += 10;
                        //5- add risk
                        if (Table.Player[i].risk == true) Table.Player[i].GameScore += (10 * GlobalVariables.RiskFactor);
                        //6- add alopsc factor
                        Table.Player[i].GameScore *= GlobalVariables.ALOPSC;
                        //7- add 13th round
                        if (GlobalVariables.OrderOfRound == 8 || GlobalVariables.OrderOfRound == 13) Table.Player[i].GameScore *= 2;
                        //8- add gamescore to totalscore
                        Table.Player[i].TotalScore = Table.Player[i].TotalScore + Table.Player[i].GameScore;
                        //9- add gamescore to allscores sheet
                        Table.Player[i].AllScores[GlobalVariables.OrderOfRound-1] = Table.Player[i].TotalScore;
                }

                else if (Table.Player[i].win == false)
                {
  
               
                    if (Table.Player[i].DashBlind == true) Table.Player[i].GameScore -= 100;
                    
                    if (Table.Player[i].DashCall == true)
                    {
                        if (GlobalVariables.GameUnder == true) Table.Player[i].GameScore -= 20;
                        if (GlobalVariables.GameOver == true) Table.Player[i].GameScore -= 30;
                    }

                    //1- add bid
                    if (Table.Player[i].bid == true) Table.Player[i].GameScore -= 10;
                    //2- add calls
                    if (Table.Player[i].DashCall == false && Table.Player[i].DashBlind == false)
                    {
                        if (Table.Player[i].NumberOfMadeCalls > 7) Table.Player[i].GameScore -= (Table.Player[i].NumberOfMadeCalls * Table.Player[i].NumberOfMadeCalls / 2);
                        else if (Table.Player[i].NumberOfMadeCalls < 8) Table.Player[i].GameScore -= (Math.Abs(Table.Player[i].NumberOfRequestedCalls - Table.Player[i].NumberOfMadeCalls));
                    }
                    //3- add only loser
                    if (Table.Player[i].only_loser == true) Table.Player[i].GameScore -= 10;
                    //4- add with
                    if (Table.Player[i].with == true) Table.Player[i].GameScore -= 10;
                    //5- add risk
                    if (Table.Player[i].risk == true) Table.Player[i].GameScore -= (10 * GlobalVariables.RiskFactor);
                    //6- add alopsc factor
                    Table.Player[i].GameScore *= GlobalVariables.ALOPSC;
                    //7- add 13th round
                    if (GlobalVariables.OrderOfRound == 8 || GlobalVariables.OrderOfRound == 13) Table.Player[i].GameScore *= 2;
                    //8- add gamescore to totalscore
                    Table.Player[i].TotalScore = Table.Player[i].TotalScore + Table.Player[i].GameScore;
                    //9- add gamescore to allscores sheet
                    Table.Player[i].AllScores[GlobalVariables.OrderOfRound-1] = Table.Player[i].TotalScore;
                    }
                }

            }
        
        public TablePlate()
        {
            InitializeComponent();

            Player1Name.MaxLength = 10;
            Player2Name.MaxLength = 10;
            Player3Name.MaxLength = 10;
            Player4Name.MaxLength = 10;

            //Add BidSuit Selectors to a grid
            for (int q = 0 ; q < 4 ; q++)
            {
                MySelectors.BidSuitChoosers[q].Height = 65;
                MySelectors.BidSuitChoosers[q].Width = 90;
                MySelectors.BidSuitChoosers[q].HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                MySelectors.BidSuitChoosers[q].VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                MySelectors.BidSuitChoosers[q].Background = new SolidColorBrush(Colors.White);
                MySelectors.BidSuitChoosers[q].Foreground = new SolidColorBrush(Colors.Black);
                MySelectors.BidSuitChoosers[q].BorderBrush = new SolidColorBrush(Colors.Black);
                MySelectors.BidSuitChoosers[q].FontSize = 20;
                MySelectors.BidSuitChoosers[q].FontWeight = FontWeights.Bold;
                MySelectors.BidSuitChoosers[q].Visibility = Visibility.Collapsed;
            }
            Grid.SetRow(MySelectors.BidSuitChoosers[0], 0);
            Grid.SetColumn(MySelectors.BidSuitChoosers[0], 0);
            Grid.SetRow(MySelectors.BidSuitChoosers[1], 2);
            Grid.SetColumn(MySelectors.BidSuitChoosers[1], 0);
            Grid.SetRow(MySelectors.BidSuitChoosers[2], 2);
            Grid.SetColumn(MySelectors.BidSuitChoosers[2], 2);
            Grid.SetRow(MySelectors.BidSuitChoosers[3], 0);
            Grid.SetColumn(MySelectors.BidSuitChoosers[3], 2);
            for (int q = 0 ; q < 4 ; q++)
            {
                GFMS.Children.Add(MySelectors.BidSuitChoosers[q]);
                MySelectors.BidSuitChoosers[q].ParentSelector(GFMS);
            }
            ////
            
            if (GlobalVariables.OrderOfRound != 1)
            {
                Player1Score.Text = Convert.ToString(Table.Player[0].TotalScore);
                Player2Score.Text = Convert.ToString(Table.Player[1].TotalScore);
                Player3Score.Text = Convert.ToString(Table.Player[2].TotalScore);
                Player4Score.Text = Convert.ToString(Table.Player[3].TotalScore);
            }

            if (Table.Player[0].name != null)
            {
                Player1Name.Text = Table.Player[0].name;
                //Player1Name.IsReadOnly = true;
            }

            if (Table.Player[1].name != null)
            {
                Player2Name.Text = Table.Player[1].name;
                //Player2Name.IsReadOnly = true;
            }

            if (Table.Player[2].name != null)
            {
                Player3Name.Text = Table.Player[2].name;
                //Player3Name.IsReadOnly = true;
            }

            if (Table.Player[3].name != null)
            {
                Player4Name.Text = Table.Player[3].name;
                //Player4Name.IsReadOnly = true;
            }
            
        }

        //backkey press
        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/StartPage.xaml", UriKind.Relative));
        }
        //
        
        //enter player names
        private void Player1Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Table.Player[0].name = Player1Name.Text;       
        }

        private void Player2Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Table.Player[1].name = Player2Name.Text;
        }

        private void Player3Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Table.Player[2].name = Player3Name.Text;
        }

        private void Player4Name_TextChanged(object sender, TextChangedEventArgs e)
        {
            Table.Player[3].name = Player4Name.Text;
        }
        //


        private void TextBlock_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {     
            //Player1Name.IsReadOnly = true;
            //Player2Name.IsReadOnly = true;
            //Player3Name.IsReadOnly = true;
            //Player4Name.IsReadOnly = true;

            GlobalVariables.CanRedo = false;

            //bids
            if (Player1Bid.IsChecked == true)
            {
                Table.Player[0].bid = true;
                Table.Player[0].bids[GlobalVariables.OrderOfRound] = true;
                try
                {
                    Table.Player[0].BidSuits[GlobalVariables.OrderOfRound - 1] = Convert.ToChar(MySelectors.BidSuitChoosers[0].Content);
                    StartCalculate5 = true;
                }
                catch(FormatException ex)
                {
                    MessageBoxResult result50 = MessageBox.Show("Please Choose BidSuit ", "", MessageBoxButton.OK);
                    StartCalculate5 = false;
                }
            }
            if (Player1Bid.IsChecked == false)
            {
                Table.Player[0].bid = false;
                Table.Player[0].BidSuits[GlobalVariables.OrderOfRound-1] = 'O';
            }

            if (Player2Bid.IsChecked == true)
            {
                Table.Player[1].bid = true;
                Table.Player[1].bids[GlobalVariables.OrderOfRound] = true;
                try
                {
                    Table.Player[1].BidSuits[GlobalVariables.OrderOfRound - 1] = Convert.ToChar(MySelectors.BidSuitChoosers[1].Content);
                    StartCalculate5 = true;
                }
                catch (FormatException ex)
                {
                    MessageBoxResult result51 = MessageBox.Show("Please Choose BidSuit ", "", MessageBoxButton.OK);
                    StartCalculate5 = false;
                }
            }
            if (Player2Bid.IsChecked == false)
            {
                Table.Player[1].bid = false;
                Table.Player[1].BidSuits[GlobalVariables.OrderOfRound-1] = 'O';
            }

            if (Player3Bid.IsChecked == true)
            {
                Table.Player[2].bid = true;
                Table.Player[2].bids[GlobalVariables.OrderOfRound] = true;
                try
                {
                    Table.Player[2].BidSuits[GlobalVariables.OrderOfRound - 1] = Convert.ToChar(MySelectors.BidSuitChoosers[2].Content);
                    StartCalculate5 = true;
                }
                catch (FormatException ex)
                {
                    MessageBoxResult result52 = MessageBox.Show("Please Choose BidSuit ", "", MessageBoxButton.OK);
                    StartCalculate5 = false;
                }
            }
            if (Player3Bid.IsChecked == false)
            {
                Table.Player[2].bid = false;
                Table.Player[2].BidSuits[GlobalVariables.OrderOfRound-1] = 'O';
            }

            if (Player4Bid.IsChecked == true)
            {
                Table.Player[3].bid = true;
                Table.Player[3].bids[GlobalVariables.OrderOfRound] = true;
                try
                {
                    Table.Player[3].BidSuits[GlobalVariables.OrderOfRound - 1] = Convert.ToChar(MySelectors.BidSuitChoosers[3].Content);
                    StartCalculate5 = true;
                }
                catch (FormatException ex)
                {
                    MessageBoxResult result53 = MessageBox.Show("Please Choose BidSuit ", "", MessageBoxButton.OK);
                    StartCalculate5 = false;
                }
            }
            if (Player4Bid.IsChecked == false)
            {
                Table.Player[3].bid = false;
                Table.Player[3].BidSuits[GlobalVariables.OrderOfRound-1] = 'O';
            }
            //

            
            
                //dashcalls
                if (Player1DashCall.IsChecked == true)
                {
                    Table.Player[0].DashCall = true;
                    Table.Player[0].NumberOfRequestedCalls = 0;
                    Table.Player[0].DashCalls[GlobalVariables.OrderOfRound-1] = true;
                }
                if (Player1DashCall.IsChecked == false)
                {
                    Table.Player[0].DashCall = false;
                    Table.Player[0].DashCalls[GlobalVariables.OrderOfRound-1] = false;
                }

                if (Player2DashCall.IsChecked == true)
                {
                    Table.Player[1].DashCall = true;
                    Table.Player[1].NumberOfRequestedCalls = 0;
                    Table.Player[1].DashCalls[GlobalVariables.OrderOfRound-1] = true;
                }
                if (Player2DashCall.IsChecked == false)
                {
                    Table.Player[1].DashCall = false;
                    Table.Player[1].DashCalls[GlobalVariables.OrderOfRound-1] = false;
                }

                if (Player3DashCall.IsChecked == true)
                {
                    Table.Player[2].DashCall = true;
                    Table.Player[2].NumberOfRequestedCalls = 0;
                    Table.Player[2].DashCalls[GlobalVariables.OrderOfRound-1] = true;
                }
                if (Player3DashCall.IsChecked == false)
                {
                    Table.Player[2].DashCall = false;
                    Table.Player[2].DashCalls[GlobalVariables.OrderOfRound-1] = false;
                }

                if (Player4DashCall.IsChecked == true)
                {
                    Table.Player[3].DashCall = true;
                    Table.Player[3].NumberOfRequestedCalls = 0;
                    Table.Player[3].DashCalls[GlobalVariables.OrderOfRound-1] = true;
                }
                if (Player4DashCall.IsChecked == false)
                {
                    Table.Player[3].DashCall = false;
                    Table.Player[3].DashCalls[GlobalVariables.OrderOfRound-1] = false;
                }
                //

                //visibility of dash blinds
                if (Table.Player[0].DashBlind_Chances == true) Player1DashBlind.Visibility = Visibility.Visible;
                else if (Table.Player[0].DashBlind_Chances == false) Player1DashBlind.Visibility = Visibility.Collapsed;

                if (Table.Player[1].DashBlind_Chances == true) Player2DashBlind.Visibility = Visibility.Visible;
                else if (Table.Player[1].DashBlind_Chances == false) Player2DashBlind.Visibility = Visibility.Collapsed;

                if (Table.Player[2].DashBlind_Chances == true) Player3DashBlind.Visibility = Visibility.Visible;
                else if (Table.Player[2].DashBlind_Chances == false) Player3DashBlind.Visibility = Visibility.Collapsed;

                if (Table.Player[3].DashBlind_Chances == true) Player4DashBlind.Visibility = Visibility.Visible;
                else if (Table.Player[3].DashBlind_Chances == false) Player4DashBlind.Visibility = Visibility.Collapsed;
                //

                //dashblinds

                if (Player1DashBlind.IsChecked == true)
                {
                    Table.Player[0].DashBlind = true;
                    Table.Player[0].NumberOfRequestedCalls = 0;
                    Table.Player[0].DashBlinds[GlobalVariables.OrderOfRound-1] = true;
                }
                else if (Player1DashBlind.IsChecked == false)
                {
                    Table.Player[0].DashBlind = false;
                    Table.Player[0].DashBlinds[GlobalVariables.OrderOfRound-1] = false;
                }


                if (Player2DashBlind.IsChecked == true)
                {
                    Table.Player[1].DashBlind = true;
                    Table.Player[1].NumberOfRequestedCalls = 0;
                    Table.Player[1].DashBlinds[GlobalVariables.OrderOfRound-1] = true;
                }
                else if (Player2DashBlind.IsChecked == false)
                {
                    Table.Player[1].DashBlind = false;
                    Table.Player[1].DashBlinds[GlobalVariables.OrderOfRound-1] = false;
                }


                if (Player3DashBlind.IsChecked == true)
                {
                    Table.Player[2].DashBlind = true;
                    Table.Player[2].NumberOfRequestedCalls = 0;
                    Table.Player[2].DashBlinds[GlobalVariables.OrderOfRound-1] = true;
                }
                else if (Player3DashBlind.IsChecked == false)
                {
                    Table.Player[2].DashBlind = false;
                    Table.Player[2].DashBlinds[GlobalVariables.OrderOfRound-1] = false;
                }


                if (Player4DashBlind.IsChecked == true)
                {
                    Table.Player[3].DashBlind = true;
                    Table.Player[3].NumberOfRequestedCalls = 0;
                    Table.Player[3].DashBlinds[GlobalVariables.OrderOfRound-1] = true;
                }
                else if (Player4DashBlind.IsChecked == false)
                {
                    Table.Player[3].DashBlind = false;
                    Table.Player[3].DashBlinds[GlobalVariables.OrderOfRound-1] = false;
                }
                //

                //blind dashes chances
                if (Player1DashBlind.IsChecked == true)
                {
                    Table.Player[0].DashBlind_Chances = false;
                    GlobalVariables.BDCMadeAt = GlobalVariables.OrderOfRound;
                }

                if (Player2DashBlind.IsChecked == true)
                {
                    Table.Player[1].DashBlind_Chances = false;
                    GlobalVariables.BDCMadeAt = GlobalVariables.OrderOfRound;
                }

                if (Player3DashBlind.IsChecked == true)
                {
                    Table.Player[2].DashBlind_Chances = false;
                    GlobalVariables.BDCMadeAt = GlobalVariables.OrderOfRound;
                }

                if (Player4DashBlind.IsChecked == true)
                {
                    Table.Player[3].DashBlind_Chances = false;
                    GlobalVariables.BDCMadeAt = GlobalVariables.OrderOfRound;
                }
                //

                //enter requested calls

                try
                {
                    Table.Player[0].NumberOfRequestedCalls = Convert.ToInt32(Player1RequestedCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player1RequestedCalls.Text = "0";
                }
                //determine the bid call
                if (Table.Player[0].bid == true) GlobalVariables.BidCalls = Table.Player[0].NumberOfRequestedCalls;

                try
                {
                    Table.Player[1].NumberOfRequestedCalls = Convert.ToInt32(Player2RequestedCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player2RequestedCalls.Text = "0";
                }
                //determine the bid call
                if (Table.Player[1].bid == true) GlobalVariables.BidCalls = Table.Player[1].NumberOfRequestedCalls;

                try
                {
                    Table.Player[2].NumberOfRequestedCalls = Convert.ToInt32(Player3RequestedCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player3RequestedCalls.Text = "0";
                }
                //determine the bid call
                if (Table.Player[2].bid == true) GlobalVariables.BidCalls = Table.Player[2].NumberOfRequestedCalls;

                try
                {
                    Table.Player[3].NumberOfRequestedCalls = Convert.ToInt32(Player4RequestedCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player4RequestedCalls.Text = "0";
                }
                //determine the bid call
                if (Table.Player[3].bid == true) GlobalVariables.BidCalls = Table.Player[3].NumberOfRequestedCalls;

                //

                //enter made calls

                try
                {
                    Table.Player[0].NumberOfMadeCalls = Convert.ToInt32(Player1MadeCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player1MadeCalls.Text = "0";
                }

                try
                {
                    Table.Player[1].NumberOfMadeCalls = Convert.ToInt32(Player2MadeCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player2MadeCalls.Text = "0";
                }

                try
                {
                    Table.Player[2].NumberOfMadeCalls = Convert.ToInt32(Player3MadeCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player3MadeCalls.Text = "0";
                }

                try
                {
                    Table.Player[3].NumberOfMadeCalls = Convert.ToInt32(Player4MadeCalls.Text);
                }
                catch (FormatException ex)
                {
                    Player4MadeCalls.Text = "0";
                }


                //determine total number of requested calls and made calls
                GlobalVariables.TotalNumberOfRequestedCalls = Table.Player[0].NumberOfRequestedCalls + Table.Player[1].NumberOfRequestedCalls + Table.Player[2].NumberOfRequestedCalls + Table.Player[3].NumberOfRequestedCalls;
                TotalNumberOfMadeCalls = Table.Player[0].NumberOfMadeCalls + Table.Player[1].NumberOfMadeCalls + Table.Player[2].NumberOfMadeCalls + Table.Player[3].NumberOfMadeCalls;

                //check if a bid exist and total number of requested calls != 13 and total number of made calls = 13 and number of player's requested calls <= 13  and bidder calls >=4
                if (Table.Player[0].bid == false && Table.Player[1].bid == false && Table.Player[2].bid == false && Table.Player[3].bid == false)
                {
                    StartCalculate1 = false;
                    MessageBoxResult result20 = MessageBox.Show("Round cannot start without Bid", "", MessageBoxButton.OK);
                }
                else
                {
                    StartCalculate1 = true;
                    if (GlobalVariables.TotalNumberOfRequestedCalls == 13)
                    {
                        StartCalculate2 = false;
                        MessageBoxResult result21 = MessageBox.Show("Total number of requested calls must not be 13", "", MessageBoxButton.OK);
                    }
                    else
                    {
                        StartCalculate2 = true;
                        if (TotalNumberOfMadeCalls != 13)
                        {
                            StartCalculate3 = false;
                            MessageBoxResult result22 = MessageBox.Show("Total number of made calls must be 13", "", MessageBoxButton.OK);
                        }
                        else
                        {
                            StartCalculate3 = true;
                            if(GlobalVariables.BidCalls < 4)
                            {
                                StartCalculate4 = false;
                                MessageBoxResult result23 = MessageBox.Show("Bidder Calls must be at least 4", "", MessageBoxButton.OK);
                            }
                            else
                            {
                                StartCalculate4 = true;
                                for(int v = 0 ; v < 4 ; v++)
                                {
                                    if (Table.Player[v].NumberOfRequestedCalls > 13)
                                    {
                                        StartCalculate6 = false;
                                        MessageBoxResult result77 = MessageBox.Show("Player cannot request more than 13 calls", "", MessageBoxButton.OK);
                                        break;
                                    }
                                    StartCalculate6 = true;
                                }
                            }
                        }
                    }
                }
                //

                if (StartCalculate1 == true && StartCalculate2 == true && StartCalculate3 == true && StartCalculate4 == true && StartCalculate5 == true && StartCalculate6 == true)
                {
                    //determine with call
                    Check_With();

                    //determine risks
                    Check_PlayerRisk();

                    //retrieve risk factor
                    Retrieve_RiskFactor();

                    //determine game over or under
                    Check_OverOrUnder();

                    //determine wins and losses
                    Check_WinorLose();
                    Check_OnlyWinner();
                    Check_OnlyLoser();

                    //check all losers
                    if (Check_AllLosers() == true)
                    {
                        GlobalVariables.ALOPSC *= 2;
                        
                        if (GlobalVariables.OrderOfRound == 1)
                        {
                            for (int t = 0; t < 4; t++)
                            {
                                Table.Player[t].AllScores[GlobalVariables.OrderOfRound - 1] = 0;
                            }
                        }

                        else
                        {
                            for (int t = 0; t < 4; t++)
                            {
                                Table.Player[t].AllScores[GlobalVariables.OrderOfRound - 1] = Table.Player[t].AllScores[GlobalVariables.OrderOfRound - 2];
                            }
                        }
                    }

                    //calculate result
                    else if (Check_AllLosers() == false)
                    {
                        CalculateResult();
                    }

                    

                    Check_NOT_AllLosers_Or_3PlayersWithSameCall();
                    GlobalVariables.ALOPSCs[GlobalVariables.OrderOfRound - 1] = GlobalVariables.ALOPSC;
                    //displaying a round stats message
                    
                    FinalMessage = "Round " + Convert.ToString(GlobalVariables.OrderOfRound);
                    
                    if (GlobalVariables.GameOver == true && GlobalVariables.GameUnder == false) FinalMessage += "\nGame is Over " + Convert.ToString(Math.Abs(13 - GlobalVariables.TotalNumberOfRequestedCalls));
                    
                    if (GlobalVariables.GameUnder == true && GlobalVariables.GameOver == false) FinalMessage += "\nGame is Under " + Convert.ToString(Math.Abs(13 - GlobalVariables.TotalNumberOfRequestedCalls));
                    
                    if (Check_3PlayersWithSameCall() == true) FinalMessage += "\n3 Players With Same Call";
                    
                    if (Check_AllLosers() == true) FinalMessage += "\nAll Losers";
                    
                    for (int i = 0; i < 4; i++)
                    {

                        if (Table.Player[i].bid == true) FinalMessage += "\n" + Table.Player[i].name + " has Bid";

                        if (Table.Player[i].DashCall == true) FinalMessage += "\n" + Table.Player[i].name + " requested a Dash Call";

                        if (Table.Player[i].with == true) FinalMessage += "\n" + Table.Player[i].name + " requested a With Call";

                        if (Table.Player[i].DashBlind == true) FinalMessage += "\n" + Table.Player[i].name + " requested a Blind Dash Call";

                        if (Table.Player[i].risk == true && GlobalVariables.RiskFactor != 0) FinalMessage += "\n" + Table.Player[i].name + " had a " + Convert.ToString(GlobalVariables.RiskFactor) + "x Risk";

                        if (Table.Player[i].only_loser == true) FinalMessage += "\n" + Table.Player[i].name + " is Only Loser";

                        if (Table.Player[i].only_winner == true) FinalMessage += "\n" + Table.Player[i].name + " is Only Winner";

                    }

                    MessageBoxResult FinalResult = MessageBox.Show(FinalMessage,"",MessageBoxButton.OK);

                    Player1Score.Text = Convert.ToString(Table.Player[0].TotalScore);
                    Player2Score.Text = Convert.ToString(Table.Player[1].TotalScore);
                    Player3Score.Text = Convert.ToString(Table.Player[2].TotalScore);
                    Player4Score.Text = Convert.ToString(Table.Player[3].TotalScore);

                    for (int i = 0; i < 4; i++)
                    {
                        Table.Player[i].bid = false;
                        Table.Player[i].DashCall = false;
                        Table.Player[i].DashBlind = false;
                        Table.Player[i].NumberOfRequestedCalls = 0;
                        Table.Player[i].NumberOfMadeCalls = 0;
                        Table.Player[i].with = false;
                        Table.Player[i].risk = false;
                        Table.Player[i].win = false;
                        Table.Player[i].only_winner = false;
                        Table.Player[i].only_loser = false;
                        Table.Player[i].GameScore = 0;
                    }

                    GlobalVariables.LoosersCounter = 0;
                    GlobalVariables.TotalNumberOfRequestedCalls = 0;
                    GlobalVariables.RiskFactor = 0;
                    GlobalVariables.BidCalls = 0;
                    GlobalVariables.GameOver = false;
                    GlobalVariables.GameUnder = false;

                    Player1Bid.IsChecked = false;
                    Player1DashCall.IsChecked = false;
                    Player1RequestedCalls.Text = "0";
                    Player1MadeCalls.Text = "0";

                    Player2Bid.IsChecked = false;
                    Player2DashCall.IsChecked = false;
                    Player2RequestedCalls.Text = "0";
                    Player2MadeCalls.Text = "0";

                    Player3Bid.IsChecked = false;
                    Player3DashCall.IsChecked = false;
                    Player3RequestedCalls.Text = "0";
                    Player3MadeCalls.Text = "0";

                    Player4Bid.IsChecked = false;
                    Player4DashCall.IsChecked = false;
                    Player4RequestedCalls.Text = "0";
                    Player4MadeCalls.Text = "0";

                    GlobalVariables.OrderOfRound++;

                    //visibilty of dash blinds

                    if (Player1DashBlind.IsChecked == true) Player1DashBlind.Visibility = Visibility.Collapsed;

                    if (Player2DashBlind.IsChecked == true) Player2DashBlind.Visibility = Visibility.Collapsed;

                    if (Player3DashBlind.IsChecked == true) Player3DashBlind.Visibility = Visibility.Collapsed;

                    if (Player4DashBlind.IsChecked == true) Player4DashBlind.Visibility = Visibility.Collapsed;

                    MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
                    MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
                    MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
                    MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
                }
            }


        
        //handling radio buttons checking/unchecking 

        private void Player1Bid_Checked(object sender, RoutedEventArgs e)
        {
            isChecked1 = (bool) Player1Bid.IsChecked;
        }

        private void Player1Bid_Click(object sender, RoutedEventArgs e)
        {
            if (Player1Bid.IsChecked == true && !isChecked1)
            {
                Player1Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
            }
            else
            {
                Player1Bid.IsChecked = true;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Visible;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
                Player1DashCall.IsChecked = false;
                Player1DashBlind.IsChecked = false;
                isChecked1 = false;
            }
        }

        private void Player1DashCall_Checked(object sender, RoutedEventArgs e)
        {
            isChecked2 = (bool)Player1DashCall.IsChecked;
        }

        private void Player1DashCall_Click(object sender, RoutedEventArgs e)
        {
            if (Player1DashCall.IsChecked == true && !isChecked2)
                Player1DashCall.IsChecked = false;
            else
            {
                Player1DashCall.IsChecked = true;
                Player1Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
                isChecked2 = false;
            }
        }

        private void Player1DashBlind_Checked(object sender, RoutedEventArgs e)
        {
            isChecked3 = (bool)Player1DashBlind.IsChecked;
        }

        private void Player1DashBlind_Click(object sender, RoutedEventArgs e)
        {
            if (Player1DashBlind.IsChecked == true && !isChecked3)
                Player1DashBlind.IsChecked = false;
            else
            {
                Player1DashBlind.IsChecked = true;
                Player1Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
                isChecked3 = false;
            }
        }

        private void Player2Bid_Checked(object sender, RoutedEventArgs e)
        {
            isChecked4 = (bool)Player2Bid.IsChecked;
        }

        private void Player2Bid_Click(object sender, RoutedEventArgs e)
        {
            if (Player2Bid.IsChecked == true && !isChecked4)
            {
                Player2Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
            }
            else
            {
                Player2Bid.IsChecked = true;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Visible;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
                Player2DashCall.IsChecked = false;
                Player2DashBlind.IsChecked = false;
                isChecked4 = false;
            }
        }

        private void Player2DashCall_Checked(object sender, RoutedEventArgs e)
        {
            isChecked5 = (bool)Player2DashCall.IsChecked;
        }

        private void Player2DashCall_Click(object sender, RoutedEventArgs e)
        {
            if (Player2DashCall.IsChecked == true && !isChecked5)
                Player2DashCall.IsChecked = false;
            else
            {
                Player2DashCall.IsChecked = true;
                Player2Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
                isChecked5 = false;
            }
        }

        private void Player2DashBlind_Checked(object sender, RoutedEventArgs e)
        {
            isChecked6 = (bool)Player2DashBlind.IsChecked;
        }

        private void Player2DashBlind_Click(object sender, RoutedEventArgs e)
        {
            if (Player2DashBlind.IsChecked == true && !isChecked6)
                Player2DashBlind.IsChecked = false;
            else
            {
                Player2DashBlind.IsChecked = true;
                Player2Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
                isChecked6 = false;
            }
        }

        private void Player3Bid_Checked(object sender, RoutedEventArgs e)
        {
            isChecked7 = (bool)Player3Bid.IsChecked;
        }

        private void Player3Bid_Click(object sender, RoutedEventArgs e)
        {
            if (Player3Bid.IsChecked == true && !isChecked7)
            {
                Player3Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
            }
            else
            {
                Player3Bid.IsChecked = true;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Visible;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
                Player3DashCall.IsChecked = false;
                Player3DashBlind.IsChecked = false;
                isChecked7 = false;
            }
        }

        private void Player3DashCall_Checked(object sender, RoutedEventArgs e)
        {
            isChecked8 = (bool)Player3DashCall.IsChecked;
        }

        private void Player3DashCall_Click(object sender, RoutedEventArgs e)
        {
            if (Player3DashCall.IsChecked == true && !isChecked8)
            {
                Player3DashCall.IsChecked = false;
            }
            else
            {
                Player3DashCall.IsChecked = true;
                Player3Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
                isChecked8 = false;
            }
        }

        private void Player3DashBlind_Checked(object sender, RoutedEventArgs e)
        {
            isChecked9 = (bool)Player3DashBlind.IsChecked;
        }

        private void Player3DashBlind_Click(object sender, RoutedEventArgs e)
        {
            if (Player3DashBlind.IsChecked == true && !isChecked9)
                Player3DashBlind.IsChecked = false;
            else
            {
                Player3DashBlind.IsChecked = true;
                Player3Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
                isChecked9 = false;
            }
        }

        private void Player4Bid_Checked(object sender, RoutedEventArgs e)
        {
            isChecked10 = (bool)Player4Bid.IsChecked;
        }

        private void Player4Bid_Click(object sender, RoutedEventArgs e)
        {
            if (Player4Bid.IsChecked == true && !isChecked10)
            {
                Player4Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
            }
                
            else
            {
                Player4Bid.IsChecked = true;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Visible;
                MySelectors.BidSuitChoosers[0].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[1].Visibility = Visibility.Collapsed;
                MySelectors.BidSuitChoosers[2].Visibility = Visibility.Collapsed;
                Player4DashCall.IsChecked = false;
                Player4DashBlind.IsChecked = false;
                isChecked10 = false;
            }
        }

        private void Player4DashCall_Checked(object sender, RoutedEventArgs e)
        {
            isChecked11 = (bool)Player4DashCall.IsChecked;
        }

        private void Player4DashCall_Click(object sender, RoutedEventArgs e)
        {
            if (Player4DashCall.IsChecked == true && !isChecked11)
                Player4DashCall.IsChecked = false;
            else
            {
                Player4DashCall.IsChecked = true;
                Player4Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
                isChecked11 = false;
            }
        }

        private void Player4DashBlind_Checked(object sender, RoutedEventArgs e)
        {
            isChecked12 = (bool)Player4DashBlind.IsChecked;
        }

        private void Player4DashBlind_Click(object sender, RoutedEventArgs e)
        {
            if (Player4DashBlind.IsChecked == true && !isChecked12)
                Player4DashBlind.IsChecked = false;
            else
            {
                Player4DashBlind.IsChecked = true;
                Player4Bid.IsChecked = false;
                MySelectors.BidSuitChoosers[3].Visibility = Visibility.Collapsed;
                isChecked12 = false;
            }
        }
        //

        //handling select all text when tapping on text boxes
        private void Player1Name_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player1Name.SelectAll();
        }

        private void Player2Name_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player2Name.SelectAll();
        }

        private void Player3Name_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player3Name.SelectAll();
        }

        private void Player4Name_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player4Name.SelectAll();
        }

        private void Player1RequestedCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player1RequestedCalls.SelectAll();
        }

        private void Player1MadeCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player1MadeCalls.SelectAll();
        }

        private void Player2RequestedCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player2RequestedCalls.SelectAll();
        }

        private void Player2MadeCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player2MadeCalls.SelectAll();
        }

        private void Player3RequestedCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player3RequestedCalls.SelectAll();
        }

        private void Player3MadeCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player3MadeCalls.SelectAll();
        }

        private void Player4RequestedCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player4RequestedCalls.SelectAll();
        }

        private void Player4MadeCalls_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Player4MadeCalls.SelectAll();
        }
        //

        //handling invalid entries for requested and made calls
        private void Player1RequestedCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try 
            {
                callo = Convert.ToInt32(Player1RequestedCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result1 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player1RequestedCalls.Text = "0";
                    Player1RequestedCalls.SelectAll();
                }
            }
            catch (FormatException ex)
            {
                Player1RequestedCalls.Text = "0";
            }
        }

        private void Player1MadeCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player1MadeCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result5 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player1MadeCalls.Text = "0";
                    Player1MadeCalls.SelectAll();
                }
            }

            catch (FormatException ex)
            {
                Player1MadeCalls.Text = "0";
            }
        }

        private void Player2RequestedCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player2RequestedCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result2 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player2RequestedCalls.Text = "0";
                    Player2RequestedCalls.SelectAll();
                }
            }
            catch (FormatException ex)
            {
                Player2RequestedCalls.Text = "0";
            }          
        }

        private void Player2MadeCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player2MadeCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result6 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player2MadeCalls.Text = "0";
                    Player2MadeCalls.SelectAll();
                }
            }
            catch (FormatException ex)
            {
                Player2MadeCalls.Text = "0";
            }
        }

        private void Player3RequestedCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player3RequestedCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result3 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player3RequestedCalls.Text = "0";
                    Player3RequestedCalls.SelectAll();
                }
            }
            catch (FormatException ex)
            {
                Player3RequestedCalls.Text = "0";
            }
        }

        private void Player3MadeCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player3MadeCalls.Text);            
                if (callo > 13)
                {
                    MessageBoxResult result7 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player3MadeCalls.Text = "0";
                    Player3MadeCalls.SelectAll();
                }
            }

            catch (FormatException ex)
            {
                Player3MadeCalls.Text = "0";
            }
        }

        private void Player4RequestedCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player4RequestedCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result4 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player4RequestedCalls.Text = "0";
                    Player4RequestedCalls.SelectAll();
                }
            }

            catch (FormatException ex)
            {
                Player4RequestedCalls.Text = "0";
            }
        }

        private void Player4MadeCalls_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                callo = Convert.ToInt32(Player4MadeCalls.Text);
                if (callo > 13)
                {
                    MessageBoxResult result8 = MessageBox.Show("Invalid Entry ", "", MessageBoxButton.OK);
                    Player4MadeCalls.Text = "0";
                    Player4MadeCalls.SelectAll();
                }
            }

            catch (FormatException ex)
            {
                Player4MadeCalls.Text = "0";
            }
        }
        //
        private void Player1Score_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ScoreSheet.xaml", UriKind.Relative));
        }

        private void Player2Score_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ScoreSheet.xaml", UriKind.Relative));
        }

        private void Player3Score_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ScoreSheet.xaml", UriKind.Relative));
        }

        private void Player4Score_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ScoreSheet.xaml", UriKind.Relative));
        }
        //

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            GFMS.Children.Clear();
        }
        //When you navigate to a page, the old instance is not removed instantly, 
        //so when you add your controls to the new page,
        //they may still be used in the old page if it's not destroyed, it will cause an exception related to parents and children.
        //so we used this function to emty the stackpanel of the page
        private void Undo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (GlobalVariables.OrderOfRound == 1)
            {
                MessageBoxResult result60 = MessageBox.Show("Cannot Undo an Empty Table!", "", MessageBoxButton.OK);
            }
            
            else
            {
                MessageBoxResult YesNo = MessageBox.Show("Are you sure Undo last game ?", "", MessageBoxButton.OKCancel);
                if (YesNo == MessageBoxResult.OK)
                {
                    try
                    {
                        GlobalVariables.CanRedo = true;
                        GlobalVariables.OrderOfRound--;

                        if (Table.Player[0].DashBlind_Chances == false && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound)
                        {
                            Table.Player[0].DashBlind_Chances = true;
                            Player1DashBlind.Visibility = Visibility.Visible;
                            Player1DashBlind.IsChecked = false;
                            Table.Player[0].BDCUndoed = true;
                        }

                        if (Table.Player[1].DashBlind_Chances == false && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound)
                        {
                            Table.Player[1].DashBlind_Chances = true;
                            Player2DashBlind.Visibility = Visibility.Visible;
                            Player2DashBlind.IsChecked = false;
                            Table.Player[1].BDCUndoed = true;
                        }

                        if (Table.Player[2].DashBlind_Chances == false && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound)
                        {
                            Table.Player[2].DashBlind_Chances = true;
                            Player3DashBlind.Visibility = Visibility.Visible;
                            Player3DashBlind.IsChecked = false;
                            Table.Player[2].BDCUndoed = true;
                        }

                        if (Table.Player[3].DashBlind_Chances == false && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound)
                        {
                            Table.Player[3].DashBlind_Chances = true;
                            Player4DashBlind.Visibility = Visibility.Visible;
                            Player4DashBlind.IsChecked = false;
                            Table.Player[3].BDCUndoed = true;
                        }

                        if (GlobalVariables.OrderOfRound == 1)
                        {
                            for (int g = 0; g < 4; g++)
                            {
                                Table.Player[g].TotalScore = 0;
                                Table.Player[g].BidSuits[0] = 'O';
                                Table.Player[g].bids[0] = false;
                                Table.Player[g].DashCalls[0] = false;
                                Table.Player[g].DashBlinds[0] = false;
                                Table.Player[g].Risks[0] = false;
                                Table.Player[g].Withs[0] = false;
                                Table.Player[g].OnlyWinners[0] = false;
                                Table.Player[g].OnlyLosers[0] = false;
                            }

                            GlobalVariables.ALOPSC = 1;
                        }
                        
                        else
                        {
                            for (int g = 0; g < 4; g++)
                            {
                                Table.Player[g].TotalScore = Table.Player[g].AllScores[GlobalVariables.OrderOfRound - 2];

                                Table.Player[g].TempBidSuits = Table.Player[g].BidSuits[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempDashCalls = Table.Player[g].DashCalls[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempDashBlinds = Table.Player[g].DashBlinds[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempRisks = Table.Player[g].Risks[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempWiths = Table.Player[g].Withs[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempOnlyWinners = Table.Player[g].OnlyWinners[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempOnlyLosers = Table.Player[g].OnlyLosers[GlobalVariables.OrderOfRound - 1];
                                Table.Player[g].TempBids = Table.Player[g].bids[GlobalVariables.OrderOfRound - 1];

                                Table.Player[g].BidSuits[GlobalVariables.OrderOfRound - 1] = 'O';
                                Table.Player[g].bids[GlobalVariables.OrderOfRound - 1] = false;
                                Table.Player[g].DashCalls[GlobalVariables.OrderOfRound - 1] = false;
                                Table.Player[g].DashBlinds[GlobalVariables.OrderOfRound - 1] = false;
                                Table.Player[g].Risks[GlobalVariables.OrderOfRound - 1] = false;
                                Table.Player[g].Withs[GlobalVariables.OrderOfRound - 1] = false;
                                Table.Player[g].OnlyWinners[GlobalVariables.OrderOfRound - 1] = false;
                                Table.Player[g].OnlyLosers[GlobalVariables.OrderOfRound - 1] = false;
                            }

                            
                            GlobalVariables.ALOPSC = GlobalVariables.ALOPSCs[GlobalVariables.OrderOfRound - 2];
                        }
                        
                        Player1Score.Text = Convert.ToString(Table.Player[0].TotalScore);
                        Player2Score.Text = Convert.ToString(Table.Player[1].TotalScore);
                        Player3Score.Text = Convert.ToString(Table.Player[2].TotalScore);
                        Player4Score.Text = Convert.ToString(Table.Player[3].TotalScore);
                        
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        MessageBoxResult result61 = MessageBox.Show("Cannot Undo an Empty Table!", "", MessageBoxButton.OK);
                    }
                }
            }
        }

        private void Redo_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (GlobalVariables.CanRedo == false)
            {
                MessageBoxResult result60 = MessageBox.Show("Cannot Redo!", "", MessageBoxButton.OK);
            }
            else
            {
                MessageBoxResult YesNo = MessageBox.Show("Are you sure Redo last game ?", "", MessageBoxButton.OKCancel);
                if (YesNo == MessageBoxResult.OK)
                {
                    GlobalVariables.OrderOfRound++;

                    if (Table.Player[0].BDCUndoed == true && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound - 1)
                    {
                        Table.Player[0].DashBlind_Chances = false;
                        Player1DashBlind.Visibility = Visibility.Collapsed;
                    }

                    if (Table.Player[1].BDCUndoed == true && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound - 1)
                    {
                        Table.Player[1].DashBlind_Chances = false;
                        Player2DashBlind.Visibility = Visibility.Collapsed;
                    }

                    if (Table.Player[2].BDCUndoed == true && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound - 1)
                    {
                        Table.Player[2].DashBlind_Chances = false;
                        Player3DashBlind.Visibility = Visibility.Collapsed;
                    }

                    if (Table.Player[3].BDCUndoed == true && GlobalVariables.BDCMadeAt == GlobalVariables.OrderOfRound - 1)
                    {
                        Table.Player[3].DashBlind_Chances = false;
                        Player4DashBlind.Visibility = Visibility.Collapsed;
                    }

                    for (int g = 0; g < 4; g++)
                    {
                        Table.Player[g].TotalScore = Table.Player[g].AllScores[GlobalVariables.OrderOfRound - 2];

                        Table.Player[g].BidSuits[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempBidSuits;
                        Table.Player[g].bids[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempBids;
                        Table.Player[g].DashCalls[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempDashCalls;
                        Table.Player[g].DashBlinds[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempDashBlinds;
                        Table.Player[g].Risks[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempRisks;
                        Table.Player[g].Withs[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempWiths;
                        Table.Player[g].OnlyWinners[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempOnlyWinners;
                        Table.Player[g].OnlyLosers[GlobalVariables.OrderOfRound - 1] = Table.Player[g].TempOnlyLosers;
                    }

                    GlobalVariables.ALOPSC = GlobalVariables.ALOPSCs[GlobalVariables.OrderOfRound - 2];
                    Player1Score.Text = Convert.ToString(Table.Player[0].TotalScore);
                    Player2Score.Text = Convert.ToString(Table.Player[1].TotalScore);
                    Player3Score.Text = Convert.ToString(Table.Player[2].TotalScore);
                    Player4Score.Text = Convert.ToString(Table.Player[3].TotalScore);
                    GlobalVariables.CanRedo = false;
                }
            }
        }
        
    }
} // alpha version