using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Shapes;
using System.Windows.Media;

namespace EstimationCalculator
{
    public static class TextBlocks
    {
        public static Rectangle[] row = new Rectangle[18];
        public static TextBlock[] OrderOfRoundBlock = new TextBlock[18];
        public static TextBlock[] Player1ScoresBlock = new TextBlock[18];
        public static TextBlock[] Player2ScoresBlock = new TextBlock[18];
        public static TextBlock[] Player3ScoresBlock = new TextBlock[18];
        public static TextBlock[] Player4ScoresBlock = new TextBlock[18];
        static TextBlocks()
        {
            for (int j = 0 ; j < 18 ; j++)
            {
                row[j] = new Rectangle();
                OrderOfRoundBlock[j] = new TextBlock();
                Player1ScoresBlock[j] = new TextBlock();
                Player2ScoresBlock[j] = new TextBlock();
                Player3ScoresBlock[j] = new TextBlock();
                Player4ScoresBlock[j] = new TextBlock();
            }
        }
    }
    
    public partial class ScoreSheet : PhoneApplicationPage
    {
        public ScoreSheet()
        {
            InitializeComponent();

            //adding players names
            Player1Name.Text = Table.Player[0].name;
            Player2Name.Text = Table.Player[1].name;
            Player3Name.Text = Table.Player[2].name;
            Player4Name.Text = Table.Player[3].name;

            for (int k = 0 ; k < GlobalVariables.OrderOfRound-1 ; k++)
            {
                    //adding row lines            
                    TextBlocks.row[k].Fill = new SolidColorBrush(Colors.Black);
                    TextBlocks.row[k].Stroke = new SolidColorBrush(Colors.Black);
                    TextBlocks.row[k].Height = 2;
                    TextBlocks.row[k].Width = 480;
                    TextBlocks.row[k].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    TextBlocks.row[k].VerticalAlignment = System.Windows.VerticalAlignment.Bottom;
                    Grid.SetColumnSpan(TextBlocks.row[k], 5);
                    Grid.SetRow(TextBlocks.row[k], (k));
                    DataScores.Children.Add(TextBlocks.row[k]);

                    //adding column lines
                    column1.Height += 35;
                    column2.Height += 35;
                    column3.Height += 35;
                    column4.Height += 35;

                    //adding order of round
                    TextBlocks.OrderOfRoundBlock[k].HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                    TextBlocks.OrderOfRoundBlock[k].VerticalAlignment = System.Windows.VerticalAlignment.Top;
                    TextBlocks.OrderOfRoundBlock[k].Foreground = new SolidColorBrush(Colors.Black);
                    TextBlocks.OrderOfRoundBlock[k].FontSize = 25;
                    TextBlocks.OrderOfRoundBlock[k].FontWeight = FontWeights.Bold;
                    TextBlocks.OrderOfRoundBlock[k].TextAlignment = TextAlignment.Center;
                    TextBlocks.OrderOfRoundBlock[k].Text = "  " + Convert.ToString(k+1);
                    Grid.SetRow(TextBlocks.OrderOfRoundBlock[k], k);
                    Grid.SetColumn(TextBlocks.OrderOfRoundBlock[k], 0);
                    DataScores.Children.Add(TextBlocks.OrderOfRoundBlock[k]);
                    
                    //adding scores of 4 players
                    TextBlocks.Player1ScoresBlock[k].HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    TextBlocks.Player1ScoresBlock[k].VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    TextBlocks.Player1ScoresBlock[k].Foreground = new SolidColorBrush(Colors.Black);
                    TextBlocks.Player1ScoresBlock[k].FontSize = 25;
                    TextBlocks.Player1ScoresBlock[k].FontWeight = FontWeights.Bold;
                    TextBlocks.Player1ScoresBlock[k].Text = Convert.ToString(Table.Player[0].AllScores[k]);
                    if (String.Equals(Convert.ToString(Table.Player[0].BidSuits[k+1]), "O") == false) TextBlocks.Player1ScoresBlock[k].Text += "," + Convert.ToString(Table.Player[0].BidSuits[k]);
                    if (Table.Player[0].Withs[k] == true) TextBlocks.Player1ScoresBlock[k].Text += ",W";
                    if (Table.Player[0].DashCalls[k] == true) TextBlocks.Player1ScoresBlock[k].Text += ",DC";
                    if (Table.Player[0].DashBlinds[k] == true) TextBlocks.Player1ScoresBlock[k].Text += ",BDC";
                    if (Table.Player[0].Risks[k] == true && GlobalVariables.RiskFactors[k] != 0) TextBlocks.Player1ScoresBlock[k].Text += "," + Convert.ToString(GlobalVariables.RiskFactors[k]) + "R";
                    if (Table.Player[0].OnlyWinners[k] == true) TextBlocks.Player1ScoresBlock[k].Text += ",OW";
                    if (Table.Player[0].OnlyLosers[k] == true) TextBlocks.Player1ScoresBlock[k].Text += ",OL";
                    TextBlocks.Player1ScoresBlock[k].TextAlignment = TextAlignment.Center;
                    Grid.SetRow(TextBlocks.Player1ScoresBlock[k], k);
                    Grid.SetColumn(TextBlocks.Player1ScoresBlock[k], 1);
                    DataScores.Children.Add(TextBlocks.Player1ScoresBlock[k]);

                    TextBlocks.Player2ScoresBlock[k].HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    TextBlocks.Player2ScoresBlock[k].VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    TextBlocks.Player2ScoresBlock[k].Foreground = new SolidColorBrush(Colors.Black);
                    TextBlocks.Player2ScoresBlock[k].FontSize = 25;
                    TextBlocks.Player2ScoresBlock[k].FontWeight = FontWeights.Bold;
                    TextBlocks.Player2ScoresBlock[k].Text = Convert.ToString(Table.Player[1].AllScores[k]);
                    if (String.Equals(Convert.ToString(Table.Player[1].BidSuits[k]), "O") == false) TextBlocks.Player2ScoresBlock[k].Text += "," + Convert.ToString(Table.Player[0].BidSuits[k]);
                    if (Table.Player[1].Withs[k] == true) TextBlocks.Player2ScoresBlock[k].Text += ",W";
                    if (Table.Player[1].DashCalls[k] == true) TextBlocks.Player2ScoresBlock[k].Text += ",DC";
                    if (Table.Player[1].DashBlinds[k] == true) TextBlocks.Player2ScoresBlock[k].Text += ",BDC";
                    if (Table.Player[1].Risks[k] == true && GlobalVariables.RiskFactors[k] != 0) TextBlocks.Player2ScoresBlock[k].Text += "," + Convert.ToString(GlobalVariables.RiskFactors[k]) + "R";
                    if (Table.Player[1].OnlyWinners[k] == true) TextBlocks.Player2ScoresBlock[k].Text += ",OW";
                    if (Table.Player[1].OnlyLosers[k] == true) TextBlocks.Player2ScoresBlock[k].Text += ",OL";
                    TextBlocks.Player2ScoresBlock[k].TextAlignment = TextAlignment.Center;
                    Grid.SetRow(TextBlocks.Player2ScoresBlock[k], k);
                    Grid.SetColumn(TextBlocks.Player2ScoresBlock[k], 2);
                    DataScores.Children.Add(TextBlocks.Player2ScoresBlock[k]);

                    TextBlocks.Player3ScoresBlock[k].HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    TextBlocks.Player3ScoresBlock[k].VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    TextBlocks.Player3ScoresBlock[k].Foreground = new SolidColorBrush(Colors.Black);
                    TextBlocks.Player3ScoresBlock[k].FontSize = 25;
                    TextBlocks.Player3ScoresBlock[k].FontWeight = FontWeights.Bold;
                    TextBlocks.Player3ScoresBlock[k].Text = Convert.ToString(Table.Player[2].AllScores[k]);
                    if (String.Equals(Convert.ToString(Table.Player[2].BidSuits[k]), "O") == false) TextBlocks.Player3ScoresBlock[k].Text += "," + Convert.ToString(Table.Player[0].BidSuits[k]);
                    if (Table.Player[2].Withs[k] == true) TextBlocks.Player3ScoresBlock[k].Text += ",W";
                    if (Table.Player[2].DashCalls[k] == true) TextBlocks.Player3ScoresBlock[k].Text += ",DC";
                    if (Table.Player[2].DashBlinds[k] == true) TextBlocks.Player3ScoresBlock[k].Text += ",BDC";
                    if (Table.Player[2].Risks[k] == true && GlobalVariables.RiskFactors[k] != 0) TextBlocks.Player3ScoresBlock[k].Text += "," + Convert.ToString(GlobalVariables.RiskFactors[k]) + "R";
                    if (Table.Player[2].OnlyWinners[k] == true) TextBlocks.Player3ScoresBlock[k].Text += ",OW";
                    if (Table.Player[2].OnlyLosers[k] == true) TextBlocks.Player3ScoresBlock[k].Text += ",OL";
                    TextBlocks.Player3ScoresBlock[k].TextAlignment = TextAlignment.Center;
                    Grid.SetRow(TextBlocks.Player3ScoresBlock[k], k);
                    Grid.SetColumn(TextBlocks.Player3ScoresBlock[k], 3);
                    DataScores.Children.Add(TextBlocks.Player3ScoresBlock[k]);

                    TextBlocks.Player4ScoresBlock[k].HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    TextBlocks.Player4ScoresBlock[k].VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    TextBlocks.Player4ScoresBlock[k].Foreground = new SolidColorBrush(Colors.Black);
                    TextBlocks.Player4ScoresBlock[k].FontSize = 25;
                    TextBlocks.Player4ScoresBlock[k].FontWeight = FontWeights.Bold;
                    TextBlocks.Player4ScoresBlock[k].Text = Convert.ToString(Table.Player[3].AllScores[k]);
                    if (String.Equals(Convert.ToString(Table.Player[3].BidSuits[k]), "O") == false) TextBlocks.Player4ScoresBlock[k].Text += "," + Convert.ToString(Table.Player[0].BidSuits[k]);
                    if (Table.Player[3].Withs[k] == true) TextBlocks.Player4ScoresBlock[k].Text += ",W";
                    if (Table.Player[3].DashCalls[k] == true) TextBlocks.Player4ScoresBlock[k].Text += ",DC";
                    if (Table.Player[3].DashBlinds[k] == true) TextBlocks.Player4ScoresBlock[k].Text += ",BDC";
                    if (Table.Player[3].Risks[k] == true && GlobalVariables.RiskFactors[k] != 0) TextBlocks.Player4ScoresBlock[k].Text += "," + Convert.ToString(GlobalVariables.RiskFactors[k]) + "R";
                    if (Table.Player[3].OnlyWinners[k] == true) TextBlocks.Player4ScoresBlock[k].Text += ",OW";
                    if (Table.Player[3].OnlyLosers[k] == true) TextBlocks.Player4ScoresBlock[k].Text += ",OL";
                    TextBlocks.Player4ScoresBlock[k].TextAlignment = TextAlignment.Center;
                    Grid.SetRow(TextBlocks.Player4ScoresBlock[k], k);
                    Grid.SetColumn(TextBlocks.Player4ScoresBlock[k], 4);
                    DataScores.Children.Add(TextBlocks.Player4ScoresBlock[k]);
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            DataScores.Children.Clear();
        }//When you navigate to a page, the old instance is not removed instantly, 
        //so when you add your controls to the new page,
        //they may still be used in the old page if it's not destroyed, it will cause an exception related to parents and children.
        //so we used this function to emty the stackpanel of the page

        private void NewTable_Click(object sender, RoutedEventArgs e)
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

            GlobalVariables.iSheet = 0;

            //initialise players data
            for (int i = 0; i < 4; i++)
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

        private void ContinueButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TablePlate.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/TablePlate.xaml", UriKind.Relative));            
        }        
    }
}